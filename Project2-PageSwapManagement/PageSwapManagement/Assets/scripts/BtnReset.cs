using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PageSwap
{
    public class BtnReset : MonoBehaviour
    {
        // Start is called before the first frame update
        private UnityEngine.UI.Button btn;
        public BtnContinuous btnContinuous;
        // 页面交换管理器
        private PageSwapManager pageSwapManager;
        // GUI管理器
        private GUIManager guiManager;

        void Start()
        {
            // 获取button组件, 并添加点击事件监听
            btn = GetComponent<UnityEngine.UI.Button>();
            btn.onClick.RemoveAllListeners(); // 移除旧的监听器
            btn.onClick.AddListener(onClick);

            // 初始化页面交换管理器
            pageSwapManager = GameObject.Find("PageSwapManager").GetComponent<PageSwapManager>();

            // 初始化GUI管理器
            guiManager = GameObject.Find("GUIManager").GetComponent<GUIManager>();
        }

        void onClick()
        {
            // 数据重置
            pageSwapManager.reset();

            // GUI重置
            guiManager.reset();

            // 如果连续执行中，停止连续执行
            btnContinuous.isContinuous = false;
        }
    }
}