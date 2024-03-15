using System.Collections.Generic;
using JetBrains.Annotations;

namespace DataStorage
{
    #region 게임 데이터 저장

    [System.Serializable]
    public class Player
    {
    }

    [System.Serializable]
    public class Dialogue_Save
    {
    }

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
    }

    public enum Log_Type
    {
        normal,
        choose,
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
}