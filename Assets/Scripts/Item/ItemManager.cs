using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Item[] itemsData = new Item[DataManager.instance.GetDefaultItemDataList().Data.Count]; //������ ����ִ� ������
    public TriggerItem[] triggerItemsData = new TriggerItem[DataManager.instance.GetDefaultItemDataList().Trigger.Count];

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
    
    public void OnClickToFindItem(int index, Transform canvas) //여기가 아이템 클릭했을 때 실행되는 구간.
    {
        if (!ItemManager.Instance.getItems.ContainsKey(index))
        {
            var obj = DataManager.instance.GameObjectLoad("Prefabs/Item");
            obj.transform.GetComponent<Image>().sprite = DataManager.instance.SpriteLoad("image"); //index�� ���缭 �̹��� �ε�ǵ��� ����
            Object.Instantiate(obj, canvas);

            ItemManager.Instance.GetItem(index);
        }
    }

    public void OnClickToFindTriggerItem(int index, Transform canvas)
    {
        if (!ItemManager.Instance.triggerItems.ContainsKey(index))
        {
            var obj =  DataManager.instance.GameObjectLoad("Prefabs/TriggerItem");

            //Sprite sprite = SpriteLoad("Look");
            //sprite�� ������ Debug���� �� �ֵ��� �������ִ� ���� ����. 27��ó�� �ѹ��� �ۼ��� ������.
            //obj.transform.GetComponent<Image>().sprite = sprite; //index�� ���缭 �̹��� �ε�ǵ��� ����
            // obj.transform.GetComponent<TriggerItem>().id = index;
            obj = Object.Instantiate(obj, canvas);
            //ItemManager.Instance.GetTriggerItem(index, obj);
        }
        else ItemManager.Instance.triggerItems[index].SetActive(true);
    }

}
