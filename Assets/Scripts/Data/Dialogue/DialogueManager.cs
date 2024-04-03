using System;
using System.Collections;
using System.Collections.Generic;
using DataStorage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using KoreanTyper;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Button[] choice_btn;
    [SerializeField] private Button _confirmbtn;
    [SerializeField] private Button _skipbtn;
    [SerializeField] private TextMeshProUGUI _chatTxt;
    [SerializeField] private TextMeshProUGUI _nameTxt;
    [SerializeField] private GameObject _chatWindow;
    [SerializeField] private Image _characterImage;
    [SerializeField] private Image _choiceWindow;
    [SerializeField] private Button _ChatlogBtn;
    [SerializeField] private GameObject _forechatObject;
    [SerializeField] private Button chatwindowBtn;

    private WaitForSeconds typerWaitTime = new WaitForSeconds(0.05f);

    private int contextcount = 0;
    public static int questcount = 0;
    public static string Targetname = "";

    [HideInInspector] public Dictionary<string, List<Dialogue_Quest_Data>> _questdic;
    [HideInInspector] public Dictionary<int, Dialogue_Data> _dialogdic;
    private Dictionary<string,chatlogdic> allchatlog;

    public static DialogueManager Instance;

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

    void Start()
    {
        for (int i = 0; i < choice_btn.Length; i++)
        {
            int index = i;
            choice_btn[i].onClick.AddListener(() => btnclick(index));
        }

        //_skipbtn.onClick.AddListener(PloatingAllText);
        chatwindowBtn.onClick.AddListener(ConfirmbtnClick);
        //_confirmbtn.onClick.AddListener(ConfirmbtnClick);
        allchatlog = ChatLogManager.Instance.allChatLog.allChatlog;
        _questdic = DataManager.Instance._questDic.DialogueQuestDic;
        _dialogdic = DataManager.Instance.dic.DialogueDic;
    }


    void GiveItem()
    {
        DataManager.Instance._inventory.inventory.Add(0, new Iteminfo());
    }

    int CheckQuest(string targetname)
    {
        int startpost = 0;
        Dialogue_Quest_Data[] dialogueQuestDatas = _questdic[targetname].ToArray(); //이렇게 캐싱
        for (int i = 0; i < dialogueQuestDatas.Length; i++)
        {
            if (dialogueQuestDatas[i].QuestState == false) //이런 부분들 찢어서 메서드로 만들어야될수도
            {
                if (dialogueQuestDatas[i].NeedQuest == null || _questdic[dialogueQuestDatas[i].NeedQuest[0]]
                        [int.Parse(dialogueQuestDatas[i].NeedQuest[1])].QuestState) //필요한 선행 퀘스트가 완료되었는지
                {
                    if (dialogueQuestDatas[i].NeedItem == null ||
                        DataManager.Instance._inventory.inventory.ContainsKey(
                            int.Parse(dialogueQuestDatas[i].NeedItem))) //Inventory가 Dic라서 containsKey로 찾으면 됨.
                    {
                        //if(_questDic.DialogueQuestDic[targetname][i].QuestType == QuestType.Normal)
                        //_questDic.DialogueQuestDic[targetname][i].QuestState = true; //
                        startpost = dialogueQuestDatas[i].QuestStartPost;
                        questcount = dialogueQuestDatas[i].QuestIdx; //TODO : 퀘스트 카운트와 퀘스트 idx를 엮을 방법을 강구할 것.
                        Targetname = targetname; //이렇게 바꿔도 되긴한데 고민 좀 해봐야될듯.
                        i = dialogueQuestDatas.Length; //i가 count로 만들어서 오버되면 끝나게.    
                    }
                    else
                    {
                        startpost = 50001010;
                        Debug.Log("진행에 필요한 아이템이 없습니다.");
                    }
                }
                else
                {
                    startpost = 50001010;
                    Debug.Log("선행 퀘스트의 완료가 필요합니다.");
                }
            }
        }

        return startpost;
    }

    public void StartDialogue(string targetname)
    {
        contextcount = CheckQuest(targetname);

        if (contextcount == 0)
        {
            Debug.Log("대화가 없습니다.");
        }
        else //대화 가능
        {
            _forechatObject.SetActive(false);
            //_confirmbtn.gameObject.SetActive(true);
            _chatWindow.SetActive(true);
            //btn3.gameObject.SetActive(false);

            if (!allchatlog.ContainsKey(targetname))
            {
                allchatlog.Add(targetname,new chatlogdic());
            }

            //_characterImage.gameObject.SetActive(true);

            if (_questdic[Targetname][questcount - 1].QuestType == QuestType.Normal)
            {
                _ChatlogBtn.gameObject.SetActive(true);
            }
            else
            {
                _ChatlogBtn.gameObject.SetActive(false);
            }
            NormalLog();
        }
    }

    void ConfirmbtnClick()
    {
        if (contextcount != 0)
        {
            Dialogue_Data dialogueData = _dialogdic[contextcount];
            if (dialogueData.Log_Type == Log_Type.normal)
                NormalLog();
            else if (dialogueData.Log_Type == Log_Type.choose ||dialogueData.Log_Type == Log_Type.Loop)
                chooseLog(dialogueData);
        }
        else
        {
            //_confirmbtn.gameObject.SetActive(false);
            //btn3.gameObject.SetActive(true);
            _chatTxt.text = "";
            if (Targetname != "")
            {
                Dialogue_Quest_Data questData = _questdic[Targetname][questcount-1]; //퀘스트 idx와 실제 위치차이 1칸
                if (questData.QuestType == QuestType.Normal)
                {
                    //이전 데이터가 없으면, 최초로 누른게 퀘스트가 필요한 경우면 값이 없음.
                    questData.QuestState = true;
                    Targetname = "";
                    questcount = 0;
                }
                // else if (questData.QuestType == QuestType.Main)
                // {
                //     part++;
                // } //파트별로 퀘스트 나누기.
            }

            _chatWindow.SetActive(false);

            questcount = 0;
            _characterImage.sprite = null;
            _characterImage.gameObject.SetActive(false);
            _forechatObject.SetActive(true);

            //대화가 모두 종료되면 반복되는 대화 지문으로 넘겨야할듯. 이야기가 진행되면 거기서도 빠져나올 수 있어야함.
            //조건을 하나 붙이는게 좋을거같음. 만약 어떤 아이템이 있으면 다음 퀘스트로, 아니면 반복지문으로 가는데 어떤 아이템을 먹으면 시작 주소를 바꿔준다던지
        }
    }
    // 재사용 x 있는거 가져다 쓰기. List<chatlogData> a = new List<chatlogData>();
    void NormalLog()
    {
        // TODO : _characterImage.sprite = Resources.Load<Sprite>($"characters/{_dialogdic[contextcount].Name}");
        chatwindowBtn.onClick.RemoveListener(ConfirmbtnClick);
        chatwindowBtn.onClick.AddListener(PloatingAllText);
        StartCoroutine(LogTyper());
        Savelog();
    }

    void Savelog()
    {
        if (_questdic[Targetname][questcount - 1].QuestType == QuestType.Normal)
        {
            string name = _dialogdic[contextcount].Name;
            string log = _dialogdic[contextcount].Log;
            chatlogData saveSingleLog = new chatlogData();
            saveSingleLog.Name = name;
            saveSingleLog.Log = log;
        
            if (!allchatlog[Targetname].saveOneLog.ContainsKey(questcount))
                allchatlog[Targetname].saveOneLog.Add(questcount,new List<chatlogData>());
            allchatlog[Targetname].saveOneLog[questcount].Add(saveSingleLog);    
        }
    }

    void savelog(int index)
    {
        if (_questdic[Targetname][questcount - 1].QuestType == QuestType.Normal)
        {
            string name = "Player"; //형사 이름으로 변경
            string log = _dialogdic[contextcount].Event_Log[index];
            chatlogData saveSingleLog = new chatlogData();
            saveSingleLog.Name = name;
            saveSingleLog.Log = log;

            if (!allchatlog[Targetname].saveOneLog.ContainsKey(questcount))
                allchatlog[Targetname].saveOneLog.Add(questcount, new List<chatlogData>());
            allchatlog[Targetname].saveOneLog[questcount].Add(saveSingleLog);
        }
    }

    void chooseLog(Dialogue_Data data)
    {
        _confirmbtn.enabled = false;
        _choiceWindow.gameObject.SetActive(true);
        for (int i = 0; i < data.Event_Log.Length; i++)
        {
            if (data.Log_Type == Log_Type.choose||!data.Event_Log_State[i])
            {
                choice_btn[i].gameObject.SetActive(true);
                choice_btn[i].GetComponentInChildren<TextMeshProUGUI>().text = data.Event_Log[i];    
            }
        }
    }

    void btnclick(int index)
    {
        savelog(index);
        if(_dialogdic[contextcount].Log_Type==Log_Type.Loop)
            _dialogdic[contextcount].Event_Log_State[index] = true;
        //TODO : 몇번째 선택지를 골랐는지 저장해서 LOG 확인용으로 남길 것.
        contextcount = int.Parse(_dialogdic[contextcount].Event_Next_Log[index]);
        NormalLog();
        _confirmbtn.enabled = true;
        foreach (Button btns in choice_btn)
        {
            if (btns.isActiveAndEnabled)
            {
                btns.gameObject.SetActive(false);
            }
            //btns.gameObject.activeInHierarchy //게임 오브젝트에서만 하이어라키로 체크 가능.
        }

        _choiceWindow.gameObject.SetActive(false);
        
    }

    IEnumerator LogTyper()
    {
        _chatTxt.text = "";
        _confirmbtn.gameObject.SetActive(false); // = false;
        _skipbtn.gameObject.SetActive(true);
        Dialogue_Data dialogueData = _dialogdic[contextcount];
        _nameTxt.text = dialogueData.Name;

        int strTypingLength = dialogueData.Log.GetTypingLength();
        for (int i = 0; i <= strTypingLength; i++)
        {
            _chatTxt.text = dialogueData.Log.Typing(i);
            yield return typerWaitTime;
        }

        FindNextLog();
        _confirmbtn.gameObject.SetActive(true); // = false;
        _skipbtn.gameObject.SetActive(false);
        chatwindowBtn.onClick.RemoveAllListeners();
        chatwindowBtn.onClick.AddListener(ConfirmbtnClick);
        //_confirmbtn.enabled = true;

        //json 역직렬화할때 int는 값이 비어있으면 오류가난다. string 받아서 tryparse 
        //대화를 시도할 때 파트를 구분해서 대화 하나하나를 클래스화(SO) 해서, 대화에 타입을 주고 선택지가 끝나면 팝업을 띄움.
        //선택을 하는 버튼에 콜백을 줘서 다음 단계로 넘어가게
        //대화를 넘어갈때는 무조건 Queue에 넣음. 분기가 갈라지는 것에 그래프. 선택지가 많으면 큐는 비추천
        //분기 갈라지는 것에 대해선 선택지에 따라 다음 대화로 연결시키는 로직을 짜면 좋을 것 같음
        //갈라지는척만하고 결과는 귀결되는 선택지도 존재 가능.
    }

    private void PloatingAllText()
    {
        StopAllCoroutines();
        _skipbtn.gameObject.SetActive(false);
        Dialogue_Data dialogueData = _dialogdic[contextcount];
        _chatTxt.text = dialogueData.Log;
        FindNextLog();
        _confirmbtn.gameObject.SetActive(true); // = false;
        chatwindowBtn.onClick.RemoveAllListeners();
        chatwindowBtn.onClick.AddListener(ConfirmbtnClick);
    }

    private void FindNextLog()
    {
        contextcount = _dialogdic[contextcount].Next_Log;
    }
}