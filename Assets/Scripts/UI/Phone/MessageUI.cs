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
    private void Start()
    {
        MakeList();
    }

    private void MakeList()
    {
        OnSetList("��ż� Ž��");
        //if (DialogueManager.Instance.allchatlog.Count > 0)
        //{
        //    foreach (string ss in DialogueManager.Instance.allchatlog.Keys)
        //    {
        //        if (ss.Contains("���ο�")) OnSetList("���ο�");
        //        else if(ss.Contains("�ѹ̷�")) OnSetList("�ѹ̷�");
        //        else if (ss.Contains("��ż�")) OnSetList("��ż�");
        //    }
        //}
    }

    private void OnSetList(string name)
    {
        if(!messageListDictionary.ContainsKey(name))
        {
            GameObject obj = Instantiate(messageListPrefab, messageNumberListPosition);
            obj.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = DataManager.Instance.SpriteLoad("Image/Phone/" + name);
            obj.transform.GetChild(2).GetComponent<Text>().text = name;
            obj.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => OnSetButton(name));
            messageListDictionary.Add(name, obj);
        }
    }

    private void OnSetButton(string name)
    {
        PopupUIManager.Instance.OpenPopupUI<MessageBubbleUI>(gameObject.transform).OnSetBubble(name);
    }
}
