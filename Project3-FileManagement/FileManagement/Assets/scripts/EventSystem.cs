using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FileManagement
{
    public class EventSystem : MonoBehaviour
    {
        // 单例模式
        private static EventSystem instance;
        public static EventSystem Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void Forward(bool forward, string name = "")
        {
            // 文件树数据更新
            if (name != "")
                FileTree.Instance.EnterFolder(name);
            else if (forward)
                FileTree.Instance.Forward();
            else
                FileTree.Instance.Backward();

            // GUI按钮更新
            GUIManager.Instance.ModifyButtons();

            // GUI路径更新
            string path = GUIManager.Instance.GetPath();

            if (forward)
            {
                if (name == "")
                    name = FileTree.Instance.GetCurrentDir().name;
                path += " > " + name;
            }
            else
            {
                int index = path.LastIndexOf('>');
                path = path.Substring(0, index - 1);
            }

            GUIManager.Instance.ModifyPath(path);

            // GUI主区域更新
            GUIManager.Instance.ReloadMainArea();
        }

        public void Fold(GameObject bar, bool fold)
        {
            bar.GetComponent<FolderBar>().folded = fold;
            GUIManager.Instance.Fold(bar, fold);
        }

        // TODO
        public void AddItem(bool isFile)
        {
            string name = FileTree.Instance.AddNode(isFile);
            GUIManager.Instance.AddBar(name, isFile);
            GUIManager.Instance.ReloadMainArea();
        }

        public void DeleteItem(bool isFile)
        {
            FileTree.Instance.DeleteNode("File1", isFile);
            GUIManager.Instance.DeleteBar();
            GUIManager.Instance.ReloadMainArea();
        }

        public void RenameItem(FileTree.Node node, string name, bool isFile)
        {
            FileTree.Instance.RenameNode(node, name);
            GUIManager.Instance.RenameBar(node, name);
            GUIManager.Instance.RenameItem(node, name);
            if (!isFile)
                GUIManager.Instance.RenamePath(node, name);
        }
    }
}