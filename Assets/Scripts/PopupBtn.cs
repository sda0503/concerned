using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupBtn : MonoBehaviour
{
    public int posID;
    private Button closeBtn;
    private Button confirmBtn;

    private void Start()
    {
        closeBtn = transform.GetChild(3).GetComponent<Button>();
        confirmBtn = transform.GetChild(4).GetComponent<Button>();
        
        closeBtn.onClick.AddListener(Close);
        confirmBtn.onClick.AddListener(Move);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Move()
    {
        gameObject.SetActive(false);
        GameManager.Instance.DayTimeChange();
        GameManager.Instance.PositionChange(posID); //TODO : 추후 PosID값으로 이동
        transform.parent.gameObject.SetActive(false);
    }

    
}
