using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FileManagement
{
    public class GUIManager : MonoBehaviour
    {
        // 单例模式
        private static GUIManager instance;
        public static GUIManager Instance
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

        // GUI组件
        public GameObject path;
        public GameObject navi;
        public GameObject main_area;
        void Start()
        {

        }

        public void ModifyPath()
        {
            Debug.Log("ModifyPath");
        }

        public void ReloadMainArea()
        {
            Debug.Log("ReloadMainArea");
        }

        public void Fold()
        {
            Debug.Log("GUI Fold");
        }

        public void Unfold()
        {
            Debug.Log("GUI Unfold");
        }

        public void AddFileBar()
        {
            Debug.Log("GUI AddFileBar");
        }

        public void AddFolderBar()
        {
            Debug.Log("GUI AddFolderBar");
        }

        public void DeleteFolderBar()
        {
            Debug.Log("GUI DeleteFolderBar");
        }

        public void DeleteFileBar()
        {
            Debug.Log("GUI DeleteFileBar");
        }

        public void AddFileItem()
        {
            Debug.Log("GUI AddFileItem");
        }

        public void AddFolderItem()
        {
            Debug.Log("GUI AddFolderItem");
        }

        public void DeleteFileItem()
        {
            Debug.Log("GUI DeleteFileItem");
        }

        public void DeleteFolderItem()
        {
            Debug.Log("GUI DeleteFolderItem");
        }
    }
}