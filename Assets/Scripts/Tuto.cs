using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto : MonoBehaviour
{
    public string TargetName;
    // Start is called before the first frame update
    void Start()
    {
        DialogueManager.Instance.StartDialogue(TargetName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
