using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    //GameManager로 내용이 들어갈 예정
    public Button inventoryButton;
    public Button phoneButton;
    public Button mapButton;

    private void Start()
    {
        inventoryButton.onClick.AddListener(OnInventory);
        phoneButton.onClick.AddListener(OnPhone);
    }

    private void OnPhone()
    {
        PopupUIManager.Instance.OpenPopupUI<PhoneUI>();
    }

    private void OnInventory()
    {
        PopupUIManager.Instance.OpenPopupUI<InventoryUI>();
    }
}
