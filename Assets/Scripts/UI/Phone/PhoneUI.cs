using DataStorage;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhoneUI : PopupUIBase
{
    public GameObject exTuto;
    public List<GameObject> imageTuto;
    int num = 0;

    public Button callButton;
    public Button messageButton;
    public Button calendarButton;

    public TextMeshProUGUI timeText;

    public GameObject CallObject;
    public GameObject MessageObject;
    public GameObject CalendarObject;

    private void Start()
    {
        exTuto.GetComponent<Button>().onClick.AddListener(() => ExplainPhone());
        callButton.onClick.AddListener(OnCallNumberUI);
        messageButton.onClick.AddListener(OnMessageUI);
        calendarButton.onClick.AddListener(OnCalendarUI);
    }

    private void OnEnable()
    {
        CallObject.SetActive(false);
        MessageObject.SetActive(false);
        CalendarObject.SetActive(false);
        timeText.text = GetDayTime();
    }

    private void OnCallNumberUI()
    {
        CallObject.SetActive(true);
        MessageObject.SetActive(false);
        CalendarObject.SetActive(false);
    }

    private void OnMessageUI()
    {
        CallObject.SetActive(false);
        MessageObject.SetActive(true);
        CalendarObject.SetActive(false);
    }

    private void OnCalendarUI()
    {
        CallObject.SetActive(false);
        MessageObject.SetActive(false);
        CalendarObject.SetActive(true);
    }

    public void OffPhone()
    {
        gameObject.SetActive(false);
        if (PopupUIManager.Instance.popupUI.ContainsKey("MessageBubbleUI"))
        {
            Destroy(PopupUIManager.Instance.popupUI["MessageBubbleUI"]);
            PopupUIManager.Instance.popupUI.Remove("MessageBubbleUI");
        }
    }

    private string GetDayTime() //������ �°� ��ȯ
    {
        switch (GameManager.Instance.Playerinformation.daytime)
        {
            case DayTimeenum.Evening:
                return "����";
            case DayTimeenum.Afternoon:
                return "����";
            case DayTimeenum.Night:
                return "����";
        }

        return "";
    }

    private void ExplainPhone()
    {
        if(num == imageTuto.Count) { exTuto.SetActive(false); return; }
        if(num > 0) 
        {
            imageTuto[num - 1].SetActive(false);
        }
        imageTuto[num].SetActive(true);
        num++;
    }
}