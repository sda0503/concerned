using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerItem : MonoBehaviour
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

    public void OnClick(InputAction.CallbackContext value)
    {
        if (!event_check)
        {
            switch (id)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    Utility.Instance.OnClickToFindItem(10, GameManager.Instance.itemCanvas);
                    break;
                case 6:
                    break;
                case 7:
                    Utility.Instance.OnClickToFindItem(1, GameManager.Instance.itemCanvas);
                    break;
                case 8:
                    break;
                case 9:
                    break;
            }
            event_check = true;
        } 
        gameObject.SetActive(false);
    }
}
