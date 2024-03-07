using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum 모음 script
public enum ItemType
{
    Nomal, Event
}

public enum LocationEvent //아이템을 가지고 특정 장소에서 탐색하면 이벤트 발동되도록
{
    House, Police, Office
}