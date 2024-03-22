using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupBtn : MonoBehaviour
{
    public string pos;
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Move()
    {
        gameObject.SetActive(false);
        GameManager.Instance.PositionChange(1);
        transform.parent.gameObject.SetActive(false);
    }
}
