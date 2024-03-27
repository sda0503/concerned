using DataStorage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneNumberUI : PopupUIBase
{
    public Transform phoneNumberListPosition;
    public Dictionary<string, GameObject> phoneNumberDictionary = new Dictionary<string, GameObject>();

    //DialogueManager allchatlog���⼭ �޾ƿ���.
    //QuestType == 0;
    //��ȣ�� ��ϵǾ����� �ƴ��� ��ųʸ��� �����ؼ� Ȯ���� ��
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
                obj.GetComponent<interactableNPC>().TargetName = key + "�ڵ���";
                phoneNumberDictionary.Add(key, obj);
            }
        }
    }

}
