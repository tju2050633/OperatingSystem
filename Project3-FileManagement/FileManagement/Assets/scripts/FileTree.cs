using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FileManagement
{
    public class FileTree : MonoBehaviour
    {
        // 单例模式
        private static FileTree instance;
        public static FileTree Instance
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
            Debug.Log("FileTree Forward");
        }

        public void Backward()
        {
            Debug.Log("FileTree Backward");
        }

        public void AddFile()
        {
            Debug.Log("FileTree AddFile");
        }

        public void AddFolder()
        {
            Debug.Log("FileTree AddFolder");
        }

        public void DeleteFile()
        {
            Debug.Log("FileTree DeleteFile");
        }

        public void DeleteFolder()
        {
            Debug.Log("FileTree DeleteFolder");
        }
    }
}