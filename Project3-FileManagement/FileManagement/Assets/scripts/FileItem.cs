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

        // 存储FileTree中对应的节点
        public FileTree.Node node;

        void Start()
        {
            // 获取collider组件, 并添加点击事件监听
            var collider = GetComponent<Collider>();

            // 获取节点
            string name = gameObject.name;
            node = FileTree.Instance.GetNode(name);

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (isClicked)
                {
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
                string content = "File Editor";

                // 绘制小窗口
                GUI.Window(0, new Rect(100, 50, 1000, 600), DoMyWindow, content);
            }
        }

        // 绘制小窗口
        void DoMyWindow(int windowID)
        {
            GUI.SetNextControlName("TextField");
            GUI.FocusControl("TextField");

            string content;
            if (node.start_block == -1)
                content = "Hello world";
            else
                content = FileTree.Instance.fat.GetFileContent(node.start_block);

            // 创建一个用于显示和修改文本的文本框
            GUI.SetNextControlName("TextField");
            // 调整字体
            GUI.skin.textArea.fontSize = 50;
            content = GUI.TextArea(new Rect(10, 30, 980, 560), content);

            // 文本框失去焦点时，保存文本框中的内容
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                GUI.FocusControl(null);
                FileTree.Instance.fat.DeleteFile(node.start_block);
                FileTree.Instance.fat.AllocateFileBlocks(content);
            }

            // 创建关闭窗口的按钮
            if (GUI.Button(new Rect(10, 5, 30, 20), "X"))
            {
                showWindow = false;
                FileTree.Instance.fat.DeleteFile(node.start_block);
                FileTree.Instance.fat.AllocateFileBlocks(content);
            }

            // 使窗口可拖动
            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }

    }
}