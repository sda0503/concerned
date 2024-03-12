using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ItemDataList
{
    //<정보를 받아올 class> 스프레드 시트에 있는 정보가 있는 시트명.
    public List<ItemData> Data = new List<ItemData>();
}

public class ItemDataManager
{
    private static ItemDataManager instance;
    public static ItemDataManager Instance
    {
        get
        {
            if (instance == null)
                instance = new ItemDataManager();

            return instance;
        }
    }

    //--------------------------------------------------------------------
    //Json
    string path = $"{Application.persistentDataPath}";

    private ItemDataList defaultItemDataList = new ItemDataList();

    public ItemDataList GetItemDataList()
    {
        return defaultItemDataList;
    }

    //json 파일 저장하기
    public void SaveData(ItemDataList itemDataList, string str)
    {
        Debug.Log(path);
        string data = JsonUtility.ToJson(itemDataList);

        //저장파일 생성. 외부에 저장.
        File.WriteAllText(path + "/" + str, data);
    }

    //json 파일 불러오기. 시작할 때 불러오는 것
    public void LoadDefaultData()
    {
        var data = Resources.Load("ItemInfo").ToString();
        defaultItemDataList = JsonConvert.DeserializeObject<ItemDataList>(data);
    }

    //json 파일 불러오기. play 중에 저장된 것 불러오거나 play 끝나고 도감에서 불러오는 것.
    public void LoadSaveData(string str)
    {
        try
        {
            var data = File.ReadAllText(path + "/" + str);
            defaultItemDataList = JsonUtility.FromJson<ItemDataList>(data);
        }
        catch { defaultItemDataList = null; }
    }
}
