using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DogamUI : MonoBehaviour
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
            itemSlots[i] = Resources.Load("Prefabs/DogamItemSlot") as GameObject;
            Instantiate(itemSlots[i], itemSlot.transform);
            int n = i;
            itemSlots[i].GetComponent<Button>().onClick.AddListener(() => DogamSlotButton(n));
            itemSlots[i].GetComponent<Button>().enabled = false;
            OnDogamItem(i);
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
            Debug.Log("On");
            itemSlots[key].transform.Find("GetItem").gameObject.SetActive(true);
            itemSlots[key].GetComponent<Button>().enabled = true;
        }
        else itemSlots[key].transform.Find("GetItem").gameObject.SetActive(false);
    }
}
