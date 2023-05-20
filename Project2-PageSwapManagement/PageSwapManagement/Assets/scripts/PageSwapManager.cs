using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PageSwap
{
    public class PageSwapManager : MonoBehaviour
    {
        System.Random Random = new System.Random();
        // 页
        private int pageMissedNum = 0;
        private float pageMissedRate = 0;
        private List<int> pageQueue = new List<int>();

        // 指令
        public List<bool> instructionList = new List<bool>();
        private int instructionExecuted = 0;

        // 选取指令算法
        private bool up_down = true;
        private int currentIns = -1;
        private bool next = false;
        void Start()
        {
            // 指令列表初始化为320个false
            for (int i = 0; i < 320; i++)
            {
                instructionList.Add(false);
            }
        }

        // 重置
        public void reset()
        {
            pageMissedNum = 0;
            pageMissedRate = 0;
            pageQueue.Clear();
            for (int i = 0; i < 320; i++)
            {
                instructionList[i] = false;
            }
            instructionExecuted = 0;
            up_down = true;
            currentIns = -1;
            next = false;

        }

        // 页面算法
        public int getPageMissedNum()
        {
            return pageMissedNum;
        }
        public float getPageMissedRate()
        {
            return pageMissedRate;
        }
        public int getPageOutNum(int pageNum)
        {
            // 如果页面列表存在该页面，不换出、不换入，返回-1
            if (pageQueue.Contains(pageNum))
            {
                pageMissedRate = pageMissedNum * 100f / instructionExecuted;
                return -1;
            }
            // 如果页面列表未满且需要的页面不存在，换入、不换出，返回-2
            else if(pageQueue.Count < 4)
            {
                // 计算缺页(率)
                pageMissedNum += 1;
                pageMissedRate = pageMissedNum * 100f / instructionExecuted;

                pageQueue.Add(pageNum);
                return -2;
            }
            // 否则弹出队首页面，加入需要的页面，返回换出的页面号
            else
            {
                // 计算缺页(率)
                pageMissedNum += 1;
                pageMissedRate = pageMissedNum * 100f / instructionExecuted;

                int pageOutNum = pageQueue[0];
                pageQueue.RemoveAt(0);
                pageQueue.Add(pageNum);
                return pageOutNum;
            }
        }

        // 指令算法
        public int getInstructionExecuted()
        {
            return instructionExecuted;
        }
        public int getNextInstruction()
        {
            if (instructionExecuted == 320)
            {
                return -1;
            }
            // 若执行m + 1
            if (next && currentIns < 319 && instructionList[currentIns + 1] == false)
            {
                next = false;
                currentIns += 1;
                instructionList[currentIns] = true;
            }
            // 不执行m + 1，则轮流执行0～m-1和m+1～319
            else
            {
                next = true;
                int low, high;
                if (up_down)
                {
                    low = currentIns + 1;
                    high = 320;
                }
                else
                {
                    low = 0;
                    high = currentIns - 1;
                }
                up_down = !up_down;

                // 获取对应区间未执行的指令
                List<int> unexecuted = new List<int>();
                for (int i = low; i < high; i++)
                {
                    if (instructionList[i] == false)
                    {
                        unexecuted.Add(i);
                    }
                }

                // 一边没有未执行的指令，则获取另一边的未执行指令
                if (unexecuted.Count == 0)
                {
                    low = low == 0 ? currentIns + 1 : 0;
                    high = high == 320 ? currentIns - 1 : 320;
                    for (int i = low; i < high; i++)
                    {
                        if (instructionList[i] == false)
                        {
                            unexecuted.Add(i);
                        }
                    }
                }

                currentIns = unexecuted[Random.Next(0, unexecuted.Count)];
                instructionList[currentIns] = true;
            }

            instructionExecuted += 1;

            return currentIns;
        }
    }
}
