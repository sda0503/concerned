using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemDataList
{
    //<정보를 받아올 class> 스프레드 시트에 있는 정보가 있는 시트명.
    public List<ItemData> Data;
}

public class ItemDataManager : MonoBehaviour
{
    string path = $"{Application.persistentDataPath}";

    private ItemDataList defaultItemDataList = new ItemDataList();

    public ItemDataList GetItemInfo()
    {
        return defaultItemDataList;
    }

    //json 파일 저장하기
    public void SaveData(ItemDataList Data)
    {
        defaultItemDataList = Data;
        string data = JsonUtility.ToJson(defaultItemDataList);

        //저장파일 생성. 외부에 저장.
        File.WriteAllText(path + "/Save", data);
    }

    //json 파일 불러오기. 시작할 때 불러오는 것
    public void LoadDefaultData()
    {
        var defaultItemDataJson = Resources.Load("ItemInfo").ToString();
        defaultItemDataList = JsonConvert.DeserializeObject<ItemDataList>(defaultItemDataJson);

        //ItemData[] dogam = new ItemData[39];
        //dogam[24] = defaultItemDataList.Data[24];
        //Debug.Log(dogam[24].Name);
    }

    //json 파일 불러오기. play 중에 저장된 것 불러오거나 play 끝나고 도감에서 불러오는 것.
    public void LoadSaveData()
    {
        var data = File.ReadAllText(path + "/Save");
        if (data == null) data = File.ReadAllText(path + "/ItemInfo.json");
        defaultItemDataList = JsonUtility.FromJson<ItemDataList>(data);
    }

    //아이템을 얻으면 true -> 도감으로 이동되어야한다.
    public bool SetItemGetCheck()
    {
        return true;
    }
}
