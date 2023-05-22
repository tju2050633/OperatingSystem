using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update
    private UnityEngine.UI.Button btn;
    void Start()
    {
        // 获取button组件, 并添加点击事件监听
        btn = GetComponent<UnityEngine.UI.Button>();
        btn.onClick.RemoveAllListeners(); // 移除旧的监听器
        btn.onClick.AddListener(onClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void onClick()
    {
        Debug.Log("Button Clicked!");
    }
}
