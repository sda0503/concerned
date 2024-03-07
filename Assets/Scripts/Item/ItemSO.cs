using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemInfo", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    public int ID;
    public string Name;
    public string Description; //아이템 설명
    public string Location; //아이템 간단 설명
}
