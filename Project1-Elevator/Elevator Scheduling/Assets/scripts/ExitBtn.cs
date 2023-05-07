using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBtn : MonoBehaviour
{
    private UnityEngine.UI.Button btn;

    // Start is called before the first frame update
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

    void onClick()
    {
        Application.Quit();
    }
}
