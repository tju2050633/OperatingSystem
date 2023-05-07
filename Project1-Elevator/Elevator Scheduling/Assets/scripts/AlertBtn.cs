using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElevatorScheduling;

namespace ElevatorScheduling
{
    public class AlertBtn : MonoBehaviour
    {
        // 持有调度器
        public Scheduler scheduler;
        // 持有UI管理器
        public UI_Manager uiManager;

        public int id;
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
            // 设置电梯报警状态
            scheduler.SetAlert(id, true);

            // 改变面板图片
            uiManager.SetPanelImage(id, 2);

            // 将报警按钮设置为不可用
            uiManager.SetInnerBtnActive(id, "Alert", true);

            // 5s后，警报解除
            StartCoroutine(CancelAlertCoroutine(id));
        }

        private IEnumerator CancelAlertCoroutine(int id)
        {
            yield return new WaitForSeconds(5f); // 延迟 5 秒

            // 取消报警状态
            scheduler.SetAlert(id, false);

            // 将报警按钮设置为可用
            uiManager.SetInnerBtnActive(id, "Alert", false);
        }
    }
}