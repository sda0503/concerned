using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EventItemInfo", menuName = "Item/Event", order = 1)]
public class EventItemSO : ItemSO
{
    public LocationEvent locationEvent;
    public string event_description; //이벤트 발동 시 아이템 설명 부분 변경.
    public bool check_event = false;
}
