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

    public Item[] items = new Item[ItemDataManager.Instance.GetDefaultItemDataList().Data.Count]; //������ ����ִ� ������
    public TriggerItem[] triggerItems = new TriggerItem[ItemDataManager.Instance.GetDefaultItemDataList().Trigger.Count];

    public Dictionary<int, Item> getItems = new Dictionary<int, Item>(); //���� Item��
    public List<int> getItemsNumber = new List<int>(); //���� Item�� ����

    private void Start()
    {
        SetItemData();
    }

    public void SetItemData()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new Item(i);
        }
        for (int i = 0; i < triggerItems.Length; i++)
        {
            triggerItems[i] = new TriggerItem(i);
        }
    }

    public void GetItem(int item_id)
    {
        getItems.Add(item_id, items[item_id]);
        getItemsNumber.Add(item_id);
        DataManager.instance.saveGetItems.Data.Add(items[item_id].itemData);
    }
}
