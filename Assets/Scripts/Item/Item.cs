using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //���� ������ ����ϴ� ������
    //id�� ������ �ְ� id�� ���ؼ� �������� ������ �ҷ���.

    [HideInInspector] public ItemData itemData;
    [HideInInspector] public int id;
    public bool event_check;

    public Item(int id)
    {
        itemData = ItemDataManager.Instance.GetItemData(id);
        this.id = id;
        event_check = false;
    }

    //public void OnEvent()
    //{
    //    event_check = true;
    //    if (itemData.itemEventType == ItemEventType.Event)
    //    {

    //    }
    //}
    //ItemData[] dogam = new ItemData[39];
    //dogam[24] = defaultItemDataList.Data[24];
    //Debug.Log(dogam[24].item_name);
}
