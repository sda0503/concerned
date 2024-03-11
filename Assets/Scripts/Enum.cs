using GoogleSheet.Core.Type;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum ���� script
[UGS(typeof(ItemEventType))]
public enum ItemEventType
{
    Nomal, 
    Event, 
    Usable, //usable�� ����� �����ų� Ŭ���� �ѹ� �� �ϸ� ������鼭 �ȿ� ������ �������� �������� ����
    Interaction //��ȣ�ۿ��� ������ ������. �κ��丮�� ���� ����.
}

[UGS(typeof(ItemType))]
public enum ItemType
{
    Photo, Object
}

public enum LocationEvent //�������� ������ Ư�� ��ҿ��� Ž���ϸ� �̺�Ʈ �ߵ��ǵ���
{
    House, Police, Office
}