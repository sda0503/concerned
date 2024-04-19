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

    private void OnEnable()
    {
        CallObject.SetActive(false);
        MessageObject.SetActive(false);
        CalendarObject.SetActive(false);
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
}