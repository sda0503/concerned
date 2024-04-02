using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identification : PopupUIBase
{
    public Transform Position;

    private void OnEnable()
    {
        OnSet(100, 200, Position);
    }
    //이건 채팅보다는 전체 로그 출력처럼 사진이랑 이름, 사는 곳, 전화번호, 주민등록번호, 짧은 설명까지 넣으면 될 것 같음.
}
