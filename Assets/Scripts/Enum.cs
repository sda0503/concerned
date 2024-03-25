//enum 모음 script
public enum ItemEventType
{
    Nomal, 
    Event, 
    Usable, //usable은 사용을 누르거나 클릭을 한번 더 하면 사라지면서 안에 숨겨진 아이템이 나오도록 설정
    Interaction //상호작용이 가능한 아이템. 인벤토리에 들어가지 않음.
} //TODO : 삭제 대기 중

public enum ItemType
{
    Normal, 
    Trigger
}

public enum LocationEvent //아이템을 가지고 특정 장소에서 탐색하면 이벤트 발동되도록
{
    House,
    Police,
    Office //TODO : 아이템 가지고 해당 위치로 가면 NPC 등장 등 이벤트 발생
}