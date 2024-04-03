using System;
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

public class DataManager : SingletonBase<DataManager> //유니티 기능을 상속 받는거 /코루틴이나 유니티 이벤트를 연동하려면 필요함.
{
    private Player _playerToSave; //Save & Load 대기용

    public event Action LoadingChange;

    public Player player
    {
        get
        {
            if (_playerToSave == null)
            {
                _playerToSave = new Player();
            }
            return _playerToSave;
        }
    }

    #region 게임에서 사용할 데이터

    public Information Playerinformation = new Information(); //TODO : 일단은 New

    public Dialogue_Dic dic = new Dialogue_Dic();
    public Dialogue_Quest_Dic _questDic = new Dialogue_Quest_Dic();

    public Inventory _inventory = new Inventory();
    private ItemDataList defaultItemDataList = new ItemDataList();
    private ItemDataList itemDataList = new ItemDataList();
    private ItemDataList dogamItemDataList = new ItemDataList();
    public ItemDataList saveGetItems = new ItemDataList();

    private ItemDataList saveItemDataList = new ItemDataList();
    public Dictionary<int, ItemData> dogamItemData = new Dictionary<int, ItemData>();

    public PlaceDBDatas PlaceDBDatas;
    public PlaceDBLoad PlaceDBLoad = new PlaceDBLoad();

    private WaitForSeconds loadingwait = new WaitForSeconds(0.5f);

    #endregion

#if UNITY_EDITOR
    string path;

    [SerializeField] public Transform asdf;


#endif

    private StringBuilder sb = new StringBuilder();

    public override void init()
    {
        base.init();
        path = Application.persistentDataPath;
        StartCoroutine(SetDatas());
    }

    /// <summary>
    /// 전체 데이터 세팅
    /// </summary>
    /// <returns></returns>
    IEnumerator SetDatas() //전체 기본 데이터 세팅
    {
        //전체적으로 다 코루틴으로 꾸려주어야함.
        yield return StartCoroutine(dialogueDBSet()); 
        //TODO : 로딩 종료될때마다 콜백줘서 진행도 표시하기
        LoadingChange?.Invoke();
        Debug.Log("다이얼로그 세팅 완료");

        yield return StartCoroutine(PlaceDBSet());
        LoadingChange?.Invoke();
        Debug.Log("장소 세팅 완료");

        yield return StartCoroutine(LoadDefaultData());
        LoadingChange?.Invoke();
        Debug.Log("아이템 세팅 완료");
        //ItemManager.Instance.getItems.Clear();

        yield return StartCoroutine(SetDogamData()); //TODO : 도감도 불러오기(세팅은 도감버튼 눌렀을 때 하고 데이터만 가져오기)
        LoadingChange?.Invoke();
        Debug.Log("도감 세팅 완료");
        
        GameManager.Instance.Playerinformation = Playerinformation; //TODO : 이건뭐지
    }
    //스트리밍에셋폴더에다가 에셋번들 집어넣어놓고 로딩하는 방법. 이 방법이 따로 서버 세팅안하고 준비할수있는 제일 좋은방법일 듯.

    public void LoadData() //TODO : 이어하기 선택 시 불러올 데이터 내용 작성 + 버튼에 들어갈 내용
    {
        StartCoroutine(LoadGameData()); 
        Debug.Log("게임 데이터 로드 완료");
    }

    #region dialogueData

    IEnumerator dialogueDBSet() //TODO : 로드에 관한 부분도 넣어서 작성해야됨 => 엎을 예정
    {
        yield return loadingwait; //TODO : 이걸 쓰거나 마지막에 yield break / yield return null을 넣어줘도 사용 가능.
        //string loadData = Resources.Load("Dialogue_DB").ToString(); //LoadAsync로 바꾸는 것 찾아보기.

        ResourceRequest dialogueDBRequest = Resources.LoadAsync<TextAsset>("Dialogue_DB");

        yield return dialogueDBRequest; //어싱크 사용할거면 무조건 기다려야함.

        TextAsset loaddialougeDB = dialogueDBRequest.asset as TextAsset;

        if (loaddialougeDB == null)
        {
            Debug.Log("파일이 없습니다. : DataManager 123");
            yield break;
        }

        string loadData = loaddialougeDB.text;

        Dialogue_List dialogueList = JsonConvert.DeserializeObject<Dialogue_List>(loadData);

        dic.DialogueDic.Clear(); //public 일 때는 클리어 한번 해주는게 좋음.

        for (int i = 0; i < dialogueList.Dialouge_Log_Data.Count; i++)
        {
            dic.DialogueDic.Add(dialogueList.Dialouge_Log_Data[i].Dialogue_idx, dialogueList.Dialouge_Log_Data[i]);
        }

        
        //DialogueManager.Instance._dialogdic = dic.DialogueDic;
    }

    IEnumerator QuestSet() //TODO : 개인 플레이 데이터 불러오는 부분이니까 분리하고 비동기로 작업하기. + 이어하기 버튼에 연결
    {
        
        if (File.Exists(path + "/save.json"))
        {
            var readAllTextAsync = File.ReadAllTextAsync(path + "/save.json");
            
            yield return readAllTextAsync.Result;
            
            // if (readAllTextAsync == null)
            // {
            //     Debug.Log("파일이 없습니다. : DataManager 151");
            //     yield break;
            // }

            string questData = readAllTextAsync.Result;
            _questDic = JsonConvert.DeserializeObject<Dialogue_Quest_Dic>(questData);
        }
        else
        {
            var readQuestJson = Resources.LoadAsync<TextAsset>("Quest_DB");
            yield return readQuestJson;

            if (readQuestJson == null)
            {
                Debug.Log("파일이 없습니다 : DataManager 166");
                yield break;
            }
            
            TextAsset questData = readQuestJson.asset as TextAsset;
            string quest = questData.text;
            Dialogue_Quest_List questList = JsonConvert.DeserializeObject<Dialogue_Quest_List>(quest);
            
            Dictionary<string, List<Dialogue_Quest_Data>> SettingDic = _questDic.DialogueQuestDic; //캐싱한거
            List<Dialogue_Quest_Data> questDatas = questList.Dialogue_Quest_Data;
            for (int i = 0; i < questDatas.Count; i++)
            {
                if (SettingDic.ContainsKey(questDatas[i].QuestTargetName))
                {
                    SettingDic[questDatas[i].QuestTargetName].Add(questDatas[i]);
                }
                else
                {
                    SettingDic.Add(questDatas[i].QuestTargetName, new List<Dialogue_Quest_Data>()); //새 Key추가
                    SettingDic[questDatas[i].QuestTargetName].Add(questDatas[i]); //이후 값 추가
                }
            }
        }
        DialogueManager.Instance._questdic = _questDic.DialogueQuestDic;
    }

    #endregion

    #region PlaceData

    IEnumerator PlaceDBSet() //TODO : 이 구문 자체는 게임 시작할 때 들어가야됨.
    {
        yield return loadingwait;
        var placeDB = Resources.LoadAsync<TextAsset>("PlaceDB");
        if (placeDB == null)
        {
            Debug.Log("파일이 없습니다 : DataManager 203");
            yield break;
        }

        var place = placeDB.asset as TextAsset;
        string placeData = place.text;
        PlaceDBDatas = JsonConvert.DeserializeObject<PlaceDBDatas>(placeData);


        for (int i = 0; i < PlaceDBDatas.PlaceDB.Count; i++)
        {
            //TODO : 수정 예정 + 캔버스 오브젝트의 트랜스폼을 받아오는 메서드를 하나 사용해야 할 듯.
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
        yield return loadingwait; //TODO : 가능하면 캐싱해서 사용 현재 3번 사용됨. 아니면 변경할 것.
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
        
        yield return loadingwait;
        if (!File.Exists(path + "/Dogam.json"))
        {
            Debug.Log("파일이 없습니다. : DataManager 260");
             yield break;   
        }
        var DogamdataRead = File.ReadAllTextAsync(path + "/Dogam.json");

        yield return DogamdataRead.Result;

        List<int> dogamList = JsonConvert.DeserializeObject<List<int>>(DogamdataRead.Result);

        foreach (var VARIABLE in dogamList)
        {
            dogamItemDataList.Data.Add(defaultItemDataList.Data[VARIABLE]);
        }
        //이렇게 변경해서 만들어줘야될듯.

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

    IEnumerator LoadDefaultData()
    {
        yield return loadingwait;
        var data = Resources.LoadAsync<TextAsset>("ItemInfo");
        yield return data;
        if (data == null)
        {
            Debug.Log("파일이 없습니다. : DataManager 298");
            yield break;
        }
        
        TextAsset loadfile = data.asset as TextAsset;
        string loaditemdata = loadfile.text;
        
        defaultItemDataList = JsonConvert.DeserializeObject<ItemDataList>(loaditemdata);
    }

    public GameObject GameObjectLoad(string str) //TODO : 어찌 처리할꼬
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
        foreach (var VARIABLE in UIManager.Instance.CanvasGroup)
        {
            _playerToSave.Information.canvasSettingData.Add(VARIABLE.Key,
                VARIABLE.Value.GetComponent<CanvasOnLoad>().states);
        }

        _playerToSave.DialogueQuestDic = _questDic;

        foreach (var items in ItemManager.Instance.getItems.Values)
        {
            _playerToSave.Inventory.Add(items.id);
        }

        DogamSave(); //TODO : 도감 저장용, 나중에 체크해볼것.

        
        string data = JsonConvert.SerializeObject(_playerToSave);
        File.WriteAllText(path+"/PlayData.json", data);
    }

    void DogamSave()
    {
        if (saveItemDataList == null) //저장된 아이템 없으면 종료
        {
            return;
        }

        List<int> dogamSave = new List<int>();

        if (dogamItemDataList == null) //도감이 비어있으면 저장된 아이템 그대로 저장.
        {
            foreach (var VARIABLE in saveItemDataList.Data)
            {
                dogamSave.Add(VARIABLE.item_id);
            }
        }
        else
        {
            saveItemDataList.Data = saveItemDataList.Data.OrderBy(x => x.item_id).ToList();
            foreach (var VARIABLE in saveItemDataList.Data)
            {
                if (!dogamItemDataList.Data.Contains(VARIABLE))
                {
                    dogamItemDataList.Data.Add(VARIABLE);
                }
            }

            dogamSave.Clear();
            foreach (var VARIABLE in dogamItemDataList.Data)
            {
                dogamSave.Add(VARIABLE.item_id);
            }
        }

        //dogamItemDataList.Data = dogamItemDataList.Data.OrderBy(x => x.item_id).ToList();
        dogamSave = dogamSave.OrderBy(x=>x).ToList();

        var dogam = JsonConvert.SerializeObject(dogamSave);
        File.WriteAllText(Application.persistentDataPath+"/Dogam.json",dogam);
    }

    IEnumerator LoadGameData()
    {
        if (!File.Exists(path + "/concerned.json"))
        {
            Debug.Log("파일이 없습니다 : DataManager 406");
            yield break;
        }
        
        var readData = File.ReadAllTextAsync(path+"/concerned.json");

        yield return readData.Result;
        
        var converData = JsonConvert.DeserializeObject<Player>(readData.Result);
        _playerToSave.DialogueQuestDic = converData.DialogueQuestDic;
        _playerToSave.Inventory = converData.Inventory;
        yield return StartCoroutine(SetInventory()); //TODO : 인벤토리 세팅해주는 과정 추가해야함.
        Debug.Log("인벤토리 세팅 완료");
        _playerToSave.Information = converData.Information;
        GameManager.Instance.Playerinformation.OnLoadSetting();
    }

    IEnumerator SetInventory()
    {
        yield return loadingwait;
        foreach (var VARIABLE in _playerToSave.Inventory)
        {
            ItemManager.Instance.getItems.Add(VARIABLE,new Item(VARIABLE));    
        }
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