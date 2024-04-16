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

    private void Start()
    {
        callButton.onClick.AddListener(OnCallNumberUI);
        calendarButton.onClick.AddListener(OnCalendarUI);
    }

    private void OnCallNumberUI()
    {
        PopupUIManager.Instance.OpenPopupUI<CallNumberUI>(gameObject.transform);
    }

    private void OnCalendarUI()
    {
        PopupUIManager.Instance.OpenPopupUI<CalendarUI>(gameObject.transform);
    }
}