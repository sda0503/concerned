using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DogamUI : MonoBehaviour
{
    private ItemDataList dogamItemDataList;
    private ItemDataList saveItemDataList;

    public Button clueButton;
    public Button albumButton;

    private void Awake()
    {
        ItemDataManager.Instance.LoadSaveData("Save");
        saveItemDataList = ItemDataManager.Instance.GetItemDataList();
        ItemDataManager.Instance.LoadSaveData("Dogam");
        dogamItemDataList = ItemDataManager.Instance.GetItemDataList();
    }

    private void OnEnable()
    {
        if(saveItemDataList == null)
        {
            //가지고 있는 것에 따라서 도감 아이템 생성해주기..
            return;
        }
        else
        {
            if(dogamItemDataList == null)
            {
                dogamItemDataList = saveItemDataList;
                dogamItemDataList.Data = dogamItemDataList.Data.OrderBy(x => x.item_id).ToList();
                ItemDataManager.Instance.SaveData(dogamItemDataList, "Dogam");
            }
        }
    }
}
