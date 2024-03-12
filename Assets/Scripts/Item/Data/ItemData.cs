using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public int item_id;
    public string item_name;
    public string default_description;
    public string simple_description;
    public ItemEventType itemEventType;
    public ItemType itemType;
    public string event_description;
}
