using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PageSwap
{
    public class BtnSingleStep : MonoBehaviour
    {
        // Start is called before the first frame update
        private UnityEngine.UI.Button btn;

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
            // 根据算法选择指令
            int insAddr = pageSwapManager.getNextInstruction();
            if (insAddr == -1)
                return;

            // 计算页号和页内偏移
            int pageNum = insAddr / 10;
            int pageOffset = insAddr % 10;

            // 计算换出页
            int pageOutNum = pageSwapManager.getPageOutNum(pageNum);

            /* GUI更新 */
            guiManager.deleteEmpty();
            guiManager.addInstruction(pageOutNum, pageNum, insAddr);
            guiManager.updatePage(pageOutNum, pageNum, pageOffset);
            guiManager.updatePanel();
        }
    }
}