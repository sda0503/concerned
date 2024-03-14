using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;
    public static ItemManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = typeof(ItemManager).Name;
                instance = obj.AddComponent<ItemManager>();
            }
            return instance;
        }
    }

    public Item[] items = new Item[33]; //도감에 들어있는 아이템
    public TriggerItem[] triggerItems = new TriggerItem[7];

    public Dictionary<int, Item> getItems = new Dictionary<int, Item>(); //얻은 Item들
    public List<int> getItemsNumber = new List<int>(); //먹은 Item들 순서

    private void Start()
    {
        SetItemData();
    }

    public void SetItemData()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new Item(i);
        }
        for (int i = 0; i < triggerItems.Length; i++)
        {
            triggerItems[i] = new TriggerItem(i);
        }
    }

    public void GetItem(int item_id)
    {
        getItems.Add(item_id, items[item_id]);
        getItemsNumber.Add(item_id);
        GameManager.Instance.saveGetItems.Data.Add(items[item_id].itemData);
    }

    public void OnItemEvent()
    {

    }
}
