using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : MonoBehaviour
{
    public GameObject messageListPrefab;
    public Transform messageNumberListPosition;
    public Dictionary<string, GameObject> messageListDictionary = new Dictionary<string, GameObject>();

    //��ȣ�� ��ϵǾ����� �ƴ��� ��ųʸ��� �����ؼ� Ȯ���� ��
    private void OnEnable()
    {
        MakeList();
    }

    private void MakeList()
    {
        if (DialogueManager.Instance.allchatlog.Count > 0)
        {
            foreach (string ss in DialogueManager.Instance.allchatlog.Keys)
            {
                OnSetList(ss);
            }
        }
    }

    private void OnSetList(string name)
    {
        if (name.Contains("���ο�") || name.Contains("�ѹ̷�") || name.Contains("��ż�") || name.Contains("��ȣ��") || name.Contains("������") || name.Contains("������"))
        {
            if (!messageListDictionary.ContainsKey(name))
            {
                GameObject obj = Instantiate(messageListPrefab, messageNumberListPosition);
                messageListDictionary.Add(name, obj);

                OnSetPrefab(name);
            }
        }
    }

    private void OnSetPrefab(string name)
    {
        string view_name = "";
        if (name.Contains("���ο�")) view_name = "���ο�";
        else if (name.Contains("�ѹ̷�")) view_name = "�ѹ̷�";
        else if (name.Contains("��ż�")) view_name = "��ż� Ž��";
        else if (name.Contains("��ȣ��")) view_name = "��ȣ��";
        else if (name.Contains("������")) view_name = "������";
        else if (name.Contains("������")) view_name = "������";
        messageListDictionary[name].transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = DataManager.Instance.SpriteLoad("Image/Phone/" + view_name);
        messageListDictionary[name].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = view_name;
        messageListDictionary[name].transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => OnSetButton(name, view_name));
    }


    private void OnSetButton(string chat_name, string image_name)
    {
        PopupUIManager.Instance.OpenPopupUI<MessageBubbleUI>(gameObject.transform).OnSetBubble(chat_name, image_name);
    }
}
