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
    //�̰� ä�ú��ٴ� ��ü �α� ���ó�� �����̶� �̸�, ��� ��, ��ȭ��ȣ, �ֹε�Ϲ�ȣ, ª�� ������� ������ �� �� ����.
}
