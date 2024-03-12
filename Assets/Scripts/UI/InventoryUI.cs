using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public List<Item> items = new List<Item>(); //�κ��丮�� ���� �����۵�
    public List<GameObject> inventorySlot = new List<GameObject>(); //�� ����, ó������ 9��(�ӽ�)�� ����� ���� ������ ������ 9���� �Ѿ�� 3���� ����
    

    private void Start()
    {
        ItemManager.Instance.AddItemInInventory += AddItemList;
    }

    public void AddItemList(Item item) //Ž������ �������� ��� ���.
    {
        SetItemSlot(item.id);
        items.Add(item);
    }

    public void SetItemSlot(int index)
    {
        //������ �̸��̶� ���� text �κ� �����ϴ� ��.
        if (items.Count % 3 == 0) 
        {
            //���� ����� �Լ� 3�� ������
        }
        //inventorySlot[items.Count - 1].transform.GetComponent<SpriteRenderer>().sprite = item.Image;
    }
}
