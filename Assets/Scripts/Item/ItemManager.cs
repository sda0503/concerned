using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;
    public static ItemManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = typeof(ItemManager).Name;
                instance = obj.AddComponent<ItemManager>();
            }
            return instance;
        }
    }

    public Item[] itemsData = new Item[ItemDataManager.Instance.GetDefaultItemDataList().Data.Count]; //������ ����ִ� ������
    public TriggerItem[] triggerItemsData = new TriggerItem[ItemDataManager.Instance.GetDefaultItemDataList().Trigger.Count];

    public Dictionary<int, Item> getItems = new Dictionary<int, Item>(); //���� Item��
    public Dictionary<int, GameObject> triggerItems = new Dictionary<int, GameObject>();
    public List<int> getItemsNumber = new List<int>(); //���� Item�� ����

    private void Start()
    {
        SetItemData();
    }

    public void SetItemData()
    {
        for (int i = 0; i < itemsData.Length; i++)
        {
            itemsData[i] = new Item(i);
        }
        for (int i = 0; i < triggerItemsData.Length; i++)
        {
            triggerItemsData[i] = new TriggerItem(i);
        }
    }

    public void GetItem(int item_id)
    {
        getItems.Add(item_id, itemsData[item_id]);
        getItemsNumber.Add(item_id);
        DataManager.instance.saveGetItems.Data.Add(itemsData[item_id].itemData);
    }

    public void GetTriggerItem(int item_id, GameObject obj)
    {
        triggerItems.Add(item_id, obj);
    }
}
