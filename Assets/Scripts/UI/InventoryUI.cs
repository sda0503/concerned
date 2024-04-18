using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : PopupUIBase
{
    public Transform itemSlotTransform;
    public Dictionary<int,Item> items = new Dictionary<int, Item>();
    public List<GameObject> inventorySlots = new List<GameObject>();

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI fullPage;
    public TextMeshProUGUI Page;
    int page_number;
    int full_page_number;

    public Button closeButton;

    public Button nextPageButton;
    public Button prePageButton;

    private void Start()
    {
        nextPageButton.onClick.AddListener(OnNextPageButton);
        prePageButton.onClick.AddListener(OnPrePageButton);
    }

    private void OnEnable()
    {
        if (inventorySlots.Count == 0) MakeInventorySlot(3);
        AddItemList();
        StartSet();
    }

    private void StartSet()
    {
        page_number = 1;
        if (inventorySlots.Count % 12 == 0) full_page_number = inventorySlots.Count / 12;
        else full_page_number = inventorySlots.Count / 12 + 1;

        Page.text = page_number.ToString();
        fullPage.text = full_page_number.ToString();

        if (inventorySlots.Count >= 12)
        {
            for(int i = 0; i < inventorySlots.Count; i++)
            {
                if (i < 12)
                {
                    inventorySlots[i].SetActive(true);
                }
                else inventorySlots[i].SetActive(false);
            }
        }
    }

    private void MakeInventorySlot(int count)
    {
        for(int i = 0; i < count; i++)
        {
            inventorySlots.Add(Instantiate(DataManager.Instance.GameObjectLoad("Prefabs/InventoryItemSlot"), itemSlotTransform));
        }
    }

    private void AddItemList()
    {
        for(int i = 0; i < DataManager.Instance.getItemsNumber.Count;i++)
        {
            SetItemSlot(DataManager.Instance.getItems[DataManager.Instance.getItemsNumber[i]].id);
            items.Add(DataManager.Instance.getItems[DataManager.Instance.getItemsNumber[i]].id, DataManager.Instance.getItems[DataManager.Instance.getItemsNumber[i]]);
        }
        DataManager.Instance.getItemsNumber.Clear();
    }

    private void SetItemSlot(int id)
    {
        if (items.Count >= 3 && items.Count % 3 == 0) 
        {
            MakeInventorySlot(3);
        }
        //inventorySlot[items.Count].transform.GetComponent<SpriteRenderer>().sprite = item.Image; �̹��� ����.
        inventorySlots[items.Count].transform.GetChild(1).gameObject.SetActive(true);
        inventorySlots[items.Count].GetComponent<Button>().onClick.AddListener(() => InventorySlotButton(id));
    }

    private void InventorySlotButton(int id)
    {
        nameText.text = items[id].itemData.item_name;
        descriptionText.text = items[id].itemData.default_description;
    }

    private void OnNextPageButton()
    {
        if (page_number == full_page_number) return;

        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (i / 12 == page_number) inventorySlots[i].SetActive(true);
            else inventorySlots[i].SetActive(false);
        }
        page_number++;
        Page.text = page_number.ToString();
    }

    private void OnPrePageButton()
    {
        if (page_number == 1) return;

        page_number--;
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (i / 12 == page_number - 1) inventorySlots[i].SetActive(true);
            else inventorySlots[i].SetActive(false);
        }

        Page.text = page_number.ToString();
    }
}
