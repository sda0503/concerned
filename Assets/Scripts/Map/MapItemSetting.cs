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
            buttons[i].onClick.AddListener(() => SetItem(itemID[n], i));
        }
    }

    private void SetItem(int id, int item_index)
    {
        if (id == 51)
        {
            if (DataManager.Instance.getItems.ContainsKey(31))
            {
                DataManager.Instance.OnClickToFindItem(id);
                buttons[item_index].gameObject.SetActive(false);
            }
        }
        else if (id == 1)
        {
            if (DataManager.Instance.getItems.ContainsKey(32))
            {
                DataManager.Instance.OnClickToFindItem(id);
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
