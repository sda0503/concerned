using GoogleSheet.Core.Type;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum ���� script

[UGS(typeof(ItemType))]
public enum ItemType
{
    Nomal, Event
}

public enum LocationEvent //�������� ������ Ư�� ��ҿ��� Ž���ϸ� �̺�Ʈ �ߵ��ǵ���
{
    House, Police, Office
}