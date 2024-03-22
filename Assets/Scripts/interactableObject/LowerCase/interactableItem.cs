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

    public int ItemId;
    //Type으로 trigger랑 분류해도 될 듯?

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        btn.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        animator.SetTrigger(trigger_animation);
    }
}
