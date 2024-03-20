using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneUI : PopupUIBase
{
    public Button callButton;
    public Button messageButton;
    public Button cameraButton;

    private void Start()
    {
        callButton.onClick.AddListener(OnCallNumberUI);
    }

    private void OnCallNumberUI()
    {
        PopupUIManager.Instance.OpenPopupUI<PhoneNumberUI>();
    }
}