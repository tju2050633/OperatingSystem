using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElevatorScheduling;

public class CloseBtn : MonoBehaviour
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
        // 设置电梯为运行状态
        scheduler.SetOpen(id, false);

        // 设置图像为关门
        uiManager.SetElevImage(id, "Elevator");

        // 设置开门按钮为立即可用
        uiManager.SetInnerBtnActive(id, "Open", false);
    }
}
