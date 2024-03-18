using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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


    //-----------------------------------------------------------------------------------------
    //Item 클릭
    public void OnClickToFindItem(int index, Transform canvas)
    {
        var obj = GameObjectLoad("Prefabs/Item");
        obj.transform.GetComponent<Image>().sprite = SpriteLoad("image"); //index에 맞춰서 이미지 로드되도록 설정
        Object.Instantiate(obj, canvas);

        ItemManager.Instance.GetItem(index);
    }

    public void OnClickToFindTriggerItem(int index, Transform canvas)
    {
        if (!ItemDataManager.Instance.triggerItemData.ContainsKey(index))
        {
            var obj = Object.Instantiate(GameObjectLoad("Prefabs/TriggerItem"), canvas);
            Sprite sprite = SpriteLoad("Look");
            //sprite가 없으면 Debug찍힐 수 있도록 설정해주는 것이 좋음. 27줄처럼 한번에 작성은 ㄴㄴ함.

            obj.transform.GetComponent<Image>().sprite = sprite; //index에 맞춰서 이미지 로드되도록 설정
            ItemDataManager.Instance.triggerItemData.Add(index, obj);
        }
        else ItemDataManager.Instance.triggerItemData[index].SetActive(true);
    }

    //-----------------------------------------------------------------------------------------
    //Resources.Load
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
        File.WriteAllText(path + "/" + str, data + ".json");
    }

    //json 파일 불러오기. play 중에 저장된 것 불러오거나 play 끝나고 도감에서 불러오는 것.
    public void LoadSaveData(string str)
    {
        try
        {
            var data = File.ReadAllText(path + "/" + str + ".json");
            itemDataList = JsonConvert.DeserializeObject<ItemDataList>(data);
        }
        catch { itemDataList = null; }
    }
}
