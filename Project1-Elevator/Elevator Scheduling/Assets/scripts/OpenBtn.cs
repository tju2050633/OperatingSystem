using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElevatorScheduling;
using UnityEngine.UI;

public class OpenBtn : MonoBehaviour
{
    // 持有调度器
    public Scheduler scheduler;
    // 持有UI管理器
    public UI_Manager uiManager;

    public int id;
    private UnityEngine.UI.Button btn;

    void Start()
    {
        // 获取button组件, 并添加点击事件监听
        btn = GetComponent<UnityEngine.UI.Button>();
        btn.onClick.RemoveAllListeners(); // 移除旧的监听器
        btn.onClick.AddListener(onClick);

        // 获取调度器和UI管理器
        scheduler = GameObject.Find("Scheduler").GetComponent<Scheduler>();
        uiManager = GameObject.Find("UI_Manager").GetComponent<UI_Manager>();
    }

    void Update()
    {

    }

    void onClick()
    {
        // 设置电梯为停靠状态
        scheduler.SetOpen(id, true);

        // 设置图像为开门
        uiManager.SetElevImage(id, "ElevatorOpen");

        // 设置面板图像为P
        uiManager.SetPanelImage(id, 0);

        // 将开门按钮设置为不可用
        uiManager.SetInnerBtnActive(id, "Open", true);

        // 3s后自动解除停靠状态、关门
        StartCoroutine(CancelOpenCoroutine(id));
    }

    IEnumerator CancelOpenCoroutine(int id)
    {
        yield return new WaitForSeconds(3f);

        // 如果开门按钮为可用状态，说明已经被关门按钮取消了
        if (btn.GetComponent<Button>().interactable == false)
        {
            // 设置电梯为非停靠状态
            scheduler.SetOpen(id, false);

            // 设置图像为关门
            uiManager.SetElevImage(id, "Elevator");

            // 将开门按钮设置为可用
            uiManager.SetInnerBtnActive(id, "Open", false);
        }
    }
}
