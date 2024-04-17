using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : PopupUIBase
{
    public GameObject messageListPrefab;
    public Transform messageNumberListPosition;
    public Button messageButton;
    public Dictionary<string, GameObject> messageListDictionary = new Dictionary<string, GameObject>();

    //��ȣ�� ��ϵǾ����� �ƴ��� ��ųʸ��� �����ؼ� Ȯ���� ��
    private void OnEnable()
    {
        
    }

    private void MakeList()
    {
        if (DialogueManager.Instance.allchatlog.Count > 0)
        {
            foreach (string ss in DialogueManager.Instance.allchatlog.Keys)
            {
                if (ss.Contains("���ο�")) OnSet("���ο�");
                else if(ss.Contains("�ѹ̷�")) OnSet("�ѹ̷�");
                else if (ss.Contains("��ż�")) OnSet("��ż�");
            }
        }
    }

    private void OnSet(string name)
    {
        if(!messageListDictionary.ContainsKey(name))
        {
            GameObject obj = Instantiate(messageListPrefab, messageNumberListPosition);
            obj.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = DataManager.Instance.SpriteLoad("Image/Phone/" + name);
            obj.transform.GetChild(2).GetComponent<Text>().text = name;
            messageListDictionary.Add(name, obj);
        }
    }
}
