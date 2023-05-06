using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElevatorScheduling
{
    public class Scheduler : MonoBehaviour
    {
        // 持有5个电梯
        public Elevator[] elevators;

        void Start()
        {

        }
        void Update()
        {

        }

        /**
         * 添加外部任务
         * @param floor 楼层
         * @param direction 方向：1上，-1下
         */
        public void AddOutTask(int task_floor, int task_direction)
        {
            // 计算任务分配给哪个电梯
            int elev_id = CalcProperElev(task_floor, task_direction);

            // 分配任务
            elevators[elev_id - 1].AssignTask(task_floor, task_direction);
        }

        /**
         * 添加内部任务
         * @param floor 楼层
         */
        public void AddInnerTask(int task_floor, int elev_id)
        {
            // 分配任务
            elevators[elev_id - 1].AssignTask(task_floor, 0);
        }

        /**
         * 计算任务分配给哪个电梯
         * @param floor 楼层
         * @param direction 方向：1上，-1下
         */
        public int CalcProperElev(int task_floor, int task_direction)
        {
            // 遍历5个电梯，求出优先级最高的电梯id
            int id = 0, min_prio = Int32.MaxValue;
            foreach (Elevator elev in elevators)
            {
                // 计算优先级
                int prio = elev.CalcTaskPriority(task_floor, task_direction);
                if (prio < min_prio)
                {
                    min_prio = prio;
                    id = elev.id;
                }
            }
            return id;
        }
    }
}