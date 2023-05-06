using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBtn : MonoBehaviour
{
    public int id;
    private UnityEngine.UI.Button btn;

    void Start()
    {
        // 获取button组件, 并添加点击事件监听
        btn = GetComponent<UnityEngine.UI.Button>();
        btn.onClick.RemoveAllListeners(); // 移除旧的监听器
        btn.onClick.AddListener(onClick);
    }

    void Update()
    {

    }

    void onClick()
    {
        Debug.Log(id + "Close");
    }
}
