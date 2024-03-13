using System.Collections;
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

    private void Start()
    {
        ItemManager.Instance.AddItemInInventory += AddItemList;
        MakeInventorySlot(3);
    }

    private void MakeInventorySlot(int count)
    {
        for(int i = 0; i < count; i++)
        {
            inventorySlot.Add(Instantiate(Resources.Load("Prefabs/InventoryItemSlot") as GameObject, itemSlot));
        }
    }

    public void AddItemList(Item item) //Ž������ �������� ��� ���.
    {
        
        SetItemSlot(item.id);
        items.Add(item.id, item);
    }

    public void SetItemSlot(int id)
    {
        //������ �̸��̶� ���� text �κ� �����ϴ� ��.
        if (items.Count > 0 && items.Count % 3 == 0) 
        {
            MakeInventorySlot(3);
        }
        //items�� ������ ���� ���̹Ƿ� Count�� ���� ������ ���� -1 �� ����.
        //inventorySlot[items.Count].transform.GetComponent<SpriteRenderer>().sprite = item.Image; �̹��� ����.
        inventorySlot[items.Count].transform.Find("GetItem").gameObject.SetActive(true);
        inventorySlot[items.Count].GetComponent<Button>().onClick.AddListener(() => InventorySlotButton(id));
    }

    public void InventorySlotButton(int id)
    {
        //�̹��� ����.
        nameText.text = items[id].itemData.item_name;
        descriptionText.text = items[id].itemData.default_description;
    }
}
