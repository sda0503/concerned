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
        //새로운 오브젝트가 생성되야하므로..
        //저장되어야하는 내용 분류 어떻게 했는지 확인 필요

        foreach (chatlogData chat in DialogueManager.Instance.allchatlog[name].saveOneLog[DialogueManager.questcount])
        {
            if (chat.Name == "나")
            {
                GameObject obj = Instantiate(bubblePrefab1, bubbleTransform);
                obj.transform.GetChild(0).GetComponent<Text>().text = chat.Log; //chat내용 넣어두기
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
