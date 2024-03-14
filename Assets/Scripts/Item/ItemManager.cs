using System;
using System.Collections;
using System.Collections.Generic;
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

    public Dictionary<int, Item> getItems = new Dictionary<int, Item>(); //얻은 Item들
    public List<int> getItemsNumber = new List<int>(); //먹은 Item들 순서

    private void Start()
    {
        for(int i = 0; i < items.Length; i++)
        {
            MakeItem(i);
        }
    }

    public void MakeItem(int index)
    {
        items[index] = new Item(index);
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
