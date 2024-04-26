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
                OnSetList(ss);
            }
        }
    }

    private void OnSetList(string name)
    {
        if(!messageListDictionary.ContainsKey(name))
        {
            GameObject obj = Instantiate(messageListPrefab, messageNumberListPosition);
            obj.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => OnSetButton(name));
            messageListDictionary.Add(name, obj);

            OnSetPrefab(name);
        }
    }

    private void OnSetPrefab(string name)
    {
        string view_name = "";
        if (name.Contains("강민우")) view_name = "강민우";
        else if (name.Contains("한미래")) view_name = "한미래";
        else if (name.Contains("흥신소")) view_name = "흥신소 탐정";
        else if (name.Contains("한미래")) view_name = "변호사";
        else if (name.Contains("한미래")) view_name = "김태현";
        else if (name.Contains("한미래")) view_name = "신현우";
        messageListDictionary[name].transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = DataManager.Instance.SpriteLoad("Image/Phone/" + view_name);
        messageListDictionary[name].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = view_name;
    }


    private void OnSetButton(string name)
    {
        PopupUIManager.Instance.OpenPopupUI<MessageBubbleUI>(gameObject.transform).OnSetBubble(name);
    }
}
