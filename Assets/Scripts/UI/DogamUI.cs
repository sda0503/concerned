using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class DogamUI : PopupUIBase
{
    public GameObject itemSlot;
    private GameObject[] itemSlots = new GameObject[50];

    public Button clueButton;
    public Button albumButton;

    public Text nameText;
    public Text descriptionText;

    private void OnEnable() //Start·Î ¹Ù²ð ¿¹Á¤.
    {
        ItemDataManager.Instance.SetDogamItemData();
        MakeDogamItemSlot();
    }

    private void MakeDogamItemSlot()
    {
        for (int i = 0; i < 12; i++)
        {
            itemSlots[i] = Instantiate(Resources.Load("Prefabs/DogamItemSlot") as GameObject, itemSlot.transform);
            OnDogamItem(i);
            int n = i;
            itemSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
            itemSlots[i].GetComponent<Button>().onClick.AddListener(() => DogamSlotButton(n));
        }
    }

    public void DogamSlotButton(int index)
    {
        nameText.text = ItemDataManager.Instance.dogamItemData[index].item_name;
        descriptionText.text = ItemDataManager.Instance.dogamItemData[index].default_description;
    }

    public void OnDogamItem(int key)
    {
        if (ItemDataManager.Instance.dogamItemData.ContainsKey(key))
        {
            itemSlots[key].transform.Find("GetItem").gameObject.SetActive(true);
            itemSlots[key].GetComponent<Button>().enabled = true;
        }
        else
        {
            itemSlots[key].transform.Find("GetItem").gameObject.SetActive(false);
            itemSlots[key].GetComponent<Button>().enabled = false;
        }
    }
}
