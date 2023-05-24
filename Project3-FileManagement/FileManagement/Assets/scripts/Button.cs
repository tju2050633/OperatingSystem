using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FileManagement
{
    public class Button : MonoBehaviour
    {
        private UnityEngine.UI.Button btn;
        private string btnType;
        void Start()
        {
            // 获取button组件, 并添加点击事件监听
            btn = GetComponent<UnityEngine.UI.Button>();
            btn.onClick.RemoveAllListeners(); // 移除旧的监听器
            btn.onClick.AddListener(onClick);
        }

        // 根据按钮类型，让EventSystem执行相应的操作
        private void onClick()
        {
            btnType = gameObject.name;

            if (btnType == "Forward")
            {
                EventSystem.Instance.Forward();
            }
            else if (btnType == "Backward")
            {
                EventSystem.Instance.Backward();
            }
            else if (btnType == "Fold")
            {
                EventSystem.Instance.Fold();
            }
            else if (btnType == "Unfold")
            {
                EventSystem.Instance.Unfold();
            }
            else if (btnType == "AddFile")
            {
                EventSystem.Instance.AddFile();
            }
            else if (btnType == "AddFolder")
            {
                EventSystem.Instance.AddFolder();
            }
            else if (btnType == "DeleteFile")
            {
                EventSystem.Instance.DeleteFile();
            }
            else if (btnType == "DeleteFolder")
            {
                EventSystem.Instance.DeleteFolder();
            }
        }

        // 鼠标悬浮时，鼠标变为手型
        private void OnMouseEnter()
        {
            Debug.Log("OnMouseEnter");
            Cursor.SetCursor(Texture2D.whiteTexture, Vector2.zero, CursorMode.Auto);
        }

        // 鼠标离开时，鼠标变为箭头型
        private void OnMouseExit()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}