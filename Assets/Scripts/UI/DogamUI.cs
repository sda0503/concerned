using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DogamUI : PopupUIBase
{
    public Transform itemSlotTransform;
    private GameObject[] itemSlots = new GameObject[50];

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
        closeButton.onClick.AddListener(CloseUI);
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
    }

    private void MakeDogamItemSlot()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i] = Instantiate(DataManager.Instance.GameObjectLoad("Prefabs/DogamItemSlot"), itemSlotTransform);
            int n = i;
            itemSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
            itemSlots[i].GetComponent<Button>().onClick.AddListener(() => DogamSlotButton(n));
            itemSlots[i].GetComponent<Button>().enabled = false;

            if (DataManager.Instance.dogamItemData.ContainsKey(i))
            {
                OnDogamItem(i);
            }
        }
    }

    public void DogamSlotButton(int index)
    {
        nameText.text = DataManager.Instance.dogamItemData[index].item_name;
        descriptionText.text = DataManager.Instance.dogamItemData[index].default_description;
    }

    private void OnDogamItem(int key)
    {
        //이미지 변경되는 부분이 필요함.
        itemSlots[key].transform.GetChild(1).gameObject.SetActive(true);
        itemSlots[key].GetComponent<Button>().enabled = true;
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
