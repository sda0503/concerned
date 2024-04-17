using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class  interactableObject : MonoBehaviour
{
    protected Button btn;

    protected virtual void Start()
    {
        btn = gameObject.GetComponent<Button>();
    }
}
