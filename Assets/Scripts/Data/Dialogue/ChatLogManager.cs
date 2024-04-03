using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DataStorage;
using TMPro;

public class ChatLogManager : MonoBehaviour
{
    [SerializeField] public GameObject _chatLogPosition;
    [SerializeField] public GameObject _chatLogPrefab;
    [SerializeField] private Button _chatLogBtn;
    [SerializeField] private Button _closeBtn;
    [SerializeField] public GameObject _chatlogwindow;
    [SerializeField] public GameObject _chatlogwindow2;
    [SerializeField] public GameObject _chatLogPosition2;

    [SerializeField] public GameObject allchatlogpannel;
    [SerializeField] public GameObject singlechatlogpannel;
    [SerializeField] public GameObject chatpannelPrefab;
    [SerializeField] public GameObject chatpannelPrefab2;
    

    private StringBuilder sb = new StringBuilder();
    
    
    //가장 큰 형태로 하나로 모든 데이터 저장.
    [HideInInspector] public AllChatLog allChatLog = new AllChatLog();
    private List<GameObject> _PrefabList = new List<GameObject>();
    
    public static ChatLogManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
        // _chatLogBtn.onClick.AddListener(ChatLogBtnClick);
        // _closeBtn.onClick.AddListener(ChatlogCloseBtnClick);
    }
    

    void test()
    {
        allchatlogpannel.SetActive(true);
        List<string> keys = allChatLog.allChatlog.Keys.ToList();
        keys.Sort();
        foreach (string key in keys)
        {
            var obj = Instantiate(chatpannelPrefab, allchatlogpannel.transform);
            if (obj.TryGetComponent(out AllChatLogSet allChatLogSet))
            {
                allChatLogSet.Setting(key);
            }
        }
    }
   
    void ChatLogBtnClick()
    {
        _chatLogBtn.gameObject.SetActive(false);
        _chatlogwindow.SetActive(true);
        foreach (chatlogData chat in allChatLog.allChatlog[DialogueManager.Targetname].saveOneLog[DialogueManager.questcount])
        {
            var obj = Instantiate(_chatLogPrefab, _chatLogPosition.transform);
            if (obj.TryGetComponent(out ChatLogSet chatLogSet))
            {
                sb.Append(chat.Name);
                sb.Append(" : ");
                chatLogSet.LogSetting(sb.ToString(),chat.Log);
                _PrefabList.Add(obj);
            }
            sb.Clear();
        }
    }
    
    void ChatlogCloseBtnClick()
    {
        foreach (GameObject prefabs in _PrefabList)
        {
            Destroy(prefabs);
        }
        _chatlogwindow.SetActive(false);
        _chatLogBtn.gameObject.SetActive(true);
    }

}
