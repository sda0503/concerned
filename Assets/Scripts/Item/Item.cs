using DataStorage;

public class Item
{
    //���� ������ ����ϴ� ������
    //id�� ������ �ְ� id�� ���ؼ� �������� ������ �ҷ���.
  

    public int id;
    public ItemData itemData;
    
    public bool event_check =false;

    public Item(int id)
    {
        itemData = DataManager.instance.GetDefaultItemDataList().Data[id];
        this.id = id;
        event_check = false;
    }
}
