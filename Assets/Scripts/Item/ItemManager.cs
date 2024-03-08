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

    public Item[] items; //������ ����ִ� ������

    public List<Item> getItems; //���� Item..��

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
