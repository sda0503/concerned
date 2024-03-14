using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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

    public void OnClickToFindItem(int index)
    {
        var obj = Resources.Load("Prefabs/Item") as GameObject;
        obj.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("image"); //index에 맞춰서 이미지 로드되도록 설정
        Object.Instantiate(obj);

        ItemManager.Instance.GetItem(index);
    }

    public void OnClickToFindTriggerItem(int index)
    {
        if (!ItemDataManager.Instance.triggerItemData.ContainsKey(index))
        {
            var obj = Resources.Load("Prefabs/TriggerItem") as GameObject;
            obj.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Look"); //index에 맞춰서 이미지 로드되도록 설정
            GameObject o = Object.Instantiate(obj);
            ItemDataManager.Instance.triggerItemData.Add(index, o);
        }
        else ItemDataManager.Instance.triggerItemData[index].SetActive(true);
    }

    //-----------------------------------------------------------------------------------------
    //Json
    string path = $"{Application.persistentDataPath}";
    private ItemDataList itemDataList = new ItemDataList();

    public ItemDataList GetItemDataList()
    {
        return itemDataList;
    }

    //json 파일 저장하기
    public void SaveData(ItemDataList itemDataList, string str)
    {
        Debug.Log(path);
        string data = JsonConvert.SerializeObject(itemDataList);

        //저장파일 생성. 외부에 저장.
        File.WriteAllText(path + "/" + str, data);
    }

    //json 파일 불러오기. play 중에 저장된 것 불러오거나 play 끝나고 도감에서 불러오는 것.
    public void LoadSaveData(string str)
    {
        try
        {
            var data = File.ReadAllText(path + "/" + str);
            itemDataList = JsonConvert.DeserializeObject<ItemDataList>(data);
        }
        catch { itemDataList = null; }
    }
}
