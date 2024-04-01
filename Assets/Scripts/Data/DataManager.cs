using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DataStorage;
using Newtonsoft.Json;
using UnityEngine;

//로딩이 얼마 안걸릴거같아도 확장성을 생각하면 코루틴이 좋고
// 설계를 할 때 큰 그림을 그리고 설계를 하면 기술적 고민을 많이 해볼 수 있음.(취준생입장)
//TODO : SingletonBase를 하나 만들어서 Init하는 방식으로 기동 순서를 정해주어야함.

public class DataManager : MonoBehaviour//유니티 기능을 상속 받는거 /코루틴이나 유니티 이벤트를 연동하려면 필요함.
{
    private Player _playerToSave = new Player();//Save & Load 대기용
    
    #region 게임에서 사용할 데이터

    public Information Playerinformation = new Information(); //TODO : 일단은 New
    
    public Dialogue_Dic dic = new Dialogue_Dic();
    public Dialogue_Quest_Dic _questDic = new Dialogue_Quest_Dic();
    
    public Inventory _inventory = new Inventory();
    private ItemDataList defaultItemDataList = new ItemDataList();
    private ItemDataList itemDataList = new ItemDataList();
    private ItemDataList dogamItemDataList = new ItemDataList();
    public ItemDataList saveGetItems = new ItemDataList();
    
    private ItemDataList saveItemDataList= new ItemDataList();
    public Dictionary<int, ItemData> dogamItemData = new Dictionary<int, ItemData>();

    public PlaceDBDatas PlaceDBDatas;
    public PlaceDBLoad PlaceDBLoad = new PlaceDBLoad();

    #endregion

#if UNITY_EDITOR
    string path;

    [SerializeField]public Transform asdf;

    
#endif
    
    private StringBuilder sb = new StringBuilder();
    public static DataManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else 
            Destroy(gameObject);
        
        LoadDefaultData();
        ItemManager.Instance.getItems.Clear();
    }

    void Start()
    {
        path = Application.persistentDataPath;
        path = sb.Append("/concerned.json").ToString();
        sb.Clear();
        StartCoroutine(SetDatas());
    }
   
    /// <summary>
    /// 전체 데이터 세팅
    /// </summary>
    /// <returns></returns>
    IEnumerator SetDatas()
    {
        //TODO : DataSet에 관한 개별 메서드 만들기 + 불러오기 했을 때 세팅되는 부분까지 작성하기.
        //전체적으로 다 코루틴으로 꾸려주어야함.
        yield return StartCoroutine(dialogueDBSet()); //이렇게 하면 끝날때까지 기다림.
        Debug.Log("다이얼로그 세팅 완료");

        yield return StartCoroutine(PlaceDBSet());        
        Debug.Log("장소 세팅 완료");
        
        yield return StartCoroutine(SetItemData());
        GameManager.Instance.Playerinformation = Playerinformation; 
        //TODO : 로드하고 난 이후 적용되어야 할 부분들 수정
    }

    #region dialogueData
   
    IEnumerator dialogueDBSet() //TODO : 로드에 관한 부분도 넣어서 작성해야됨 => 엎을 예정
    {
        yield return new WaitForSeconds(1f);
        string loadData = Resources.Load("Dialogue_DB").ToString();
        
        Dialogue_List dialogueList = JsonConvert.DeserializeObject<Dialogue_List>(loadData);
        
        dic.DialogueDic.Clear(); //public 일 때는 클리어 한번 해주는게 좋음.
        
        for (int i = 0; i < dialogueList.Dialouge_Log_Data.Count; i++)
        {
            dic.DialogueDic.Add(dialogueList.Dialouge_Log_Data[i].Dialogue_idx, dialogueList.Dialouge_Log_Data[i]);
        }

        if (File.Exists(path + "/save.json"))
        {
            var c = File.ReadAllText(path + "/save.json");
            _questDic = JsonConvert.DeserializeObject<Dialogue_Quest_Dic>(c);
        }
        else
        {
            Dictionary<string,List<Dialogue_Quest_Data>> SettingDic = _questDic.DialogueQuestDic;
            List<Dialogue_Quest_Data> questDatas = dialogueList.Dialogue_Quest_Data;
            for (int i = 0; i < questDatas.Count; i++)
            {
                if (SettingDic.ContainsKey(questDatas[i].QuestTargetName))
                {
                    SettingDic[questDatas[i].QuestTargetName].Add(questDatas[i]);    
                }
                else
                {
                    SettingDic.Add(questDatas[i].QuestTargetName,new List<Dialogue_Quest_Data>()); //새 Key추가
                    SettingDic[questDatas[i].QuestTargetName].Add(questDatas[i]); //이후 값 추가
                }
            }
        }
        DialogueManager.instance._questdic = _questDic.DialogueQuestDic;
        DialogueManager.instance. _dialogdic = dic.DialogueDic;
    }
    
    #endregion

    #region PlaceData
    IEnumerator PlaceDBSet()
    {
        yield return new WaitForSeconds(1);
        string placeDB = Resources.Load("PlaceDB").ToString();
        PlaceDBDatas = JsonConvert.DeserializeObject<PlaceDBDatas>(placeDB);
        
        
        for (int i = 0; i < PlaceDBDatas.PlaceDB.Count; i++)
        {
            //TODO : 수정 예정
            // var objtoload = Resources.Load<GameObject>($"{PlaceDBDatas.PlaceDB[i].Place_Path}");  // 프리팹 가져오기
            // var obj = Instantiate(objtoload, 캔버시즈 오브젝트 트랜스폼); // 프리팹 복제
            // obj.SetActive(false);
            // obj.GetComponent<CanvasOnLoad>().states =
            //     _playerToSave.Information.canvasSettingData[PlaceDBDatas.PlaceDB[i].Place_ID];
            // UIManager.instance.CanvasGroup.Add(PlaceDBDatas.PlaceDB[i].Place_ID,obj); //프리팹 Dic에 추가하기.
            
        }
        // foreach (var VARIABLE in PlaceDBDatas.PlaceDB) //확인용
        // {
        //     Debug.Log(VARIABLE.Place_Name);
        // }
    }    
        #endregion

    #region itemData
   
    public ItemDataList GetDefaultItemDataList()
    {
        return defaultItemDataList;
    }
    
    public ItemDataList GetItemDataList()
    {
        return itemDataList;
    }
    
    IEnumerator SetItemData()
    {
        yield return new WaitForSeconds(1); //TODO : 가능하면 캐싱해서 사용 현재 3번 사용됨. 아니면 변경할 것.
        //TODO : 아이템 데이터 불러오는 부분은 뭔가 변경이 필요해 보임.
        LoadSaveData("Save"); //SaveItem이라고 이름 바뀌어야 함. => Inventory랑 
        saveItemDataList = GetItemDataList();
    }
    
    /// <summary>
    /// 다회차 플레이의 경우 얻은 아이템들의 기록 => 남겨놓기 + 기존 아이템 저장이랑 분리해서 관리.(Load하는 시점이 달라야됨.)
    /// </summary>
    /// <returns></returns>
    IEnumerator SetDogamData()
    {
        //TODO : 게임 끝날 때 저장해서 따로 Json으로 저장해두기. + 삭제되는건 없고 계속 쌓여야됨.
        yield return new WaitForSeconds(1); 
        LoadSaveData("Dogam"); 
        dogamItemDataList = GetItemDataList();

        if (saveItemDataList == null)
        {
            yield break;
        }
        else
        {
            if (dogamItemDataList == null)
            {
                dogamItemDataList = saveItemDataList;
            }
            else
            {
                saveItemDataList.Data = saveItemDataList.Data.OrderBy(x => x.item_id).ToList();
                for (int i = 0; i < saveItemDataList.Data.Count; i++)
                {
                    for (int j = 0; j < dogamItemDataList.Data.Count; j++)
                    {
                        if (saveItemDataList.Data[i].item_id == dogamItemDataList.Data[j].item_id) break;
                        if (j + 1 == dogamItemDataList.Data.Count)
                        {
                            dogamItemDataList.Data.Add(saveItemDataList.Data[i]);
                        }
                    }
                }
            }
            dogamItemDataList.Data = dogamItemDataList.Data.OrderBy(x => x.item_id).ToList();
            SaveData(dogamItemDataList, "Dogam");
        }
        DogamItemInDic();
    }

    private void DogamItemInDic()
    {
        for (int i = 0; i < dogamItemDataList.Data.Count; i++)
        {
            if (!dogamItemData.ContainsKey(dogamItemDataList.Data[i].item_id)) 
                dogamItemData.Add(dogamItemDataList.Data[i].item_id, dogamItemDataList.Data[i]);
        }
    }
    
    #endregion

    #region Resources.Load

    //json ���� �ҷ�����. ������ �� �ҷ����� ��
    public void LoadDefaultData()
    {
        var data = Resources.Load("ItemInfo").ToString();
        defaultItemDataList = JsonConvert.DeserializeObject<ItemDataList>(data);
    }
    
    public GameObject GameObjectLoad(string str)
    {
        var obj = Resources.Load(str) as GameObject;
        if (obj == null)
        {
            Debug.Log("Fail Load");
            return null;
        }
        return obj;
    }

    public Sprite SpriteLoad(string str)
    {
        var obj = Resources.Load<Sprite>(str);
        if (obj == null)
        {
            Debug.Log("Fail Load");
            return null;
        }
        return obj;
    }
    
    #endregion

    #region Save & Load
    
    //저장 1번 : 대화 퀘스트 딕셔너리 / 2번 인벤토리 / 3번 진행 데이터 
    //Save & Load 여부에 관계없이 읽어오는 파일들은 이미 구현 되어있음. TODO : 아이템 관련해서 잠깐 볼 것.
    public void Save()
    {
        _playerToSave.Information = GameManager.Instance.Playerinformation;
        foreach (var VARIABLE in UIManager.instance.CanvasGroup)
        {
            _playerToSave.Information.canvasSettingData.Add(VARIABLE.Key,VARIABLE.Value.GetComponent<CanvasOnLoad>().states);    
        }
        _playerToSave.DialogueQuestDic = _questDic;
        
        foreach (var items in ItemManager.Instance.getItems.Values)
        {
            _playerToSave.Inventory.Add(items.id);
        }
        
        //TODO : 도감도 저장.
        string data = JsonConvert.SerializeObject(_playerToSave);
        File.WriteAllText(path,data);
    }

    public void Load()
    {
        var readData = File.ReadAllText(path);
        var converData = JsonConvert.DeserializeObject<Player>(readData);
        _playerToSave.DialogueQuestDic = converData.DialogueQuestDic;
        _playerToSave.Inventory = converData.Inventory;
        _playerToSave.Information = converData.Information;
        //TODO : 인벤토리 세팅해주는 과정 추가해야함.   
        //TODO : 도감도 불러오기(세팅은 도감버튼 눌렀을 때 하고 데이터만 가져오기)
    }
    
    public void SaveData(ItemDataList itemDataList, string str)
    {
        string data = JsonConvert.SerializeObject(itemDataList);
        sb.Append(path);
        sb.Append($"/{str}.json");
        File.WriteAllText(sb.ToString(), data);
        sb.Clear();
    }
    
    public void LoadSaveData(string str)
    {
       
            sb.Append(path);
            sb.Append($"/{str}.json");
            if (File.Exists(sb.ToString()))
            {
                var data = File.ReadAllText(sb.ToString());
                itemDataList = JsonConvert.DeserializeObject<ItemDataList>(data);
            }
            else
            {
                Debug.Log("아이템 데이터 리스트가 없습니다.(DataManger : 284");
                itemDataList = null;
            }

            sb.Clear();

    }
    
    private void OffGame()
    {
        SaveData(saveGetItems, "SaveItem");
    }
    
    #endregion
}