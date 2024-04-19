using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Tuto : MonoBehaviour
{
    public string TargetName;

    bool showImageBool = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("TutoStart", 3f);

        DialogueManager.Instance.TargetNameChange += showImage;
    }

    void TutoStart()
    {
        DialogueManager.Instance.StartDialogue(TargetName);
    }

    void showImage()
    {
        if (!showImageBool)
        {
            Debug.Log("¥Î»≠ ≥°");
            transform.GetChild(0).gameObject.SetActive(true);
            showImageBool = true;
        }
    }
}
