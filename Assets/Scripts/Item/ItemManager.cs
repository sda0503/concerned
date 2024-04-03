using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class ItemManager : MonoBehaviour
{
    public Item[] itemsData;
    public Item[] triggerItemsData;

    public Dictionary<int, Item> getItems = new Dictionary<int, Item>();
    public Dictionary<int, GameObject> triggerItems = new Dictionary<int, GameObject>();
    public List<int> getItemsNumber = new List<int>();

    public static ItemManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetItemData();
    }

    public void SetItemData()
    {
        int itemCount = 0;
        int triggerItemCount = 0;
        for (int i = 0; i < DataManager.Instance.GetDefaultItemDataList().Data.Count; i++)
        {
            if (DataManager.Instance.GetDefaultItemDataList().Data[i].itemType == ItemType.Normal) itemCount++;
            else if (DataManager.Instance.GetDefaultItemDataList().Data[i].itemType == ItemType.Trigger) triggerItemCount++;
        }
        itemsData = new Item[itemCount];
        triggerItemsData = new Item[triggerItemCount];

        itemCount = 0;
        triggerItemCount = 0;
        for (int i = 0; i < DataManager.Instance.GetDefaultItemDataList().Data.Count; i++)
        {
            if (DataManager.Instance.GetDefaultItemDataList().Data[i].itemType == ItemType.Normal)
            {
                itemsData[itemCount] = new Item(i);
                itemCount++;
            }
            else if (DataManager.Instance.GetDefaultItemDataList().Data[i].itemType == ItemType.Trigger)
            {
                triggerItemsData[itemCount] = new Item(i);
                triggerItemCount++;
            }
        }
    }

    public void GetItem(int item_id)
    {
        getItems.Add(item_id, itemsData[item_id]);
        getItemsNumber.Add(item_id);
        DataManager.Instance.saveGetItems.Data.Add(itemsData[item_id].itemData);
    }

    public void GetTriggerItem(int item_id, GameObject obj)
    {
        triggerItems.Add(item_id, obj);
    }
    
    public void OnClickToFindItem(int index) //여기가 아이템 클릭했을 때 실행되는 구간.
    {
        if (!getItems.ContainsKey(index))
        {
            var obj = DataManager.Instance.GameObjectLoad("Prefabs/Item");
            obj.transform.GetComponent<Image>().sprite = DataManager.Instance.SpriteLoad("image");
            Instantiate(obj, UIManager.instance.itemCanvas);

            GetItem(index);
        }
    }

    public void OnClickToFindTriggerItem(int index)
    {
        if (!triggerItems.ContainsKey(index))
        {
            var obj =  DataManager.Instance.GameObjectLoad("Prefabs/TriggerItem");

            //Sprite sprite = SpriteLoad("Look");
            //obj.transform.GetComponent<Image>().sprite = sprite;
            //obj.transform.GetComponent<TriggerItem>().id = index;
            obj = Instantiate(obj, UIManager.instance.itemCanvas);
            GetTriggerItem(index, obj);
        }
        else triggerItems[index].SetActive(true);
    }

}
