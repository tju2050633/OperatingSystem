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
            // 检查是否5个电梯都在报警
            bool all_alerting = elevators[0].getAlerting() && elevators[1].getAlerting() && elevators[2].getAlerting() && elevators[3].getAlerting() && elevators[4].getAlerting();

            // 遍历5个电梯，求出优先级最高的电梯id
            int id = 0, min_prio = Int32.MaxValue;
            foreach (Elevator elev in elevators)
            {
                // 如果不是5个都在报警，则排除正在报警的电梯
                if (!all_alerting && elev.getAlerting())
                    continue;

                // 计算优先级
                int prio = elev.CalcTaskPriority(task_floor, task_direction);

                // 更新条件：优先级更高，或者优先级相同但距离更近
                if (prio < min_prio || (prio == min_prio && id != 0 && Math.Abs(elev.getFloor() - task_floor) < Math.Abs(elevators[id - 1].getFloor() - task_floor)))
                {
                    min_prio = prio;
                    id = elev.id;
                }
            }

            return id;
        }

        /**
         * 设置电梯为警报状态
         * @param id 电梯id
         */
        public void SetAlert(int id, bool alert)
        {
            elevators[id - 1].setAlerting(alert);

        }

        /**
         * 设置电梯开门
         * @param id 电梯id
         */
        public void SetOpen(int id, bool open)
        {
            // 设置停止状态
            elevators[id - 1].setParking(open);
        }
    }
}