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

    //번호가 등록되었는지 아닌지 딕셔너리로 저장해서 확인할 것
    private void OnEnable()
    {
        
    }

    private void MakeList()
    {
        if (DialogueManager.Instance.allchatlog.Count > 0)
        {
            foreach (string ss in DialogueManager.Instance.allchatlog.Keys)
            {
                if (ss.Contains("강민우")) OnSet("강민우");
                else if(ss.Contains("한미래")) OnSet("한미래");
                else if (ss.Contains("흥신소")) OnSet("흥신소");
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
