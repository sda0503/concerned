using DataStorage;

public class Item
{
    public int id;
    public ItemData itemData;
    
    public bool event_check =false;

    public Item(int id)
    {
        itemData = DataManager.Instance.GetDefaultItemDataList().Data[id];
        this.id = id;
        event_check = false;
    }
}
