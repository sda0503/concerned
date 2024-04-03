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

    public ItemType ItemType => DataManager.Instance.GetDefaultItemDataList().Data[ItemId].itemType;
    //Type으로 trigger랑 분류해도 될 듯?

    protected override void Start()
    {
        //Debug.Log($"{DataManager.Instance.GetDefaultItemDataList().Data[ItemId].item_name} | {ItemType}");
        base.Start();
        animator = gameObject.GetComponent<Animator>();
        _image = gameObject.GetComponent<Image>();
        //_image = gameObject.GetComponent<Image>().sprite;
        _image.sprite = Resources.Load<Sprite>("Image/1x/돋보기"); //여기는 ItemID에 따라 자동으로 스프라이트 지정해주는 거
        
        btn.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        animator.SetTrigger(trigger_animation);
    }
}
