using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public List<Item> items = new List<Item>(); //인벤토리에 들어가는 아이템들
    public List<GameObject> inventorySlot = new List<GameObject>(); //빈 슬롯, 처음에는 9개(임시)로 만들고 얻은 아이템 갯수가 9개가 넘어가면 3개씩 증가
    

    private void Start()
    {
        ItemManager.Instance.AddItemInInventory += AddItemList;
    }

    public void AddItemList(Item item) //탐색으로 아이템을 얻는 경우.
    {
        SetItemSlot(item.id);
        items.Add(item);
    }

    public void SetItemSlot(int index)
    {
        //아이템 이름이랑 설명 text 부분 연결하는 곳.
        if (items.Count % 3 == 0) 
        {
            //슬롯 만드는 함수 3번 돌리기
        }
        //inventorySlot[items.Count - 1].transform.GetComponent<SpriteRenderer>().sprite = item.Image;
    }
}
