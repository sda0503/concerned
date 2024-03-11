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

public class ItemDataManager : MonoBehaviour
{
    string path = $"{Application.persistentDataPath}";

    private ItemDataList defaultItemDataList = new ItemDataList();

    public ItemDataList GetItemInfo()
    {
        return defaultItemDataList;
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
        var defaultItemDataJson = Resources.Load("ItemInfo").ToString();
        defaultItemDataList = JsonConvert.DeserializeObject<ItemDataList>(defaultItemDataJson);

        //ItemData[] dogam = new ItemData[39];
        //dogam[24] = defaultItemDataList.Data[24];
        //Debug.Log(dogam[24].Name);
    }

    //json ���� �ҷ�����. play �߿� ����� �� �ҷ����ų� play ������ �������� �ҷ����� ��.
    public void LoadSaveData()
    {
        var data = File.ReadAllText(path + "/Save");
        if (data == null) data = File.ReadAllText(path + "/ItemInfo.json");
        defaultItemDataList = JsonUtility.FromJson<ItemDataList>(data);
    }

    //�������� ������ true -> �������� �̵��Ǿ���Ѵ�.
    public bool SetItemGetCheck()
    {
        return true;
    }
}
