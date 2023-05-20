using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PageSwap
{
    public class GUIManager : MonoBehaviour
    {
        // 页面交换管理器
        private PageSwapManager pageSwapManager;
        // panel组件
        public GameObject pageMissedNum;
        public GameObject pageMissedRate;
        // list组件
        public GameObject pageList;
        public GameObject instructionListContent;
        // empty组件
        public TextMeshProUGUI pageEmpty;
        public TextMeshProUGUI instructionEmpty;
        // prefabs
        public GameObject instructionPrefab;
        // other
        Color originalColor;

        void Start()
        {
            // 初始化页面交换管理器
            pageSwapManager = GameObject.Find("PageSwapManager").GetComponent<PageSwapManager>();

            // 获取初始颜色
            originalColor = pageList.transform.Find("Page 0").Find("0").GetComponent<Image>().color;
        }

        public void reset()
        {
            // 重置panel
            pageMissedNum.GetComponent<TextMeshProUGUI>().text = "0";
            pageMissedRate.GetComponent<TextMeshProUGUI>().text = "0%";

            // 重置页面
            foreach (Transform child in pageList.transform)
            {
                child.gameObject.SetActive(false);
            }
            pageEmpty.gameObject.SetActive(true);

            // 重置指令
            foreach (Transform child in instructionListContent.transform)
            {
                if (child.gameObject.name == "Original")
                    continue;
                Destroy(child.gameObject);
            }
            instructionEmpty.gameObject.SetActive(true);
        }

        public void deleteEmpty()
        {
            /* 删除empty */
            if (pageEmpty.IsActive())
            {
                pageEmpty.gameObject.SetActive(false);
            }
            if (instructionEmpty.IsActive())
            {
                instructionEmpty.gameObject.SetActive(false);
            }
        }

        public void addInstruction(int pageOutNum, int pageNum, int insAddr)
        {
            // 指令增加一条
            Vector3 pos = instructionListContent.transform.Find("Original").transform.position + new Vector3(0, -20, 0) * pageSwapManager.getInstructionExecuted();
            GameObject newInstruction = Instantiate(instructionPrefab, pos, Quaternion.identity);
            newInstruction.transform.SetParent(instructionListContent.transform);
            newInstruction.transform.localScale = new Vector3(2, 0.2f, 1);

            // 指令文本内容
            newInstruction.transform.Find("ID").GetComponent<TextMeshProUGUI>().text = pageSwapManager.getInstructionExecuted().ToString();
            newInstruction.transform.Find("Address").GetComponent<TextMeshProUGUI>().text = insAddr.ToString();

            if (pageOutNum == -1)
            {
                newInstruction.transform.Find("Missed").GetComponent<TextMeshProUGUI>().text = "No";
                newInstruction.transform.Find("SwapOutPage").GetComponent<TextMeshProUGUI>().text = "-";
                newInstruction.transform.Find("SwapInPage").GetComponent<TextMeshProUGUI>().text = "-";
            }
            else if (pageOutNum == -2)
            {
                newInstruction.transform.Find("Missed").GetComponent<TextMeshProUGUI>().text = "Yes";
                newInstruction.transform.Find("SwapOutPage").GetComponent<TextMeshProUGUI>().text = "-";
                newInstruction.transform.Find("SwapInPage").GetComponent<TextMeshProUGUI>().text = pageNum.ToString();
            }
            else
            {
                newInstruction.transform.Find("Missed").GetComponent<TextMeshProUGUI>().text = "Yes";
                newInstruction.transform.Find("SwapOutPage").GetComponent<TextMeshProUGUI>().text = pageOutNum.ToString();
                newInstruction.transform.Find("SwapInPage").GetComponent<TextMeshProUGUI>().text = pageNum.ToString();
            }
        }

        public void updatePage(int pageOutNum, int pageNum, int pageOffset)
        {
            // 不换出、不换入，对应指令地址闪烁
            if (pageOutNum == -1)
            {
                // 找到指令所在页
                string pageName = "Page " + pageNum.ToString();
                GameObject page = pageList.transform.Find(pageName.ToString()).gameObject;

                // 找到换出页对应的地址，闪烁
                GameObject addr = page.transform.Find(pageOffset.ToString()).gameObject;
                StartCoroutine(BlinkCoroutine(addr));
            }
            // 换入、不换出，增加页面
            else if (pageOutNum == -2)
            {
                // 找到未激活页面，激活并修改名字、文本
                GameObject newPage = null;
                for (int i = 0; i < 4; i++)
                {
                    Transform pageTransform = pageList.transform.GetChild(i);
                    newPage = pageTransform.gameObject;
                    if (!newPage.activeSelf)
                    {
                        break;
                    }
                }

                newPage.SetActive(true);
                string pageName = "Page " + pageNum.ToString();
                newPage.name = pageName;
                newPage.transform.Find("PageNum").GetComponent<TextMeshProUGUI>().text = pageName;

                // 找到指令地址，闪烁
                GameObject addr = newPage.transform.Find(pageOffset.ToString()).gameObject;
                StartCoroutine(BlinkCoroutine(addr));
            }
            // 换入、换出，删除换出页，替换为换入页
            else
            {
                // 找到换出页，修改名字、文本
                string outPageName = "Page " + pageOutNum.ToString();
                GameObject page = pageList.transform.Find(outPageName).gameObject;

                string inPageName = "Page " + pageNum.ToString();
                page.name = inPageName;
                page.transform.Find("PageNum").GetComponent<TextMeshProUGUI>().text = inPageName;

                // 指令颜色恢复
                for (int i = 0; i < 10; i++)
                {
                    GameObject ins = page.transform.Find(i.ToString()).gameObject;
                    Image image = ins.GetComponent<Image>();
                    image.color = originalColor;
                }

                // 找到指令地址，闪烁
                GameObject addr = page.transform.Find(pageOffset.ToString()).gameObject;
                StartCoroutine(BlinkCoroutine(addr));
            }
        }

        private IEnumerator BlinkCoroutine(GameObject addr)
        {
            Image image = addr.GetComponent<Image>();

            image.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            image.color = originalColor;
        }

        public void updatePanel()
        {
            pageMissedNum.GetComponent<TextMeshProUGUI>().text = pageSwapManager.getPageMissedNum().ToString();

            float rate = pageSwapManager.getPageMissedRate();
            rate = (float)Math.Round(rate, 2);
            pageMissedRate.GetComponent<TextMeshProUGUI>().text = rate + "%";
        }
    }
}