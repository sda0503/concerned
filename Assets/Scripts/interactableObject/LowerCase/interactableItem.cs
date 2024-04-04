using System;
using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class interactableItem : interactableObject
{
    public Animator animator;
    private static readonly int trigger_animation = Animator.StringToHash("OnIn");
    private Image _image;
    //private Sprite _image;
    public int ItemId;

    public ItemType ItemType;
    //Type으로 trigger랑 분류해도 될 듯?

    protected override void Start()
    {
        //Debug.Log($"{DataManager.Instance.GetDefaultItemDataList().Data[ItemId].item_name} | {ItemType}");
        base.Start();
        animator = gameObject.GetComponent<Animator>();
        SettingItem();
    }

    public void OnClickItem()
    {
        animator.SetTrigger(trigger_animation);
    }

    public void OnClickTriggerItem()
    {
        SettingTriggerItem(ItemId);
        gameObject.SetActive(false);
    }

    private void SettingItem()
    {
        if (ItemManager.Instance.getItems.ContainsKey(ItemId)) ItemType = ItemType.Normal;
        else if (ItemManager.Instance.triggerItemsData.ContainsKey(ItemId)) ItemType = ItemType.Trigger;
        if (ItemType == ItemType.Normal)
        {
            //ItemManager.Instance.OnClickToFindItem(item_id);
            btn.onClick.AddListener(OnClickItem);
        }
        else if (ItemType == ItemType.Trigger)
        {
            //ItemManager.Instance.OnClickToFindTriggerItem(item_id);
            btn.onClick.AddListener(OnClickTriggerItem);
        }
    }

    private void SettingTriggerItem(int id)
    {
        switch (id)
        {
            case 51:
                ItemManager.Instance.OnClickToFindItem(1);
                break;
        }
    }
}
