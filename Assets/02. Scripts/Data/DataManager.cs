using System.Collections.Generic;
using System.IO;
using DataStorage;
using Newtonsoft.Json;
using UnityEngine;

//로딩이 얼마 안걸릴거같아도 확장성을 생각하면 코루틴이 좋고
// 설계를 할 때 큰 그림을 그리고 설계를 하면 기술적 고민을 많이 해볼 수 있음.(취준생입장)
public class DataManager : MonoBehaviour//유니티 기능을 상속 받는거 /코루틴이나 유니티 이벤트를 연동하려면 필요함.
{
    public Dialogue_Dic dic = new Dialogue_Dic();
    public Dialogue_Quest_Dic _questDic = new Dialogue_Quest_Dic();
    public Inventory _inventory = new Inventory();
    private string Path;

    public static DataManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else 
            Destroy(gameObject);
    }

    void Start()
    {
        Path = Application.persistentDataPath;
        SetDatas();
    }
    
    public void SaveData()
    {//overwrite : 이미 존재하는 객체에 덮어씌우기 가능
        var save = JsonConvert.SerializeObject(_questDic); //여기체크
        File.WriteAllText(Path+"/save.json",save);
    }

    void SetDatas()
    {
        string loadData = Resources.Load("Dialogue_DB").ToString();
        Dialogue_List dialogueList = JsonConvert.DeserializeObject<Dialogue_List>(loadData);
        dic.DialogueDic.Clear(); //public 일 때는 클리어 한번 해주는게 좋음.
        
        for (int i = 0; i < dialogueList.Dialouge_Log_Data.Count; i++)
        {
            dic.DialogueDic.Add(dialogueList.Dialouge_Log_Data[i].Dialogue_idx, dialogueList.Dialouge_Log_Data[i]);
        }

        if (File.Exists(Path + "/save.json"))
        {
            var c = File.ReadAllText(Path + "/save.json");
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
    }
 
}