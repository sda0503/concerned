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

    public Button[] triggerButton = new Button[5];

    private bool[] click_button = new bool[5];

    public int clear;

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
            GameManager.Instance.PuzzleCount--;
            Debug.Log("fail");
            return;
        }
        if (!click_button[1] && (click_button[2] || click_button[3] || click_button[4])) 
        { 
            click_button = Enumerable.Repeat(false, click_button.Length).ToArray();
            GameManager.Instance.PuzzleCount--;
            Debug.Log("fail");
            return;
        }
        if (!click_button[2] && (click_button[3] || click_button[4]))
        { 
            click_button = Enumerable.Repeat(false, click_button.Length).ToArray();
            GameManager.Instance.PuzzleCount--;
            Debug.Log("fail"); 
            return;
        }
        if (!click_button[3] && click_button[4])
        {
            click_button = Enumerable.Repeat(false, click_button.Length).ToArray();
            GameManager.Instance.PuzzleCount--;
            Debug.Log("fail");
            return;
        }
        if (click_button[4]) 
        { 
            Debug.Log("clear");
            DataManager.Instance.GetItem(clear);
            gameObject.SetActive(false);
            UIManager.Instance.OnGUI();
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
        for (int i = 0; i < 40; i++)
        {
            Instantiate(obj, new Vector3(783 + 51 * (i % 8), 800 - 126 * (i / 8), 0), Quaternion.identity, books);
        }
    }
}
