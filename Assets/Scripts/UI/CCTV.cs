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
    //�̰� CCTV ������ ���� ȸ��ó�� �����ִ°ɷ�?

}
