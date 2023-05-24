using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FileManagement
{
    public class FileItem : MonoBehaviour, IPointerClickHandler
    {
        private bool isClicked = false;
        private float clickTimeThreshold = 0.3f; // 双击时间阈值，单位为秒
        public bool showWindow = false; // 是否显示小窗口

        void Start()
        {
            // 获取collider组件, 并添加点击事件监听
            var collider = GetComponent<Collider>();


        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (isClicked)
                {
                    // 双击逻辑
                    Debug.Log("Double Clicked!");

                    // 显示文件内容窗口
                    showWindow = !showWindow;
                }
                else
                {
                    isClicked = true;
                    Invoke("ResetClick", clickTimeThreshold);
                }
            }
        }

        private void ResetClick()
        {
            isClicked = false;
        }

        // OnGUI()自动调用, 用于绘制GUI
        void OnGUI()
        {
            // Make sure we only call GUI.Window if doWindow0 is true.
            if (showWindow)
            {
                string content = "Hello World!";

                // 绘制小窗口
                GUI.Window(0, new Rect(110, 10, 2000, 600), DoMyWindow, content);
            }
        }

        // 绘制小窗口
        void DoMyWindow(int windowID)
        {
            GUI.Button(new Rect(10, 30, 800, 200), "Click Me!");
            GUI.Label(new Rect(10, 10, 100, 20), "Hello World!");

            // Make the windows be draggable.
            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }
    }
}