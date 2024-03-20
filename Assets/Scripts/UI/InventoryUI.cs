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

    private void OnEnable()
    {
        if (inventorySlot.Count == 0) MakeInventorySlot(3);
        AddItemList();
    }

    private void MakeInventorySlot(int count)
    {
        for(int i = 0; i < count; i++)
        {
            inventorySlot.Add(Instantiate(Resources.Load("Prefabs/InventoryItemSlot") as GameObject, itemSlot));
        }
    }

    public void AddItemList() //Ž������ �������� ��� ���.
    {
        for(int i = 0; i < ItemManager.Instance.getItemsNumber.Count;i++)
        {
            SetItemSlot(ItemManager.Instance.getItems[ItemManager.Instance.getItemsNumber[i]].id);
            items.Add(ItemManager.Instance.getItems[ItemManager.Instance.getItemsNumber[i]].id, ItemManager.Instance.getItems[ItemManager.Instance.getItemsNumber[i]]);
        }
        ItemManager.Instance.getItemsNumber.Clear();
    }

    public void SetItemSlot(int id)
    {
        //������ �̸��̶� ���� text �κ� �����ϴ� ��.
        if (items.Count >= 3 && items.Count % 3 == 0) 

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
