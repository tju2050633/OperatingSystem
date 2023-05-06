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
        public void SetPanelImage(int id, int direction)
        {
            // 找到对应电梯的面板图片
            ElevatorCompose elevator = elevators[id - 1];
            GameObject elevatorPanel = elevator.transform.Find("ElevatorPanel").gameObject;
            GameObject statePanel = elevatorPanel.transform.Find("StatePanel").gameObject;
            GameObject imageObj = statePanel.transform.Find("Image").gameObject;
            Image image = imageObj.GetComponentInChildren<Image>();

            // 设置面板图片
            string img_path = "";
            switch (direction)
            {
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

        // 设置内部按钮颜色为灰色
        public void SetInnerBtnShadow(int id, int floor, bool active)
        {
            // 找到该按钮
            ElevatorCompose elevator = elevators[id - 1];
            GameObject elevatorPanel = elevator.transform.Find("ElevatorPanel").gameObject;
            GameObject floorBtn = elevatorPanel.transform.Find("FloorBtn").gameObject;
            GameObject btn = floorBtn.transform.Find(floor.ToString()).gameObject;
            Image image = btn.GetComponentInChildren<Image>();

            // 设置为灰色/非灰色
            image.color = active ? new Color(0.5f, 0.5f, 0.5f) : new Color(1, 1, 1);
        }

        // 设置外部一层2个按钮为激活状态
        public void SetOutBtnActive(int floor, bool active)
        {
            // 找到该层2个外部按钮，若按钮存在则设置为激活/非激活状态
            GameObject outBtnContainer = GameObject.Find("OutBtn");
            Transform outBtnUpTransform = outBtnContainer.transform.Find("Up" + floor.ToString());
            Transform outBtnDownTransform = outBtnContainer.transform.Find("Down" + floor.ToString());
            if(outBtnUpTransform != null)
            {
                GameObject outBtnUp = outBtnUpTransform.gameObject;
                Image upImage = outBtnUp.GetComponentInChildren<Image>();
                upImage.sprite = active ? Resources.Load<Sprite>("BtnOutUpActive") : Resources.Load<Sprite>("BtnOutUp");
            }
            if(outBtnDownTransform != null)
            {
                GameObject outBtnDown = outBtnDownTransform.gameObject;
                Image downImage = outBtnDown.GetComponentInChildren<Image>();
                downImage.sprite = active ? Resources.Load<Sprite>("BtnOutDownActive") : Resources.Load<Sprite>("BtnOutDown");
            }
        }
    }
}