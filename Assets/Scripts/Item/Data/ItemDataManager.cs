using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDataList
{
    //<������ �޾ƿ� class> �������� ��Ʈ�� �ִ� ������ �ִ� ��Ʈ��.
    public List<ItemData> Data = new List<ItemData>();
    public List<ItemData> Trigger = new List<ItemData>();
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

    public Dictionary<int, GameObject> triggerItemData = new Dictionary<int, GameObject>();


    //--------------------------------------------------------------------
    //Dogam, �������� ���� ������ ����
    private ItemDataList dogamItemDataList;
    private ItemDataList saveItemDataList;
    public Dictionary<int, ItemData> dogamItemData = new Dictionary<int, ItemData>();

    public void SetDogamItemData()
    {
        Utility.Instance.LoadSaveData("Save");
        saveItemDataList = Utility.Instance.GetItemDataList();
        Utility.Instance.LoadSaveData("Dogam");
        dogamItemDataList = Utility.Instance.GetItemDataList();

        if (saveItemDataList == null)
        {
            return;
        }
        else
        {
            if (dogamItemDataList == null)
            {
                dogamItemDataList = saveItemDataList;
            }
            else
            {
                saveItemDataList.Data = saveItemDataList.Data.OrderBy(x => x.item_id).ToList();
                for (int i = 0; i < saveItemDataList.Data.Count; i++)
                {
                    for (int j = 0; j < dogamItemDataList.Data.Count; j++)
                    {
                        if (saveItemDataList.Data[i].item_id == dogamItemDataList.Data[j].item_id) break;
                        if (j + 1 == dogamItemDataList.Data.Count)
                        {
                            dogamItemDataList.Data.Add(saveItemDataList.Data[i]);
                        }
                    }
                }
            }
            dogamItemDataList.Data = dogamItemDataList.Data.OrderBy(x => x.item_id).ToList();
            Utility.Instance.SaveData(dogamItemDataList, "Dogam");
        }
        DogamItemInDic();
    }

    private void DogamItemInDic()
    {
        for (int i = 0; i < dogamItemDataList.Data.Count; i++)
        {
            if (!dogamItemData.ContainsKey(dogamItemDataList.Data[i].item_id)) 
                dogamItemData.Add(dogamItemDataList.Data[i].item_id, dogamItemDataList.Data[i]);
        }
    }


    //--------------------------------------------------------------------
    //Json, ��� ������ ���� ��������.
    private ItemDataList defaultItemDataList = new ItemDataList();

    public ItemDataList GetDefaultItemDataList()
    {
        return defaultItemDataList;
    }

    //json ���� �ҷ�����. ������ �� �ҷ����� ��
    public void LoadDefaultData()
    {
        var data = Resources.Load("ItemInfo").ToString();
        defaultItemDataList = JsonConvert.DeserializeObject<ItemDataList>(data);
    }
}
