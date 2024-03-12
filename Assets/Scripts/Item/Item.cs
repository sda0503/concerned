using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    //���� ������ ����ϴ� ������
    //id�� ������ �ְ� id�� ���ؼ� �������� ������ �ҷ���.
    public Animator animator;

    [HideInInspector] public ItemData itemData;
    [HideInInspector] public int id;
    public bool event_check;

    public Item(int id)
    {
        itemData = ItemDataManager.Instance.GetItemDataList().Data[id];
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

    public void OnClick(InputAction.CallbackContext value)
    {
        animator.SetTrigger("OnIn");
    }
}
