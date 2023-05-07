using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ElevatorScheduling
{
    public class UI_Manager : MonoBehaviour
    {
        // 持有电梯容器
        public List<ElevatorCompose> elevators;

        void Start()
        {

        }

        void Update()
        {

        }

        // 设置电梯面板楼层文字
        public void SetPanelText(int id, string content)
        {
            // 找到对应电梯的面板文字
            ElevatorCompose elevator = elevators[id - 1];
            GameObject elevatorPanel = elevator.transform.Find("ElevatorPanel").gameObject;
            GameObject statePanel = elevatorPanel.transform.Find("StatePanel").gameObject;
            TextMeshProUGUI text = statePanel.GetComponentInChildren<TextMeshProUGUI>();

            // 设置面板文字
            text.GetComponent<TextMeshProUGUI>().text = content;
        }

        // 设置电梯面板状态图像
        public void SetPanelImage(int id, int state)
        {
            // 找到对应电梯的面板图片
            ElevatorCompose elevator = elevators[id - 1];
            GameObject elevatorPanel = elevator.transform.Find("ElevatorPanel").gameObject;
            GameObject statePanel = elevatorPanel.transform.Find("StatePanel").gameObject;
            GameObject imageObj = statePanel.transform.Find("Image").gameObject;
            Image image = imageObj.GetComponentInChildren<Image>();

            // 设置面板图片
            string img_path = "";
            switch (state)
            {
                case 2:
                    img_path = "StateAlert";
                    break;
                case 1:
                    img_path = "StateAscending";
                    break;
                case -1:
                    img_path = "StateDescending";
                    break;
                case 0:
                    img_path = "StateIdle";
                    break;
                default:
                    break;
            }
            image.sprite = Resources.Load<Sprite>(img_path);
        }

        // 设置电梯图标
        public void SetElevImage(int id, string path)
        {
            // 找到电梯图像
            ElevatorCompose elevator = elevators[id - 1];
            GameObject elevatorContainer = elevator.transform.Find("ElevatorContainer").gameObject;
            GameObject elevatorImage = elevatorContainer.transform.Find("Elevator").gameObject;
            Image image = elevatorImage.GetComponentInChildren<Image>();

            // 设置电梯图像
            image.sprite = Resources.Load<Sprite>(path);
        }

        // 设置内部按钮颜色
        public void SetInnerBtnActive(int id, string type, bool active)
        {
            // 找到该按钮
            ElevatorCompose elevator = elevators[id - 1];
            GameObject elevatorPanel = elevator.transform.Find("ElevatorPanel").gameObject;
            GameObject btn;
            if(type == "Alert" || type == "Open" || type == "Close")
            {
                GameObject otherBtn = elevatorPanel.transform.Find("OtherBtn").gameObject;
                btn = otherBtn.transform.Find(type).gameObject;
            }
            else
            {
                GameObject floorBtn = elevatorPanel.transform.Find("FloorBtn").gameObject;
                btn = floorBtn.transform.Find(type).gameObject;
            }
            Image image = btn.GetComponentInChildren<Image>();

            // 设置为灰色/非灰色
            Color color = image.color;
            image.color = active ? new Color(color.r, color.g, color.b, 0.8f) : new Color(color.r, color.g, color.b, 1);

            // 设置为可用/不可用
            btn.GetComponent<Button>().interactable = !active;
        }

        // 设置外部一层2个按钮
        public void SetOutBtnActive(int floor, string type, bool active)
        {
            // 找到该层外部按钮，若按钮存在则设置为激活/非激活状态
            GameObject outBtnContainer = GameObject.Find("OutBtn");

            Transform outBtnUpTransform = outBtnContainer.transform.Find(type + floor.ToString());
            if (outBtnUpTransform != null)
            {
                GameObject outBtnUp = outBtnUpTransform.gameObject;
                Image upImage = outBtnUp.GetComponentInChildren<Image>();
                upImage.sprite = active ? Resources.Load<Sprite>("BtnOut" + type + "Active") : Resources.Load<Sprite>("BtnOut" + type);
            }
        }
    }
}