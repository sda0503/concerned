using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class PhoneUI : PopupUIBase
{
    public Button item1;public Button item2;//юс╫ц
    public Button callButton;
    public Button messageButton;
    public Button calendarButton;

    private void Start()
    {
        item1.onClick.AddListener(() => DataManager.Instance.OnClickToFindItem(1));item2.onClick.AddListener(() => DataManager.Instance.OnClickToFindItem(1000));
        callButton.onClick.AddListener(OnCallNumberUI);
        calendarButton.onClick.AddListener(OnCalendarUI);
    }

    private void OnCallNumberUI()
    {
        PopupUIManager.Instance.OpenPopupUI<CallNumberUI>(gameObject.transform);
    }

    private void OnCalendarUI()
    {
        PopupUIManager.Instance.OpenPopupUI<CalendarUI>(gameObject.transform).SetButton();
    }
}