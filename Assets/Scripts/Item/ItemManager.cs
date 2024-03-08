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

    public Item[] items; //도감에 들어있는 아이템

    public List<Item> getItems; //얻은 Item..들

    public Action<Item> AddItemInInventory;

    private void Awake()
    {
        getItems.Clear();
    }

    public void GetItem(Item item)
    {
        getItems.Add(item);
        AddItemInInventory?.Invoke(item);
    }

    public void OnItemEvent()
    {

    }
}
