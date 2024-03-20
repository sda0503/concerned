using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    public Transform itemCanvas; 

    //test
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;

    public ItemDataList saveGetItems = new ItemDataList();

    private void Awake()
    {
        ItemDataManager.Instance.LoadDefaultData();
        ItemManager.Instance.getItems.Clear();
    }

    private void Start()
    {
        button1.onClick.AddListener(() => Utility.Instance.OnClickToFindTriggerItem(1, itemCanvas));
        button3.onClick.AddListener(() => Utility.Instance.OnClickToFindItem(1, itemCanvas));
        button4.onClick.AddListener(() => Utility.Instance.OnClickToFindItem(5, itemCanvas));
        button5.onClick.AddListener(() => Utility.Instance.OnClickToFindItem(2, itemCanvas));
        button2.onClick.AddListener(() => OffGame());
    }

    private void OffGame()
    {
        Utility.Instance.SaveData(saveGetItems, "SaveItem");
    }
}
