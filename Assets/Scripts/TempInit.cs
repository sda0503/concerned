using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempInit : MonoBehaviour
{
    public Button b1;
    public Button b2;
    public Button b3;

    void Start()
    {
        DataManager.Instance.init();
        //b1.onClick.AddListener(GameManager.Instance.DateChange);
        //b2.onClick.AddListener(ssss);
        //b3.onClick.AddListener(() => DataManager.Instance.OnClickToFindItem(1));
    }

    void ssss()
    {
        string ss = "��ż� Ž�� �ڵ���";
        PopupUIManager.Instance.popupUI["PhoneUI"].SetActive(false);
        PopupUIManager.Instance.OpenPopupUI<ChatCalendarUI>().charactername = ss.Replace("�ڵ���", "");
    }
}
