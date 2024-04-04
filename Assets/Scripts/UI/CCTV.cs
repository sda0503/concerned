using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : PopupUIBase
{
    public Transform Position;

    private void OnEnable()
    {
        OnSet(300, 400, Position);
    }
    //이거 CCTV 누르면 과거 회상처럼 보여주는걸로?

}
