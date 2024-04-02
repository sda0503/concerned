using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactableNPC : interactableObject
{
    public string TargetName;

    [SerializeField] private bool[] check = new bool[3];
    //시간대에 따라 On할것인지 OFF할것인지 결정.

    protected override void Start()
    {
        base.Start();
        btn.onClick.AddListener(()=>DialogueManager.Instance.StartDialogue(TargetName));
        GameManager.Instance.OnDayTimeChange += onoff;
    }

    void onoff()
    {
        if (check[(int)DataManager.Instance.Playerinformation.dayTime])
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
