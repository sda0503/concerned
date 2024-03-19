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
    [SerializeField] private GameObject _chatLogPosition;
    [SerializeField] private GameObject _chatLogPrefab;
    [SerializeField] private Button _chatLogBtn;
    [SerializeField] private Button _closeBtn;
    [SerializeField] private GameObject _chatlogwindow;

    [SerializeField] private TextMeshProUGUI testTxt;
    [SerializeField] private Button testBtn;

    private StringBuilder sb = new StringBuilder();
    
    
    //가장 큰 형태로 하나로 모든 데이터 저장.
    [HideInInspector] public AllChatLog allChatLog = new AllChatLog();
    private List<GameObject> _PrefabList = new List<GameObject>();

    public static ChatLogManager instance; 
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        _chatLogBtn.onClick.AddListener(ChatLogBtnClick);
        _closeBtn.onClick.AddListener(ChatlogCloseBtnClick);
        //testBtn.onClick.AddListener(test);
    }

    // void test()
    // {
    //     StringBuilder sb = new StringBuilder();
    //     foreach (var VARIABLE in allChatLog.allChatlog.Keys.ToArray())
    //     {
    //         sb.Append(VARIABLE);
    //         sb.Append(", ");
    //     }
    //
    //     testTxt.text = sb.ToString();
    // }
   
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
