using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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

    //--------------------------------------------------------------------------
    //Json
    string path = $"{Application.persistentDataPath}";
    private Item itemInfo = new Item();

    public Item GetItemInfo()
    {
        return itemInfo;
    }

    //json ���� �����ϱ�
    public void SaveData(Item Data)
    {
        itemInfo = Data;
        string data = JsonUtility.ToJson(itemInfo);

        //�������� ����. �ܺο� ����.
        File.WriteAllText(path+"/Save", data);
    }

    //json ���� �ҷ�����. ������ �� �ҷ����� ��
    public void LoadDefaultData()
    {
        string data = File.ReadAllText(path + "/ItemInfo");
        itemInfo = JsonUtility.FromJson<Item>(data);
    }

    //json ���� �ҷ�����. play �߿� ����� �� �ҷ����ų� play ������ �������� �ҷ����� ��.
    public void LoadSaveData()
    {
        string data = File.ReadAllText(path + "/Save");
        if (data == null) data = File.ReadAllText(path + "/ItemInfo");
        itemInfo = JsonUtility.FromJson<Item>(data);
    }
}
