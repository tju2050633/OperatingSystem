using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PageSwap
{
    public class BtnContinuous : MonoBehaviour
    {
        // Start is called before the first frame update
        private UnityEngine.UI.Button btn;
        private UnityEngine.UI.Button btnSingleStep;
        // 页面交换管理器
        private PageSwapManager pageSwapManager;
        // 指示连续执行中
        public bool isContinuous = false;

        void Start()
        {
            // 获取button组件, 并添加点击事件监听
            btn = GetComponent<UnityEngine.UI.Button>();
            btn.onClick.RemoveAllListeners(); // 移除旧的监听器
            btn.onClick.AddListener(onClick);

            btnSingleStep = GameObject.Find("BtnSingleStep").GetComponent<UnityEngine.UI.Button>();

            // 初始化页面交换管理器
            pageSwapManager = GameObject.Find("PageSwapManager").GetComponent<PageSwapManager>();
        }

        void onClick()
        {
            // 连续执行使用协程完成，每0.1秒执行一次单步执行
            StartCoroutine(PauseCoroutine());
        }

        private IEnumerator PauseCoroutine()
        {
            isContinuous = true;
            while (pageSwapManager.getInstructionExecuted() < 320 && isContinuous)
            {
                btnSingleStep.onClick.Invoke();
                yield return new WaitForSeconds(0.01f);
            }
            isContinuous = false;
        }
    }
}