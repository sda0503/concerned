using DataStorage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DogamUI : PopupUIBase
{
    public Transform itemSlotTransform;
    private GameObject[] itemSlots = new GameObject[40];
    public Image itemImage;

    public Button closeButton;

    public Button nextPageButton;
    public Button prePageButton;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public TextMeshProUGUI fullPage;
    public TextMeshProUGUI Page;
    int page_number;

    private void Awake()
    {
        nextPageButton.onClick.AddListener(OnNextPageButton);
        prePageButton.onClick.AddListener(OnPrePageButton);

        DataManager.Instance.SetItemData(); //TODO : 순서 상 로비화면에서 아이템 매니저가 없음. 딱히 없어도 될 것 같긴 함.
        MakeDogamItemSlot();
    }

    private void OnEnable()
    {
        StartSet();
    }

    private void StartSet()
    {
        page_number = 1;
        Page.text = page_number.ToString();
        fullPage.text = (itemSlots.Length / 12 + 1).ToString();

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < 12)
            {
                itemSlots[i].SetActive(true);
            }
            else itemSlots[i].SetActive(false);
        }
        itemImage.sprite = DataManager.Instance.SpriteLoad("Evidence/00");
        nameText.text = "???";
        descriptionText.text = "???";
    }

    private void MakeDogamItemSlot()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i] = Instantiate(DataManager.Instance.GameObjectLoad("Prefabs/DogamItemSlot"), itemSlotTransform);
            //int n = i;
            itemSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
            //itemSlots[i].GetComponent<Button>().onClick.AddListener(() => DogamSlotButton(n));
            itemSlots[i].GetComponent<Button>().enabled = false;
        }

        OnDogamItem();
    }

    public void DogamSlotButton(int index)
    {
        if (index < 100)
        {
            itemImage.sprite = DataManager.Instance.SpriteLoad("Evidence/" + index.ToString());
        }
        else
        {
            itemImage.sprite = DataManager.Instance.SpriteLoad("Evidence/" + ((index / 100) * 100).ToString());
        }
        nameText.text = DataManager.Instance.dogamItemData[index].item_name;
        descriptionText.text = DataManager.Instance.dogamItemData[index].default_description;
    }

    private void OnDogamItem()
    {
        int i = 0;
        foreach (ItemData item in DataManager.Instance.GetDefaultItemDataList().Data)
        {
            if (item.item_id >= 10000) break;
            if(item.itemType == ItemType.Normal)
            {
                if (DataManager.Instance.dogamItemData.ContainsKey(item.item_id))
                {
                    if (item.item_id < 100)
                    {
                        itemSlots[i].transform.GetChild(1).GetComponent<Image>().sprite = DataManager.Instance.SpriteLoad("Guide Book/" + item.item_id.ToString());
                    }
                    else
                    {
                        itemSlots[i].transform.GetChild(1).GetComponent<Image>().sprite = DataManager.Instance.SpriteLoad("Guide Book/" + ((item.item_id / 10) * 10).ToString());
                    }
                    itemSlots[i].transform.GetChild(1).gameObject.SetActive(true);
                    itemSlots[i].GetComponent<Button>().onClick.AddListener(() => DogamSlotButton(item.item_id));
                    itemSlots[i].GetComponent<Button>().enabled = true;
                }
                i++;
            }
        }
    }

    private void OnNextPageButton()
    {
        if (page_number == itemSlots.Length / 12 + 1) return;
        
        for(int i = 0; i < itemSlots.Length; i++)
        {
            if (i / 12 == page_number) itemSlots[i].SetActive(true);
            else itemSlots[i].SetActive(false);
        }
        page_number++;
        Page.text = page_number.ToString();
    }

    private void OnPrePageButton()
    {
        if (page_number == 1) return;

        page_number--;
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i / 12 == page_number - 1) itemSlots[i].SetActive(true);
            else itemSlots[i].SetActive(false);
        }
        
        Page.text = page_number.ToString();
    }
}
