using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBtn : MonoBehaviour
{
    
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Move(int num)
    {
        gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }
}
