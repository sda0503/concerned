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

    //번호가 등록되었는지 아닌지 딕셔너리로 저장해서 확인할 것
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
                if (ss.Contains("강민우")) OnSetList("강민우");
                else if (ss.Contains("한미래")) OnSetList("한미래");
                else if (ss.Contains("흥신소")) OnSetList("흥신소 탐정");
            }
        }
    }

    private void OnSetList(string name)
    {
        if(!messageListDictionary.ContainsKey(name))
        {
            GameObject obj = Instantiate(messageListPrefab, messageNumberListPosition);
            obj.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = DataManager.Instance.SpriteLoad("Image/Phone/" + name);
            obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = name;
            obj.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => OnSetButton(name));
            messageListDictionary.Add(name, obj);
        }
    }

    private void OnSetButton(string name)
    {
        PopupUIManager.Instance.OpenPopupUI<MessageBubbleUI>(gameObject.transform).OnSetBubble(name);
    }
}
