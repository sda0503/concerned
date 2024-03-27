using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMoveBtn : interactableObject
{
    [SerializeField] private int TargetPosID;
    protected override void Start()
    {
        base.Start();
        btn.onClick.AddListener(()=>GameManager.Instance.PositionChange(TargetPosID));
    }
}
