using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElevatorScheduling
{
    public class AlertBtn : MonoBehaviour
    {
        public int id;
        private UnityEngine.UI.Button btn;

        void Start()
        {
            // 获取button组件, 并添加点击事件监听
            btn = GetComponent<UnityEngine.UI.Button>();
            btn.onClick.RemoveAllListeners(); // 移除旧的监听器
            btn.onClick.AddListener(onClick);
        }
        void Update()
        {

        }

        void onClick()
        {
            // 设置电梯状态


            // 改变图片
        }
    }
}