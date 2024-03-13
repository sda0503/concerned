using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : PopupUIBase
{
    public Dictionary<int,Item> items = new Dictionary<int, Item>(); //인벤토리에 들어가는 아이템들
    public List<GameObject> inventorySlot = new List<GameObject>(); //빈 슬롯, 처음에는 9개(임시)로 만들고 얻은 아이템 갯수가 9개가 넘어가면 3개씩 증가

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

    public void AddItemList(Item item) //탐색으로 아이템을 얻는 경우.
    {
        
        SetItemSlot(item.id);
        items.Add(item.id, item);
    }

    public void SetItemSlot(int id)
    {
        //아이템 이름이랑 설명 text 부분 연결하는 곳.
        if (items.Count > 0 && items.Count % 3 == 0) 
        {
            MakeInventorySlot(3);
        }
        //items에 정보가 들어가기 전이므로 Count는 들어온 아이템 갯수 -1 과 동일.
        //inventorySlot[items.Count].transform.GetComponent<SpriteRenderer>().sprite = item.Image; 이미지 변경.
        inventorySlot[items.Count].transform.Find("GetItem").gameObject.SetActive(true);
        inventorySlot[items.Count].GetComponent<Button>().onClick.AddListener(() => InventorySlotButton(id));
    }

    public void InventorySlotButton(int id)
    {
        //이미지 변경.
        nameText.text = items[id].itemData.item_name;
        descriptionText.text = items[id].itemData.default_description;
    }
}
