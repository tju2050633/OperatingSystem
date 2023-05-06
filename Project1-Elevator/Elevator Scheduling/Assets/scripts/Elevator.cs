using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElevatorScheduling;

namespace ElevatorScheduling
{
    public class Elevator : MonoBehaviour
    {
        // 持有UI管理器
        public UI_Manager uiManager;

        public int id;  // 电梯id
        private int floor;  // 当前所在楼层
        private int direction;  // 电梯运行方向：1上，-1下，0停止
        private bool alerting;  // 是否报警
        private bool parking;  // 是否停靠
        private List<int> innerTasks;  // 内部任务队列
        private const float distance = 49.0f; // 两层楼间的距离
        private List<float> heights;  // 每层楼的高度

        void Start()
        {
            // 初始化
            floor = 1;
            direction = 0;
            parking = false;
            alerting = false;
            innerTasks = new List<int>();
            uiManager = GameObject.Find("UI_Manager").GetComponent<UI_Manager>();

            // 计算每层楼的高度
            float y = transform.position.y;
            heights = new List<float>();
            for (int i = 0; i < 20; i++)
            {
                heights.Add(y + i * distance);
            }
        }

        /*
        * 电梯移动
        */
        void Update()
        {
            // 停靠、报警、无任务时不移动
            if (parking || alerting || innerTasks.Count == 0)
                return;

            // 判定移动方向，更新电梯图片
            UpdateDirection();

            // 每帧移动一次，速率为每秒1层楼
            transform.Translate(Vector3.up * distance * direction * Time.deltaTime);

            // 判断是否到达某层
            float y = transform.position.y;
            int f;
            for (f = 1; f <= 20; f++)
            {
                // 电梯于某层距离小于0.5f时，判定抵达该层
                if (Mathf.Abs(y - heights[f - 1]) < 0.5f && f != floor)
                {
                    if (innerTasks[0] == f)
                    {
                        Park(f);
                    }

                    floor = f;
                    uiManager.SetPanelText(id, floor.ToString());
                    return;
                }
            }
        }

        /*
        * 每帧检查电梯方向，并设置状态图片
        */
        private void UpdateDirection()
        {
            if (innerTasks.Count == 0)
                direction = 0;
            else
                direction = innerTasks[0] > floor ? 1 : -1;
            
            uiManager.SetPanelImage(id, direction);
        }

        /*
        * 电梯停靠
        */
        public void Park(int park_floor)
        {
            // 去掉对应的任务
            innerTasks.Remove(park_floor);

            // 设置电梯自身状态停止
            parking = true;

            // 开门
            uiManager.SetElevImage(id, "ElevatorOpen");

            // 设置3秒后电梯继续运行
            Invoke("Continue", 3.0f);

            // 设置该楼层外面的对应上/下按钮非激活
            uiManager.SetOutBtnActive(park_floor, false);

            // 设置电梯内部该楼层按钮去掉灰色
            uiManager.SetInnerBtnShadow(id, park_floor, false);
        }

        private void Continue()
        {
            parking = false;

            // 关门
            uiManager.SetElevImage(id, "Elevator");

            // 若停靠在其他楼层，则3秒后检查是否还有任务，若没有则返回1楼
            if (floor != 1)
                Invoke("CheckIdle", 3.0f);
        }

        private void CheckIdle()
        {
            if (innerTasks.Count == 0)
            {
                innerTasks.Add(1);
            }
        }

        /*
        * 计算任务优先级
        */
        public int CalcTaskPriority(int task_floor, int task_direction)
        {
            // task_direction为0，说明是内部任务，电梯经过该楼层
            // task_direction为1，任务指定上行，task_direction为-1，任务指定下行
            bool up_or_pass = task_direction >= 0;
            bool down_or_pass = task_direction <= 0;

            for (int i = 0; i < innerTasks.Count; i++)
            {
                // 计算电梯移动路径两端的楼层
                int last_floor = i == 0 ? floor : innerTasks[i - 1];
                int next_floor = innerTasks[i];

                // 检查电梯任务列表，计算目标任务是否在电梯移动路径两端的楼层之间
                if (last_floor < next_floor && up_or_pass && task_floor >= last_floor && task_floor < next_floor
                || last_floor > next_floor && down_or_pass && task_floor <= last_floor && task_floor > next_floor)
                {
                    return i;
                }
            }

            // 若没有找到合适的位置，则优先级排最低
            return innerTasks.Count;
        }

        /*
        * 分配任务，指定其优先级
        */
        public void AssignTask(int task_floor, int task_direction)
        {
            // 计算任务优先级
            int priority = CalcTaskPriority(task_floor, task_direction);

            // 若任务重复，则不添加
            if (innerTasks.Contains(task_floor))
                return;

            // 添加任务到内部任务队列
            innerTasks.Insert(priority, task_floor);
        }
    }
}