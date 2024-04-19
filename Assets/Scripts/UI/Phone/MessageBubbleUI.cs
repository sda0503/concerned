using DataStorage;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageBubbleUI : PopupUIBase
{
    public Image characterImage;
    public TextMeshProUGUI characterText;

    public GameObject bubblePrefab1;
    public GameObject bubblePrefab2;
    public Transform bubbleTransform;

    public void OnSetBubble(string name)
    {
        //���ο� ������Ʈ�� �����Ǿ��ϹǷ�..
        //����Ǿ���ϴ� ���� �з� ��� �ߴ��� Ȯ�� �ʿ�
        characterImage.sprite = DataManager.Instance.SpriteLoad("Image/Phone/" + name);
        characterText.text = name + " ��ȭ";

        foreach(int n in DialogueManager.Instance.allchatlog[name].saveOneLog.Keys)
        {
            foreach (chatlogData chat in DialogueManager.Instance.allchatlog[name].saveOneLog[n])
            {
                if (chat.Name == "��")
                {
                    GameObject obj = Instantiate(bubblePrefab1, bubbleTransform);
                    obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = chat.Log;
                }
                else if (chat.Name == "����") { }
                else
                {
                    GameObject obj = Instantiate(bubblePrefab2, bubbleTransform);
                    obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = chat.Log;
                }
            }
        }
    }

    public void OnDestroy()
    {
        PopupUIManager.Instance.popupUI.Remove("MessageBubbleUI");
        Destroy(gameObject);
    }
}
