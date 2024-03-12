using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ItemDataList
{
    //<������ �޾ƿ� class> �������� ��Ʈ�� �ִ� ������ �ִ� ��Ʈ��.
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

    //json ���� �����ϱ�
    public void SaveData(ItemDataList itemDataList, string str)
    {
        Debug.Log(path);
        string data = JsonUtility.ToJson(itemDataList);

        //�������� ����. �ܺο� ����.
        File.WriteAllText(path + "/" + str, data);
    }

    //json ���� �ҷ�����. ������ �� �ҷ����� ��
    public void LoadDefaultData()
    {
        var data = Resources.Load("ItemInfo").ToString();
        defaultItemDataList = JsonConvert.DeserializeObject<ItemDataList>(data);
    }

    //json ���� �ҷ�����. play �߿� ����� �� �ҷ����ų� play ������ �������� �ҷ����� ��.
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
