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
        //새로운 오브젝트가 생성되야하므로..
        //저장되어야하는 내용 분류 어떻게 했는지 확인 필요
        characterImage.sprite = DataManager.Instance.SpriteLoad("Image/Phone/" + name);
        characterText.text = name + " 대화";

        foreach(int n in DialogueManager.Instance.allchatlog[name].saveOneLog.Keys)
        {
            foreach (chatlogData chat in DialogueManager.Instance.allchatlog[name].saveOneLog[n])
            {
                if (chat.Name == "나")
                {
                    GameObject obj = Instantiate(bubblePrefab1, bubbleTransform);
                    obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = chat.Log;
                }
                else if (chat.Name == "선택") { }
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
