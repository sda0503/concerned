using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PopupUIBase : MonoBehaviour
{
    protected void OnSet(int a, int b, Transform t)
    {
        Dictionary<int, Item> dd = ItemManager.Instance.getItems.Where(x => x.Key >= a && x.Key < b).ToDictionary(x => x.Key, x => x.Value);
        if (dd.Count > 0)
        {
            foreach (var item in dd)
            {
                //신원확인 가능하도록 세팅
                GameObject obj = Instantiate(DataManager.Instance.GameObjectLoad("Prefabs/PhoneNumberList"), t);
                obj.GetComponentInChildren<Text>().text = ItemManager.Instance.getItems[1].itemData.item_name;
                obj.GetComponent<interactableNPC>().TargetName = ItemManager.Instance.getItems[1].itemData.item_name;
            }
        }
        dd.Clear();
    }

    protected void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
