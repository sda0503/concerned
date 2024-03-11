using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Utility
{
    private static Utility instance;
    //프로퍼티를 이용한 싱글턴 -> AddComponent해주지 않아도 사용이 가능함.
    public static Utility Instance
    {
        get
        {
            if (instance == null)
                instance = new Utility();

            return instance;
        }
    }

    //--------------------------------------------------------------------------
    //Json
    string path = $"{Application.persistentDataPath}";
    private Item itemInfo = new Item();

    public Item GetItemInfo()
    {
        return itemInfo;
    }

    //json 파일 저장하기
    public void SaveData(Item Data)
    {
        itemInfo = Data;
        string data = JsonUtility.ToJson(itemInfo);

        //저장파일 생성. 외부에 저장.
        File.WriteAllText(path+"/Save", data);
    }

    //json 파일 불러오기. 시작할 때 불러오는 것
    public void LoadDefaultData()
    {
        string data = File.ReadAllText(path + "/ItemInfo");
        itemInfo = JsonUtility.FromJson<Item>(data);
    }

    //json 파일 불러오기. play 중에 저장된 것 불러오거나 play 끝나고 도감에서 불러오는 것.
    public void LoadSaveData()
    {
        string data = File.ReadAllText(path + "/Save");
        if (data == null) data = File.ReadAllText(path + "/ItemInfo");
        itemInfo = JsonUtility.FromJson<Item>(data);
    }
}
