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
        button2.onClick.AddListener(() => OffGame());
    }

    private void OffGame()
    {
        for (int i = 0; i < ItemManager.Instance.getItems.Count; i++)
        {
            saveGetItems.Data.Add(ItemManager.Instance.getItems[i].itemData);
        }
        ItemDataManager.Instance.SaveData(saveGetItems, "Save");
    }
}
