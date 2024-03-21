using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStorage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleChatLogSet : MonoBehaviour
{
    public List<chatlogData> ChatlogDatas;

    [SerializeField] private Button btn;

    [SerializeField] private TextMeshProUGUI text;
    //[SerializeField] private GameObject 

    private void Start()
    {
        btn.onClick.AddListener(onclick);
    }


    public void set(int idx,string name)
    {
        ChatlogDatas = ChatLogManager.instance.allChatLog.allChatlog[name].saveOneLog[idx];
        text.text = $"{name} {idx}";
       
    }

    void onclick()
    {
        ChatLogManager.instance._chatlogwindow2.SetActive(true);
        
        foreach (chatlogData chat in ChatlogDatas)
        {
            var obj = Instantiate(ChatLogManager.instance._chatLogPrefab, ChatLogManager.instance._chatLogPosition2.transform);
            if (obj.TryGetComponent(out ChatLogSet chatLogSet))
            {
                string a = chat.Name + " : ";   
                chatLogSet.LogSetting(a,chat.Log);
            }
        }
    }
    
    
}