using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum ���� script
public enum ItemType //�̰ɷ� trigger�� �ƴѰŶ� ����
{
    Photo, Object
}

public enum LocationEvent //�������� ������ Ư�� ��ҿ��� Ž���ϸ� �̺�Ʈ �ߵ��ǵ���
{
    House, Police, Office
}

public enum ItemEventType // ->
{
    Nomal,
    Event,
    Usable, //usable�� ����� �����ų� Ŭ���� �ѹ� �� �ϸ� ������鼭 �ȿ� ������ �������� �������� ����
    Interaction //��ȣ�ۿ��� ������ ������. �κ��丮�� ���� ����.
}