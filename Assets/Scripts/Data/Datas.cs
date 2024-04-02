using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace DataStorage
{
    #region 게임 데이터 저장

    [System.Serializable]
    public class Player
    {
        //public AllChatLog AllChatLog; //대화 기록
        //public Dialogue_Dic DialogueDic; //대화 dic
        public Dialogue_Quest_Dic DialogueQuestDic; //대화 Quest dic
        public List<int> Inventory; //인벤토리
        public Information Information; //캐릭터 진행 데이터
        
    }

    #endregion

    #region 캐릭터 info 데이터

    public class Information
    {
        public enum DayTimeenum
        {
            Evening,
            Afternoon,
            Night
        }

        private DayTimeenum DayTime;

        public DayTimeenum dayTime
        {
            get { return DayTime; }
            set { DayTime = value; }
        }

        private int Date;

        public int date
        {
            get { return Date; }
            set { Date = value; }
        }

        private int Position;

        public int position
        {
            get { return Position; }
            set { Position = value; }
        }
        //position은 주소로 하고 DB 만들기

        public Dictionary<int, List<bool>> canvasSettingData = new Dictionary<int, List<bool>>(); //캔버스 오브젝트 배치에 관한 데이터.
        

        public Information() //첫 시작 때 사용
        {
            DayTime = DayTimeenum.Evening;
            Date = 0;
            //Position = "집 주소"; DB상 주소를 얘기함. 집주소 아니면 사건현장?
        }

        public void OnLoadSetting() //로드할 때에 맞춰서 세팅
        {
            dayTime = DataManager.instance.player.Information.dayTime;
            date = DataManager.instance.player.Information.date;
            position = DataManager.instance.player.Information.position;
        }
    } //TODO : 최초 세팅 때 설정해주고, 이어하기 할 때는 로드하기.

        #endregion

    #region Dialogue 역직렬화

    [System.Serializable]
    public class Dialogue_Data
    {
        public int Dialogue_idx;
        public string Name;
        public string Log;
        public Log_Type Log_Type;
        public int Next_Log;
        public string[] Event_Log;
        public string[] Event_Next_Log;
        public bool[] Event_Log_State;
    }

    public enum Log_Type
    {
        normal,
        choose,
        Loop
    }

    public enum QuestType
    {
        Normal,
        Loop,
    }

    public class Dialogue_List
    {
        public List<Dialogue_Data> Dialouge_Log_Data;
        public List<Dialogue_Quest_Data> Dialogue_Quest_Data;
    }

    public class Dialogue_Dic
    {
        public Dictionary<int, Dialogue_Data> DialogueDic = new Dictionary<int, Dialogue_Data>();
    }

    [System.Serializable]
    public class Dialogue_Quest_Data
    {
        public string QuestTargetName;
        public int QuestIdx;
        public int QuestStartPost;
        public QuestType QuestType;
        public bool QuestState;
        [CanBeNull] public string[] NeedQuest;
        public string NeedItem;
    }

    [System.Serializable] //Serializable =>직렬화 / 직렬화하려면 달아주어야함. 
    public class Dialogue_Quest_Dic //퀘스트 딕셔너리
    {
      public Dictionary<string, List<Dialogue_Quest_Data>> DialogueQuestDic =
            new Dictionary<string, List<Dialogue_Quest_Data>>();
    }

    public class Iteminfo
    {
        public int Item_id;
        public int Item_name;
    }

    public class Inventory
    {
        public Dictionary<int, Iteminfo> inventory = new Dictionary<int, Iteminfo>();
    }

    #endregion

    #region ChatLogSave
    //굳이 Save&Load를 해야하는가?
    public class AllChatLog //전체 대화 로그
    { 
        //Dictionary<int, chatlogData> => int = Quest 순서, ChatlogData = 퀘스트 안의 대화 내역
        public Dictionary<string,chatlogdic> allChatlog = new
            Dictionary<string, chatlogdic>();
    }
    public class chatlogData //대화 하나 저장
    {
        public string Name;
        public string Log;
    }
    
    public class chatlogdic //한 대화의 전부
    {
        public Dictionary<int,List<chatlogData>> saveOneLog = new Dictionary<int, List<chatlogData>>();
    }

    #endregion

    #region canvas 오브젝트 배치 저장용

    public class CanvasDic
    {
        public Dictionary<int, List<bool>> CanvasContorllers = new Dictionary<int, List<bool>>();
    }
    
    #endregion

    #region PlaceDB

    public class PlaceDB
    {
       public int Place_ID; //Index
       public string Place_Name; // 이름
       public string Place_Path; // Resources.Load에 사용할 주소값
    }

    public class PlaceDBDatas
    {
        public List<PlaceDB> PlaceDB = new List<PlaceDB>();
    }

    #endregion

    #region 데이터 세팅용

    public class PlaceDBLoad
    {
        public Dictionary<int, GameObject> placeDic = new Dictionary<int, GameObject>();
        public Dictionary<int, Sprite> BGDic = new Dictionary<int, Sprite>();
    }

    #endregion

    #region 아이템 데이터

    [System.Serializable]
    public class ItemData
    {
        public int item_id;
        public string item_name;
        public string default_description;
        public string simple_description;
        public ItemEventType itemEventType;
        public ItemType itemType;
        public string event_description;
    }
    
    public class ItemDataList
    {
        //<������ �޾ƿ� class> �������� ��Ʈ�� �ִ� ������ �ִ� ��Ʈ��.
        public List<ItemData> Data = new List<ItemData>();
        public List<ItemData> Trigger = new List<ItemData>();
    }


    #endregion
}