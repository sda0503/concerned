using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
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
        
        DataManager.Instance.SetItemData(); //TODO : 순서 상 로비화면에서 아이템 매니저가 없음. 딱히 없어도 될 것 같긴 함.
        MakeDogamItemSlot();
        closeButton.onClick.AddListener(CloseUI);
    }

    private void MakeDogamItemSlot()
    {
        for (int i = 0; i < 12; i++)
        {
            itemSlots[i] = Instantiate(DataManager.Instance.GameObjectLoad("Prefabs/DogamItemSlot"), itemSlot);
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
}
