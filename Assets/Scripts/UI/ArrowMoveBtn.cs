using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMoveBtn : MonoBehaviour
{
    [SerializeField] private string ArrowName;
    public int posID;

    public void ArrowMove()
    {
        GameManager.Instance.PositionChange(posID); //TODO : ���� PosID������ �̵�
    }
}
