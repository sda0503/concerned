using System;
using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class interactableItem : interactableObject
{
    public Animator animator;
    private static readonly int trigger_animation = Animator.StringToHash("OnIn");
    [SerializeField] private Button btn;
    private Sprite _image;

    public int ItemId;
    //Type으로 trigger랑 분류해도 될 듯?

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        _image = gameObject.GetComponent<Image>().sprite;
        _image = Resources.Load<Sprite>("Image/1x/돋보기"); //여기는 ItemID에 따라 자동으로 스프라이트 지정해주는 거
        
        btn.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        animator.SetTrigger(trigger_animation);
    }
}
