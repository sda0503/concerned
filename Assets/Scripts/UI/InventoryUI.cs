using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : PopupUIBase
{
    public Dictionary<int,Item> items = new Dictionary<int, Item>(); //�κ��丮�� ���� �����۵�
    public List<GameObject> inventorySlot = new List<GameObject>(); //�� ����, ó������ 9��(�ӽ�)�� ����� ���� ������ ������ 9���� �Ѿ�� 3���� ����

    public Transform itemSlot;

    public Text nameText;
    public Text descriptionText;

    public Button closeButton;

    private void Start()
    {
        closeButton.onClick.AddListener(CloseUI);
    }

    private void OnEnable()
    {
        if (inventorySlot.Count == 0) MakeInventorySlot(3);
        AddItemList();
    }

    private void MakeInventorySlot(int count)
    {
        for(int i = 0; i < count; i++)
        {
            inventorySlot.Add(Instantiate(DataManager.Instance.GameObjectLoad("Prefabs/InventoryItemSlot"), itemSlot));
        }
    }

    private void AddItemList()
    {
        Dictionary<int, Item> getItems = DataManager.Instance.getItems;
        for(int i = 0; i < DataManager.Instance.getItemsNumber.Count;i++)
        {
            List<int> getItemsNumber = DataManager.Instance.getItemsNumber;
            SetItemSlot(getItems[getItemsNumber[i]].id);
            items.Add(getItems[getItemsNumber[i]].id, getItems[getItemsNumber[i]]);
        }
        DataManager.Instance.getItemsNumber.Clear();
    }

    public void SetItemSlot(int id)
    {
        if (items.Count >= 3 && items.Count % 3 == 0) 
        {
            MakeInventorySlot(3);
        }
        //inventorySlot[items.Count].transform.GetComponent<SpriteRenderer>().sprite = item.Image; �̹��� ����.
        inventorySlot[items.Count].transform.Find("GetItem").gameObject.SetActive(true);
        inventorySlot[items.Count].GetComponent<Button>().onClick.AddListener(() => InventorySlotButton(id));
    }

    public void InventorySlotButton(int id)
    {
        nameText.text = items[id].itemData.item_name;
        descriptionText.text = items[id].itemData.default_description;
    }
}
