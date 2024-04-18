using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class PhoneUI : PopupUIBase
{
    public Button callButton;
    public Button messageButton;
    public Button calendarButton;

    public GameObject CallObject;
    public GameObject MessageObject;
    public GameObject CalendarObject;

    private void Start()
    {
        callButton.onClick.AddListener(OnCallNumberUI);
        messageButton.onClick.AddListener(OnMessageUI);
        calendarButton.onClick.AddListener(OnCalendarUI);
    }

    private void OnCallNumberUI()
    {
        CallObject.SetActive(true);
    }

    private void OnMessageUI()
    {
        MessageObject.SetActive(true);
    }

    private void OnCalendarUI()
    {
        CalendarObject.SetActive(true);
    }
}