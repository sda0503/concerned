using DataStorage;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CallNumberUI : MonoBehaviour
{
    public GameObject callListPrefab;
    public Transform phoneNumberListPosition;
    public Dictionary<string, GameObject> phoneNumberDictionary = new Dictionary<string, GameObject>();

    //번호가 등록되었는지 아닌지 딕셔너리로 저장해서 확인할 것
    private void OnEnable()
    {
        MakeList(700);
        MakeList(701);
        MakeList(800);
        MakeList(801);
        MakeList(10000);
        MakeList(10001);
        MakeList(10002);
        MakeList(10003);
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
            case 700:
            case 701:
                name = "흥신소 탐정";
                obj.GetComponentInChildren<interactableNPC>().TargetName = "흥신소 탐정 핸드폰";
                break;
            case 800:
            case 801:
                name = "변호사";
                obj.GetComponentInChildren<interactableNPC>().TargetName = "변호사 핸드폰";
                break;
            case 10000:
                name = "강민우";
                obj.GetComponentInChildren<interactableNPC>().TargetName = "강민우 핸드폰";
                break;
            case 10001:
                name = "김태현";
                obj.GetComponentInChildren<interactableNPC>().TargetName = "김태현 핸드폰";
                break;
            case 10002:
                name = "신현우";
                obj.GetComponentInChildren<interactableNPC>().TargetName = "신현우 핸드폰";
                break;
            case 10003:
                name = "한미래";
                obj.GetComponentInChildren<interactableNPC>().TargetName = "한미래 핸드폰";
                break;
        }
        obj.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = DataManager.Instance.SpriteLoad("Image/Phone/" + name);
        obj.GetComponentInChildren<TextMeshProUGUI>().text = name;
        obj.GetComponentInChildren<Button>().onClick.AddListener(() => OnCalling(name));
        phoneNumberDictionary.Add(DataManager.Instance.getItems[id].itemData.item_name, obj);
    }

    void OnCalling(string name)
    {
        PopupUIManager.Instance.OpenPopupUI<CallingUI>();
        PopupUIManager.Instance.OpenPopupUI<CallingUI>().OnSet(name);
    }
}
