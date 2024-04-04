using System.Collections;
using System.Collections.Generic;
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
        PopupUIManager.Instance.OpenPopupUI<PhoneNumberUI>();
    }

    private void OnCalendarUI()
    {
        PopupUIManager.Instance.OpenPopupUI<CalendarUI>().SetButton(false);
    }
}