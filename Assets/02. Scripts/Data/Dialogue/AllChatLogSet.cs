using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class AllChatLogSet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button btn;
    void Start()
    {
        btn.onClick.AddListener(Click);
    }

    public void Setting(string name)
    {
        text.text = name;
    }

    void Click()
    {
        ChatLogManager.instance.allchatlogpannel.SetActive(false);
        ChatLogManager.instance.singlechatlogpannel.SetActive(true);
        foreach (var idx in ChatLogManager.instance.allChatLog.allChatlog[text.text].saveOneLog.Keys.ToArray())
        {
            var obj = Instantiate(ChatLogManager.instance.chatpannelPrefab2,
                ChatLogManager.instance.singlechatlogpannel.transform);
            if (obj.TryGetComponent(out SingleChatLogSet singleChatLogSet))
            {
                singleChatLogSet.set(idx,text.text);
            }
        }
    }
}
