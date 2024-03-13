using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //test
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;

    ItemDataList saveGetItems = new ItemDataList();

    private void Awake()
    {
        ItemDataManager.Instance.LoadDefaultData();
        ItemManager.Instance.getItems.Clear();
    }

    private void Start()
    {
        button1.onClick.AddListener(() => Utility.Instance.OnClickToFindItem(10));
        button3.onClick.AddListener(() => Utility.Instance.OnClickToFindItem(1));
        button4.onClick.AddListener(() => Utility.Instance.OnClickToFindItem(5));
        button5.onClick.AddListener(() => Utility.Instance.OnClickToFindItem(2));
        button2.onClick.AddListener(() => OffGame());
    }

    private void OffGame()
    {
        for (int i = 0; i < ItemManager.Instance.getItems.Count; i++)
        {
            saveGetItems.Data.Add(ItemManager.Instance.getItems[i].itemData);
        }
        Utility.Instance.SaveData(saveGetItems, "Save");
    }
}
