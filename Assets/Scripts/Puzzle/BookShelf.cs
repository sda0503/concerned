using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BookShelf : MonoBehaviour
{
    public Transform books;

    public Button[] triggerButton = new Button[4];

    private bool[] click_button = new bool[4];

    private void Start()
    {
        MakeBook();
        SetTriggerButton();
    }

    private void SetTrigger()
    {
        if (!click_button[0]) 
        { 
            click_button = Enumerable.Repeat(false, click_button.Length).ToArray(); 
            Debug.Log("fail");
            return;
        }
        if (!click_button[1] && (click_button[2] || click_button[3])) 
        { 
            click_button = Enumerable.Repeat(false, click_button.Length).ToArray(); 
            Debug.Log("fail");
            return;
        }
        if (!click_button[2] && click_button[3]) 
        { 
            click_button = Enumerable.Repeat(false, click_button.Length).ToArray(); 
            Debug.Log("fail"); 
            return;
        }
        if (click_button[3])
        {
            Debug.Log("clear");
            ItemManager.Instance.OnClickToFindTriggerItem(2, UIManager.instance.itemCanvas);
        }
    }

    private void SetTriggerButton()
    {
        for (int i = 0; i < triggerButton.Length; i++)
        {
            int n = i;
            triggerButton[i].onClick.AddListener(() => ClickTriggerButton(n));
        }
    }

    private void ClickTriggerButton(int index)
    {
        click_button[index] = true;
        SetTrigger();
    }

    private void MakeBook()
    {
        var obj = DataManager.Instance.GameObjectLoad("Prefabs/book");
        for (int i = 0; i < 32; i++)
        {
            GameObject newBook = Instantiate(obj, books);
            newBook.transform.localPosition = new Vector3(-25 + 60 * (i % 8), 0 - 120 * (i / 8), 0);
        }
    }
}
