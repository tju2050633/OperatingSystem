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

            // 初始化文件树
            root = new Node("root", false);
            current = root;

            Node folder1 = new Node("Folder1", false);
            Node folder2 = new Node("Folder2", false);
            Node file1 = new Node("File1", true);
            Node file2 = new Node("File2", true);
            Node file3 = new Node("File3", true);
            Node file4 = new Node("File4", true);

            root.AddChild(folder1);
            root.AddChild(folder2);
            root.AddChild(file1);
            root.AddChild(file2);
            folder1.AddChild(file3);
            folder2.AddChild(file4);
        }

        // 文件树所需数据结构
        public class Node
        {
            public string name;
            public bool isFile;
            public List<Node> children;
            public Node parent;
            public bool onPath;

            public Node(string name, bool isFile)
            {
                this.name = name;
                this.isFile = isFile;
                this.children = new List<Node>();
                this.parent = null;
                onPath = false;
            }

            public void AddChild(Node child)
            {
                this.children.Add(child);
                child.parent = this;
            }

            public void RemoveChild(Node child)
            {
                this.children.Remove(child);
                child.parent = null;
            }
        }

        private Node root;
        private Node current;

        // GUI组件持有它对应的文件树内部节点
        // 可能是不好的依赖，或没有用
        // 保留删除两个GetNode方法的可能性

        public Node GetNode(string name)
        {
            // 从root开始递归查找
            return GetNode(root, name);
        }

        private Node GetNode(Node node, string name)
        {
            // 如果找到了，返回该节点
            if (node.name == name)
            {
                return node;
            }

            // 否则递归查找
            foreach (Node child in node.children)
            {
                Node result = GetNode(child, name);
                if (result != null)
                {
                    return result;
                }
            }

            // 如果没找到，返回null
            return null;
        }

        public Node GetCurrentDir()
        {
            return current;
        }

        public List<Node> GetCurrentDirChildren()
        {
            List<Node> children = new List<Node>();

            foreach (Node child in current.children)
            {
                children.Add(child);
            }

            return children;
        }

        public bool CanBackward()
        {
            return current.parent != null;
        }

        public bool CanForward()
        {
            foreach (Node child in current.children)
            {
                if (child.onPath)
                {
                    return true;
                }
            }
            return false;
        }

        public void EnterFolder(string name)
        {
            // 当前节点所有子节点的onPath设为false
            foreach (Node child in current.children)
            {
                child.onPath = false;
            }

            // 找出子节点中名字为name的节点，将其设为当前节点
            Node next = null;
            foreach (Node child in current.children)
            {
                if (child.name == name && !child.isFile)
                {
                    next = child;
                    break;
                }
            }
            if (next == null)
            {
                Debug.Log("FileTree EnterFolder Error: No such folder");
                return;
            }
            current = next;

            // 当前节点的onPath设为true
            current.onPath = true;
        }

        // 沿着路径前进
        public void Forward()
        {
            // 找出子节点中onPath为true的节点，将其设为当前节点
            Node next = null;
            foreach (Node child in current.children)
            {
                if (child.onPath)
                {
                    next = child;
                    break;
                }
            }
            if (next == null)
            {
                Debug.Log("FileTree Forward Error: No child on path");
                return;
            }
            current = next;
        }

        // 沿着路径后退
        public void Backward()
        {
            // 返回父节点
            if (current.parent == null)
            {
                Debug.Log("FileTree Backward Error: No parent");
                return;
            }
            current = current.parent;
        }

        // TODO

        // 添加文件/文件夹
        public string AddNode(bool isFile)
        {
            Debug.Log("FileTree AddNode");

            // 找一个当前文件夹没有的名字
            string name = "New";
            List<string> names = new List<string>();
            foreach (Node child in current.children)
            {
                if(child.isFile == isFile)
                    names.Add(child.name);
            }

            int i = 1;
            while (true)
            {
                if (!names.Contains(name))
                {
                    break;
                }
                name = "New" + i.ToString();
                i++;
            }

            // 创建新节点，加入当前文件夹
            Node node = new Node(name, isFile);
            current.AddChild(node);

            return name;
        }


        // 删除文件/文件夹
        public void DeleteNode(string name, bool isFile)
        {
            Debug.Log("FileTree DeleteNode");

            Node node = null;
            foreach (Node child in current.children)
            {
                if (child.name == name && child.isFile == isFile)
                {
                    node = child;
                    break;
                }
            }
            if (node == null)
            {
                Debug.Log("FileTree DeleteNode Error: No such node");
                return;
            }
            current.RemoveChild(node);

            // 如果是文件夹，则递归删除子节点
            if (isFile)
                return;

            foreach (Node child in node.children)
            {
                DeleteNode(child.name, true);
                DeleteNode(child.name, false);
            }
        }

        // 重命名文件/文件夹
        public void RenameNode(Node node, string name)
        {
            Debug.Log("FileTree RenameNode");

            node.name = name;
        }
    }
}