using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapItemSetting : MonoBehaviour
{
    public List<Button> buttons;
    public List<int> itemID;

    private void Start()
    {
        SetButtonKey();
    }

    private void SetButtonKey()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            int n = i;
            buttons[i].onClick.AddListener(() => SetItem(itemID[n], n));
        }
    }

    private void SetItem(int id, int item_index)
    {
        if (id == 1)
        {
            if (DataManager.Instance.getItems.ContainsKey(301))
            {
                DataManager.Instance.OnClickToFindItem(id);
                PopupUIManager.Instance.popupUI["InventoryUI"].GetComponent<InventoryUI>().items[31].itemData.default_description += " 사용되었다.";
                buttons[item_index].gameObject.SetActive(false);
            }
        }
        else if (id == 501)
        {
            if (DataManager.Instance.getItems.ContainsKey(302))
            {
                DataManager.Instance.OnClickToFindItem(id);
                PopupUIManager.Instance.popupUI["InventoryUI"].GetComponent<InventoryUI>().items[32].itemData.default_description += " 사용되었다.";
                buttons[item_index].gameObject.SetActive(false);
            }
        }
        else
        {
            DataManager.Instance.OnClickToFindItem(id);
            buttons[item_index].gameObject.SetActive(false);
        }
    }
}
