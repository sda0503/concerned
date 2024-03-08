using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EventItemInfo", menuName = "Item/Event", order = 1)]
public class EventItemSO : ItemSO
{
    public LocationEvent locationEvent;
    public string event_description; //�̺�Ʈ �ߵ� �� ������ ���� �κ� ����.
    public bool check_event = false;
}
