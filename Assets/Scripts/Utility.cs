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
    //������Ƽ�� �̿��� �̱��� -> AddComponent������ �ʾƵ� ����� ������.
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
    //Item Ŭ��
    public void OnClickToFindItem(int index, Transform canvas) //여기가 아이템 클릭했을 때 실행되는 구간.
    {
        if (!ItemManager.Instance.getItems.ContainsKey(index))
        {
            var obj = GameObjectLoad("Prefabs/Item");
            obj.transform.GetComponent<Image>().sprite = SpriteLoad("image"); //index�� ���缭 �̹��� �ε�ǵ��� ����
            Object.Instantiate(obj, canvas);

            ItemManager.Instance.GetItem(index);
        }
    }

    public void OnClickToFindTriggerItem(int index, Transform canvas)
    {
        if (!ItemDataManager.Instance.triggerItemData.ContainsKey(index))
        {
            var obj = Object.Instantiate(GameObjectLoad("Prefabs/TriggerItem"), canvas);
            Sprite sprite = SpriteLoad("Look");
            //sprite�� ������ Debug���� �� �ֵ��� �������ִ� ���� ����. 27��ó�� �ѹ��� �ۼ��� ������.

            obj.transform.GetComponent<Image>().sprite = sprite; //index�� ���缭 �̹��� �ε�ǵ��� ����
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

    //json ���� �����ϱ�
    public void SaveData(ItemDataList itemDataList, string str)
    {
        Debug.Log(path);
        string data = JsonConvert.SerializeObject(itemDataList);

        //�������� ����. �ܺο� ����.
        File.WriteAllText(path + "/" + str + ".json", data);
    }

    //json ���� �ҷ�����. play �߿� ����� �� �ҷ����ų� play ������ �������� �ҷ����� ��.
    public void LoadSaveData(string str)
    {
        try
        {
            var data = File.ReadAllText(path + "/" + str + ".json");
            itemDataList = JsonConvert.DeserializeObject<ItemDataList>(data);
        }
        catch (FileNotFoundException)
        { 
            Debug.Log("catch"); 
            itemDataList = null; 
        }
    }
}
