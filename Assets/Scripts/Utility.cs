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

    public void OnClickToFindItem(int index)
    {
        var obj = Resources.Load("Prefabs/Item") as GameObject;
        obj.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("image"); //index�� ���缭 �̹��� �ε�ǵ��� ����
        Object.Instantiate(obj);

        ItemManager.Instance.GetItem(index);
    }

    public void OnClickToFindTriggerItem(int index)
    {
        if (!ItemDataManager.Instance.triggerItemData.ContainsKey(index))
        {
            var obj = Resources.Load("Prefabs/TriggerItem") as GameObject;
            obj.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Look"); //index�� ���缭 �̹��� �ε�ǵ��� ����
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

    //json ���� �����ϱ�
    public void SaveData(ItemDataList itemDataList, string str)
    {
        Debug.Log(path);
        string data = JsonConvert.SerializeObject(itemDataList);

        //�������� ����. �ܺο� ����.
        File.WriteAllText(path + "/" + str, data);
    }

    //json ���� �ҷ�����. play �߿� ����� �� �ҷ����ų� play ������ �������� �ҷ����� ��.
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
