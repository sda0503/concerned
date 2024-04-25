using DataStorage;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class PhoneUI : PopupUIBase
{
    public Button callButton;
    public Button messageButton;
    public Button calendarButton;

    public TextMeshProUGUI timeText;

    public GameObject CallObject;
    public GameObject MessageObject;
    public GameObject CalendarObject;

    private void Start()
    {
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

    private string GetDayTime() //정서에 맞게 변환
    {
        switch (GameManager.Instance.Playerinformation.daytime)
        {
            case DayTimeenum.Evening:
                return "오전";
            case DayTimeenum.Afternoon:
                return "오후";
            case DayTimeenum.Night:
                return "저녁";
        }

        return "";
    }
}