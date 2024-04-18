using DataStorage;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CallNumberUI : PopupUIBase
{
    public GameObject callListPrefab;
    public Transform phoneNumberListPosition;
    public Dictionary<string, GameObject> phoneNumberDictionary = new Dictionary<string, GameObject>();

    //번호가 등록되었는지 아닌지 딕셔너리로 저장해서 확인할 것
    private void OnEnable()
    {
        MakeList(1);
        MakeList(112);
    }

    private void MakeList(int item_id)
    {
        if (DataManager.Instance.getItems.ContainsKey(item_id)&& !phoneNumberDictionary.ContainsKey(DataManager.Instance.getItems[item_id].itemData.item_name))
        {
            OnSet(item_id);
        }
    }

    private void OnSet(int id)
    {
        string name = "";
        GameObject obj = Instantiate(callListPrefab, phoneNumberListPosition);
        switch (id) 
        {
            case 1://111
            case 112:
                name = "흥신소 탐정";
                obj.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = DataManager.Instance.SpriteLoad("Image/Phone/"+name);
                obj.GetComponentInChildren<Text>().text = name;
                obj.GetComponentInChildren<interactableNPC>().TargetName = "흥신소 탐정 핸드폰";
                break;
            case 121:
            case 122:
                obj.GetComponentInChildren<Text>().text = "변호사";
                obj.GetComponentInChildren<interactableNPC>().TargetName = "변호사 핸드폰";
                break;
        }
        obj.GetComponentInChildren<Button>().onClick.AddListener(() => OnCalling(name));
        phoneNumberDictionary.Add(DataManager.Instance.getItems[id].itemData.item_name, obj);
    }

    void OnCalling(string name)
    {
        PopupUIManager.Instance.OpenPopupUI<CallingUI>();
        PopupUIManager.Instance.OpenPopupUI<CallingUI>().OnSet(name);
    }
}
