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
    //���� ���� ���� ��ȭ�����̶� ���� �����̶� ������ ������ ����� ���� ������ ����.
}
