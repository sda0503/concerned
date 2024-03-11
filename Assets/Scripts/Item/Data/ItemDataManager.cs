using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemDataList
{
    //<������ �޾ƿ� class> �������� ��Ʈ�� �ִ� ������ �ִ� ��Ʈ��.
    public List<ItemData> Data;
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

    public ItemData GetItemData(int id)
    {
        return defaultItemDataList.Data[id];
    }

    //json ���� �����ϱ�
    public void SaveData(ItemDataList Data)
    {
        defaultItemDataList = Data;
        string data = JsonUtility.ToJson(defaultItemDataList);

        //�������� ����. �ܺο� ����.
        File.WriteAllText(path + "/Save", data);
    }

    //json ���� �ҷ�����. ������ �� �ҷ����� ��
    public void LoadDefaultData()
    {
        var data = Resources.Load("ItemInfo").ToString();
        defaultItemDataList = JsonConvert.DeserializeObject<ItemDataList>(data);
    }

    //json ���� �ҷ�����. play �߿� ����� �� �ҷ����ų� play ������ �������� �ҷ����� ��.
    public void LoadSaveData()
    {
        var data = File.ReadAllText(path + "/Save");
        if (data == null) data = Resources.Load("ItemInfo").ToString();
        defaultItemDataList = JsonUtility.FromJson<ItemDataList>(data);
    }
}
