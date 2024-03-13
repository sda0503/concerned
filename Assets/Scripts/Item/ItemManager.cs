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

    public Item[] items = new Item[40]; //������ ����ִ� ������

    public Dictionary<int, Item> getItems = new Dictionary<int, Item>(); //���� Item..��

    public Action<Item> AddItemInInventory;

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
        AddItemInInventory?.Invoke(items[item_id]);
    }

    public void OnItemEvent()
    {

    }
}
