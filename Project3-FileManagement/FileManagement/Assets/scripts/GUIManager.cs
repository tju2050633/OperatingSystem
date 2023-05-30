using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        public GameObject buttons;
        public GameObject path;
        public GameObject navi;
        Vector2 navi_original = new Vector2(0, 520);
        Vector2 navi_bias = new Vector2(180, -80);
        public GameObject main_area;
        Vector2 main_area_original = new Vector2(-770, 409);
        Vector2 main_area_bias = new Vector2(270, -200);
        int max_col = 6;
        public GameObject file_item;
        public GameObject folder_item;

        public string GetPath()
        {
            return path.GetComponent<TextMeshProUGUI>().text;
        }

        // 修改按钮状态
        public void ModifyButtons()
        {
            // 获取按钮组件
            UnityEngine.UI.Button btn_backward = buttons.transform.Find("Backward").GetComponent<UnityEngine.UI.Button>();
            UnityEngine.UI.Button btn_forward = buttons.transform.Find("Forward").GetComponent<UnityEngine.UI.Button>();

            // 获取是否可以前进、后退
            bool backward = FileTree.Instance.CanBackward();
            bool forward = FileTree.Instance.CanForward();

            // 可用性
            btn_backward.interactable = backward;
            btn_forward.interactable = forward;

            // 颜色
            ColorBlock cb_backward = btn_backward.colors;
            ColorBlock cb_forward = btn_forward.colors;
            cb_backward.normalColor = backward ? Color.black : Color.gray;
            cb_forward.normalColor = forward ? Color.black : Color.gray;
            btn_backward.colors = cb_backward;
            btn_forward.colors = cb_forward;
        }

        // 修改路径
        public void ModifyPath(string path)
        {
            this.path.GetComponent<TextMeshProUGUI>().text = path;
        }

        // 重新加载主区域
        public void ReloadMainArea()
        {
            // 清空主区域
            foreach (Transform child in main_area.transform)
            {
                Destroy(child.gameObject);
            }

            // 获取当前目录下的文件夹和文件
            List<FileTree.Node> current_dir_children = FileTree.Instance.GetCurrentDirChildren();
            List<FileTree.Node> current_dir_folder_children = new List<FileTree.Node>();
            List<FileTree.Node> current_dir_file_children = new List<FileTree.Node>();

            foreach (FileTree.Node node in current_dir_children)
            {
                if (!node.isFile)
                    current_dir_folder_children.Add(node);
                else
                    current_dir_file_children.Add(node);
            }

            // 重新加载主区域
            int row = 0;
            int col = 0;

            // 添加文件夹
            foreach (FileTree.Node node in current_dir_folder_children)
            {
                string name = node.name;

                GameObject folderItem = Instantiate(folder_item, main_area.transform);
                folderItem.GetComponent<RectTransform>().anchoredPosition = main_area_original + new Vector2(col * main_area_bias.x, row * main_area_bias.y);
                folderItem.gameObject.name = name;
                folderItem.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = name;

                if (col != max_col)
                    col++;
                else
                {
                    col = 0;
                    row++;
                }
            }

            // 添加文件
            foreach (FileTree.Node node in current_dir_file_children)
            {
                string name = node.name;

                GameObject fileItem = Instantiate(file_item, main_area.transform);
                fileItem.GetComponent<RectTransform>().anchoredPosition = main_area_original + new Vector2(col * main_area_bias.x, row * main_area_bias.y);
                fileItem.gameObject.name = name;
                fileItem.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = name;

                if (col != max_col)
                    col++;
                else
                {
                    col = 0;
                    row++;
                }
            }
        }

        public void Fold(GameObject bar, bool fold)
        {
            // 找到name对应物体和箭头
            string arrow_name = fold ? "Fold" : "Unfold";
            GameObject arrow = bar.transform.Find(arrow_name).gameObject;

            // 箭头改名
            arrow.gameObject.name = fold ? "Unfold" : "Fold";

            // 箭头旋转
            arrow.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, fold ? 90 : -90));

            // 获取所有子物体
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in bar.transform.Find("Children"))
            {
                children.Add(child.gameObject);
            }

            // 将所有子物体隐藏或显示
            foreach (GameObject child in children)
            {
                child.SetActive(!fold);
            }

            // 将所有高度小于bar的兄弟物体下移或上移
            foreach (Transform sibling in bar.transform.parent)
            {
                if (sibling.gameObject == bar)
                    continue;
                if (sibling.GetComponent<RectTransform>().anchoredPosition.y < bar.GetComponent<RectTransform>().anchoredPosition.y)
                {
                    float bias = navi_bias.y;
                    sibling.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, fold ? -bias : bias);
                }
            }

        }

        // TODO

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

        public void RenameBar(FileTree.Node node, string name, bool isFile)
        {
            Debug.Log("GUI RenameBar");

            RenameBar(navi, node, name, isFile);
        }

        //////////////////////////////////////////////
        /// 修改名字：bar, item, path
        //////////////////////////////////////////////

        public void RenameBar(GameObject root, FileTree.Node node, string name, bool isFile)
        {
            // 递归找到node对应的物体
            foreach (Transform child in root.transform)
            {
                FileTree.Node cur_node = child.GetComponent<FolderBar>().node;
                if(cur_node == null)
                    cur_node = child.GetComponent<FileBar>().node;

                if (cur_node == node)
                {
                    child.gameObject.name = name;
                    break;
                }

                // 如果是文件夹，还需递归查找
                if (child.GetComponent<FolderBar>() != null)
                {
                    RenameBar(child.gameObject, node, name, isFile);
                }
            }
        }

        public void RenameItem(FileTree.Node node, string name, bool isFile)
        {
            Debug.Log("GUI RenameItem");

            // 从主区域中找到node对应的物体
            foreach (Transform child in main_area.transform)
            {
                FileTree.Node cur_node = child.GetComponent<FolderBar>().node;
                if(cur_node == null)
                    cur_node = child.GetComponent<FileBar>().node;

                if (cur_node == node)
                {
                    child.gameObject.name = name;
                    break;
                }
            }
        }

        public void RenamePath(FileTree.Node node, string name, bool isFile)
        {
            Debug.Log("GUI RenamePath");

            if(!node.onPath)
                return;

            // 从path中找到旧名字子字符串，替换为新名字
            path.GetComponent<TextMeshProUGUI>().text = path.GetComponent<TextMeshProUGUI>().text.Replace(node.name, name);
        }
    }
}