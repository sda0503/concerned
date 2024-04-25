using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class map5 : MonoBehaviour
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
            buttons[i].onClick.AddListener(() => DataManager.Instance.OnClickToFindItem(itemID[n]));
        }
    }
}
