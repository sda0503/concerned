using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class ItemManager : MonoBehaviour
{
    public Dictionary<int, Item> itemsData = new Dictionary<int, Item>();
    public Dictionary<int, Item> triggerItemsData = new Dictionary<int, Item>();

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
        for (int i = 0; i < DataManager.Instance.GetDefaultItemDataList().Data.Count; i++)
        {
            if (DataManager.Instance.GetDefaultItemDataList().Data[i].itemType == ItemType.Normal)
            {
                itemsData.Add(DataManager.Instance.GetDefaultItemDataList().Data[i].item_id, new Item(i));
            }
            else if (DataManager.Instance.GetDefaultItemDataList().Data[i].itemType == ItemType.Trigger)
            {
                triggerItemsData.Add(DataManager.Instance.GetDefaultItemDataList().Data[i].item_id, new Item(i));
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
        if (itemsData.ContainsKey(index) && !getItems.ContainsKey(index))
        {
            var obj = DataManager.Instance.GameObjectLoad("Prefabs/Item");
            obj.transform.GetComponent<interactableItem>().ItemId = index;
            obj.transform.GetComponent<Image>().sprite = DataManager.Instance.SpriteLoad("image");
            Instantiate(obj, UIManager.Instance.itemCanvas);

            GetItem(index);
            return;
        }
        else if (itemsData.ContainsKey(index)) return;
        if (triggerItemsData.ContainsKey(index) && !triggerItems.ContainsKey(index))
        {
            var obj = DataManager.Instance.GameObjectLoad("Prefabs/Item");

            //Sprite sprite = SpriteLoad("Look");
            //obj.transform.GetComponent<Image>().sprite = sprite;
            obj.transform.GetComponent<interactableItem>().ItemId = index;
            obj = Instantiate(obj, UIManager.Instance.itemCanvas);
            GetTriggerItem(index, obj);
            return;
        }
        else if (triggerItemsData.ContainsKey(index)) 
        { 
            triggerItems[index].SetActive(true); 
            return; 
        }
        Debug.Log("Item Error");
    }
}
