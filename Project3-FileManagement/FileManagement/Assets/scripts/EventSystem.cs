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

        void Start()
        {

        }

        public void Forward()
        {
            FileTree.Instance.Forward();
            GUIManager.Instance.ModifyPath();
            GUIManager.Instance.ReloadMainArea();
        }

        public void Backward()
        {
            FileTree.Instance.Backward();
            GUIManager.Instance.ModifyPath();
            GUIManager.Instance.ReloadMainArea();
        }

        public void Fold()
        {
            GUIManager.Instance.Fold();
        }

        public void Unfold()
        {
            GUIManager.Instance.Unfold();
        }

        public void AddFile()
        {
            FileTree.Instance.AddFile();
            GUIManager.Instance.AddFileBar();
            GUIManager.Instance.AddFileItem();
        }

        public void AddFolder()
        {
            FileTree.Instance.AddFolder();
            GUIManager.Instance.AddFolderBar();
            GUIManager.Instance.AddFolderItem();
        }

        public void DeleteFile()
        {
            FileTree.Instance.DeleteFile();
            GUIManager.Instance.DeleteFileBar();
            GUIManager.Instance.DeleteFileItem();
        }

        public void DeleteFolder()
        {
            FileTree.Instance.DeleteFolder();
            GUIManager.Instance.DeleteFolderBar();
            GUIManager.Instance.DeleteFolderItem();
        }
    }
}