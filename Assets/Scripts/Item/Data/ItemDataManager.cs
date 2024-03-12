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
    private ItemDataList saveItemDataList = new ItemDataList();

    public ItemData GetItemData(int id)
    {
        return defaultItemDataList.Data[id];
    }

    //json ���� �����ϱ�
    public void SaveData()
    {
        for(int i = 0; i < ItemManager.Instance.getItems.Count; i++)
        {
            saveItemDataList.Data.Add(ItemManager.Instance.getItems[i].itemData);
        }
        Debug.Log(path);
        string data = JsonUtility.ToJson(saveItemDataList);

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
