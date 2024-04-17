using DataStorage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBubbleUI : PopupUIBase
{
    public GameObject bubblePrefab1;
    public GameObject bubblePrefab2;
    public Transform bubbleTransform;

    public void OnSetBubble(string name)
    {
        //���ο� ������Ʈ�� �����Ǿ��ϹǷ�..
        //����Ǿ���ϴ� ���� �з� ��� �ߴ��� Ȯ�� �ʿ�

        foreach (chatlogData chat in DialogueManager.Instance.allchatlog[name].saveOneLog[DialogueManager.questcount])
        {
            if (chat.Name == "��")
            {
                GameObject obj = Instantiate(bubblePrefab1, bubbleTransform);
                obj.transform.GetChild(0).GetComponent<Text>().text = chat.Log; //chat���� �־�α�
            }
            else if (chat.Name == "Player") { }
            else
            {
                GameObject obj = Instantiate(bubblePrefab2, bubbleTransform);
                obj.transform.GetChild(0).GetComponent<Text>().text = "";
            }
        }
    }
}
