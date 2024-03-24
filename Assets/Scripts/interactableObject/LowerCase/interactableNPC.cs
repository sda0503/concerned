using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactableNPC : interactableObject
{
    public string TargetName;
    [SerializeField] private Button btn;

    private void Start()
    {
        btn.onClick.AddListener(()=>DialogueManager.instance.StartDialogue(TargetName));
    }
}
