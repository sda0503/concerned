using DataStorage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneNumberUI : PopupUIBase
{
    public Transform phoneNumberListPosition;
    public Dictionary<string, GameObject> phoneNumberDictionary = new Dictionary<string, GameObject>();

    //DialogueManager allchatlog여기서 받아오기.
    //QuestType == 0;
    //번호가 등록되었는지 아닌지 딕셔너리로 저장해서 확인할 것
    private void OnEnable()
    {
        OnSet();
    }

    void OnSet()
    {
        foreach (string key in DialogueManager.instance.allchatlog.Keys)
        {
            if(!phoneNumberDictionary.ContainsKey(key))
            {
                GameObject obj = Instantiate(Utility.Instance.GameObjectLoad("Prefabs/PhoneNumberList"), phoneNumberListPosition);
                obj.GetComponentInChildren<Text>().text = key;
                obj.GetComponent<interactableNPC>().TargetName = key + "핸드폰";
                phoneNumberDictionary.Add(key, obj);
            }
        }
    }

}
