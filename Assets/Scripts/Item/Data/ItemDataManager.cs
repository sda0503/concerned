using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ItemDataList
{
    //<정보를 받아올 class> 스프레드 시트에 있는 정보가 있는 시트명.
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
    //Dogam, 도감에서 사용될 데이터 정리
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
    //Json, 모든 아이템 정보 가져오기.
    private ItemDataList defaultItemDataList = new ItemDataList();

    public ItemDataList GetDefaultItemDataList()
    {
        return defaultItemDataList;
    }

    //json 파일 불러오기. 시작할 때 불러오는 것
    public void LoadDefaultData()
    {
        var data = Resources.Load("ItemInfo").ToString();
        defaultItemDataList = JsonConvert.DeserializeObject<ItemDataList>(data);
    }
}
