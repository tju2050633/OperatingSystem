using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElevatorScheduling;

namespace ElevatorScheduling
{
    public class FloorBtn : MonoBehaviour
    {
        // 持有调度器
        public Scheduler scheduler;

        // 持有UI管理器
        public UI_Manager uiManager;
        public int id;
        public int floor;
        private UnityEngine.UI.Button btn;

        void Start()
        {
            // 获取button组件, 并添加点击事件监听
            btn = GetComponent<UnityEngine.UI.Button>();
            btn.onClick.RemoveAllListeners(); // 移除旧的监听器
            btn.onClick.AddListener(onClick);

            // 获取调度器和UI管理器
            scheduler = GameObject.Find("Scheduler").GetComponent<Scheduler>();
            uiManager = GameObject.Find("UI_Manager").GetComponent<UI_Manager>();
        }

        void Update()
        {

        }

        void onClick()
        {
            // 设置按钮颜色灰色
            uiManager.SetInnerBtnShadow(id, floor, true);

            // 通过调度器给所在电梯添加任务
            scheduler.AddInnerTask(floor, id);
        }
    }
}