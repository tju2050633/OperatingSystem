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
        public void onClick()
        {
            btnType = gameObject.name;

            if (btnType == "Forward" || btnType == "Backward")
            {
                EventSystem.Instance.Forward(btnType == "Forward");
            }
            else if (btnType == "Fold" || btnType == "Unfold")
            {
                // 找到按钮所在的文件夹物体
                GameObject bar = transform.parent.gameObject;
                EventSystem.Instance.Fold(bar, btnType == "Fold");
            }
            // TODO
            else if (btnType == "AddFile" || btnType == "AddFolder")
            {
                EventSystem.Instance.AddItem(btnType == "AddFile");
            }
            else if (btnType == "DeleteFile" || btnType == "DeleteFolder")
            {
                EventSystem.Instance.DeleteItem(btnType == "DeleteFile");
            }
            else if (btnType == "RenameFile" || btnType == "RenameFolder")
            {
                FileTree.Node node = null;
                EventSystem.Instance.RenameItem(node, "newName", btnType == "RenameFile");
            }
        }
    }
}