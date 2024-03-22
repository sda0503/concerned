using System;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Item
{
    //���� ������ ����ϴ� ������
    //id�� ������ �ְ� id�� ���ؼ� �������� ������ �ҷ���.
  

    public int id;
    public ItemData itemData;
    
    public bool event_check =false;

    public Item(int id)
    {
        itemData = ItemDataManager.Instance.GetDefaultItemDataList().Data[id];
        this.id = id;
        event_check = false;
    }
}
