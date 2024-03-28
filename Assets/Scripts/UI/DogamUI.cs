using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class DogamUI : PopupUIBase
{
    public Transform itemSlot;
    private GameObject[] itemSlots = new GameObject[50];

    public Button clueButton;
    public Button albumButton;
    public Button closeButton;

    public Text nameText;
    public Text descriptionText;

    private void Start()
    {
        ItemManager.Instance.SetItemData();
        MakeDogamItemSlot();
        closeButton.onClick.AddListener(CloseDogam);
    }

    private void MakeDogamItemSlot()
    {
        for (int i = 0; i < 12; i++)
        {
            itemSlots[i] = Instantiate(DataManager.instance.GameObjectLoad("Prefabs/DogamItemSlot"), itemSlot);
            OnDogamItem(i);
            int n = i;
            itemSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
            itemSlots[i].GetComponent<Button>().onClick.AddListener(() => DogamSlotButton(n));
        }
    }

    public void DogamSlotButton(int index)
    {
        nameText.text = DataManager.instance.dogamItemData[index].item_name;
        descriptionText.text = DataManager.instance.dogamItemData[index].default_description;
    }

    public void OnDogamItem(int key)
    {
        if (DataManager.instance.dogamItemData.ContainsKey(key))
        {
            //�̹��� ����Ǵ� �κ��� �ʿ���.
            itemSlots[key].transform.Find("GetItem").gameObject.SetActive(true);
            itemSlots[key].GetComponent<Button>().enabled = true;
        }
        else
        {
            itemSlots[key].transform.Find("GetItem").gameObject.SetActive(false);
            itemSlots[key].GetComponent<Button>().enabled = false;
        }
    }

    public void CloseDogam()
    {
        gameObject.SetActive(false);
    }
}
