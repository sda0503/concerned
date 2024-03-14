using System.Collections;
using DataStorage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using KoreanTyper;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private Button btn1; //선택지1
    [SerializeField] private Button btn2; //선택지2
    [SerializeField] private Button btn3; //npc a
    [SerializeField] private Button btn4; //npc b
    [SerializeField] private Button btn5; //Save Btn
    [SerializeField] private Button btn6; //giveitem Btn
    [SerializeField] private Button confirmbtn;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private WaitForSeconds typerWaitTime = new WaitForSeconds(0.05f);
    
    private int contextcount = 0;
    private int questcount = 0;
    private string Targetname = "";
   
   void Start()
    {
        btn1.onClick.AddListener(btn1click);
        btn2.onClick.AddListener(btn2click);
        btn3.onClick.AddListener(()=> StartDialogue(btn3.GetComponentInChildren<TextMeshProUGUI>().text));
        btn4.onClick.AddListener(()=> StartDialogue(btn4.GetComponentInChildren<TextMeshProUGUI>().text));
        btn5.onClick.AddListener(DataManager.instance.SaveData);
        btn6.onClick.AddListener(GiveItem);
        confirmbtn.onClick.AddListener(ConfirmbtnClick);
    }

    void GiveItem()
    {
        DataManager.instance._inventory.inventory.Add(0,new Iteminfo());
    }
    
    int CheckQuest(string targetname)
    {
        int startpost = 0;
        Dialogue_Quest_Data[] dialogueQuestDatas = DataManager.instance._questDic.DialogueQuestDic[targetname].ToArray(); //이렇게 캐싱
        for (int i = 0; i < dialogueQuestDatas.Length; i++)
        {
            if (dialogueQuestDatas[i].QuestState == false) //이런 부분들 찢어서 메서드로 만들어야될수도
            {
                if (dialogueQuestDatas[i].NeedQuest.Length == 0 || DataManager.instance._questDic.DialogueQuestDic[dialogueQuestDatas[i].NeedQuest[0]]
                        [int.Parse(dialogueQuestDatas[i].NeedQuest[1])].QuestState)//필요한 선행 퀘스트가 완료되었는지
                {
                    if (dialogueQuestDatas[i].NeedItem == null || DataManager.instance._inventory.inventory.ContainsKey(int.Parse(dialogueQuestDatas[i].NeedItem)))
                    {
                        //if(_questDic.DialogueQuestDic[targetname][i].QuestType == QuestType.Normal)
                        //_questDic.DialogueQuestDic[targetname][i].QuestState = true; //
                        startpost = dialogueQuestDatas[i].QuestStartPost;
                        questcount = i;
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
    
    void StartDialogue(string targetname)
    {
        contextcount = CheckQuest(targetname);
        if (contextcount == 0)
        {
            Debug.Log("대화가 없습니다.");
        }
        else //대화 가능
        {
            confirmbtn.gameObject.SetActive(true);
            btn3.gameObject.SetActive(false);
            NormalLog();    
        }
    }

    void ConfirmbtnClick()
    {
        if (contextcount != 0)
        {
            Dialogue_Data dialogueData = DataManager.instance.dic.DialogueDic[contextcount];
            if (dialogueData.Log_Type == Log_Type.normal)
                NormalLog();
            else if (dialogueData.Log_Type == Log_Type.choose)
                chooseLog(dialogueData);
        }
        else
        {
            
            confirmbtn.gameObject.SetActive(false);
            btn3.gameObject.SetActive(true);
            _textMeshProUGUI.text = "";
            if (Targetname != "")
            {
                Dialogue_Quest_Data questData=DataManager.instance._questDic.DialogueQuestDic[Targetname][questcount];
                if (questData.QuestType == QuestType.Normal)
                { //이전 데이터가 없으면, 최초로 누른게 퀘스트가 필요한 경우면 값이 없음.
                    questData.QuestState = true;
                    Targetname = "";
                    questcount = 0;
                }    
                // else if (questData.QuestType == QuestType.Main)
                // {
                //     part++;
                // } //파트별로 퀘스트 나누기.
            }
        
            //대화가 모두 종료되면 반복되는 대화 지문으로 넘겨야할듯. 이야기가 진행되면 거기서도 빠져나올 수 있어야함.
            //조건을 하나 붙이는게 좋을거같음. 만약 어떤 아이템이 있으면 다음 퀘스트로, 아니면 반복지문으로 가는데 어떤 아이템을 먹으면 시작 주소를 바꿔준다던지
        }
    }

    void NormalLog()
    {
        StartCoroutine(LogTyper());
    }

    void chooseLog(Dialogue_Data data)
    {
        confirmbtn.enabled = false;
        btn1.gameObject.SetActive(true);
        btn2.gameObject.SetActive(true);
        btn1.GetComponentInChildren<TextMeshProUGUI>().text = data.Event_Log_1;
        btn2.GetComponentInChildren<TextMeshProUGUI>().text = data.Event_Log_2;
    }

    void btn1click()
    {
        contextcount = int.Parse(DataManager.instance.dic.DialogueDic[contextcount].Event_1_name);
        NormalLog();
        confirmbtn.enabled = true;
        btn1.gameObject.SetActive(false);
        btn2.gameObject.SetActive(false);
    }

    void btn2click()
    {
        contextcount = int.Parse(DataManager.instance.dic.DialogueDic[contextcount].Event_2_name);
        NormalLog();

        confirmbtn.enabled = true;
        btn1.gameObject.SetActive(false);
        btn2.gameObject.SetActive(false);
    }

    IEnumerator LogTyper()
    {
        _textMeshProUGUI.text = "";
        btn1.enabled = false;
        btn2.enabled = false;
        confirmbtn.enabled = false;
        Dialogue_Data dialogueData = DataManager.instance.dic.DialogueDic[contextcount];

        int strTypingLength = dialogueData.Log.GetTypingLength();
        for (int i = 0; i <= strTypingLength; i++)
        {
            _textMeshProUGUI.text = dialogueData.Log.Typing(i);
            yield return typerWaitTime;
        }

        contextcount = dialogueData.Next_Log;
       
        btn1.enabled = true;
        btn2.enabled = true;
        confirmbtn.enabled = true;
        
        //json 역직렬화할때 int는 값이 비어있으면 오류가난다. string 받아서 tryparse 
        //대화를 시도할 때 파트를 구분해서 대화 하나하나를 클래스화(SO) 해서, 대화에 타입을 주고 선택지가 끝나면 팝업을 띄움.
        //선택을 하는 버튼에 콜백을 줘서 다음 단계로 넘어가게
        //대화를 넘어갈때는 무조건 Queue에 넣음. 분기가 갈라지는 것에 그래프. 선택지가 많으면 큐는 비추천
        //분기 갈라지는 것에 대해선 선택지에 따라 다음 대화로 연결시키는 로직을 짜면 좋을 것 같음
        //갈라지는척만하고 결과는 귀결되는 선택지도 존재 가능.
    }
}