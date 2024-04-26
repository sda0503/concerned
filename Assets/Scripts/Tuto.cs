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
        GameManager.Instance.Playerinformation.position = 711;
        Invoke("TutoStart", 1f);
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
           
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            //transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            //transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
            showImageBool = true;
        }
    }
}
