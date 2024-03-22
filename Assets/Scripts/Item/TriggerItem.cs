using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerItem //도감 적용안되는 아이템.
{
    [HideInInspector] public ItemData itemData;
    [HideInInspector] public int id;
    public bool event_check;

    public TriggerItem(int id)
    {
        itemData = ItemDataManager.Instance.GetDefaultItemDataList().Trigger[id];
        this.id = id;
        event_check = false;
    }

    // public void OnClick(InputAction.CallbackContext value)
    // {
    //     //�̹� ã�� ���̸� �������� �������� �ʵ��� ����. id�� ���� ������ ������ ������ �ٸ�.
    //     if (!event_check)
    //     {
    //         Utility.Instance.OnClickToFindItem(10, DataManager.instance.itemCanvas);
    //         event_check = true;
    //     } 
    //     gameObject.SetActive(false);
    // }
}
