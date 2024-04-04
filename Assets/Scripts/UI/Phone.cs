using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : PopupUIBase
{
    public Transform Position;

    private void OnEnable()
    {
        OnSet(200, 300, Position);
    }
    //폰은 내용 만들어서 통화내역이랑 문자 내역이랑 갤러리 정도만 만들면 되지 않을까 싶음.
}
