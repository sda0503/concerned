using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    //실제 유저가 사용하는 아이템
    //id만 가지고 있고 id를 통해서 아이템의 정보를 불러옴.
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
