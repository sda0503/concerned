using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//스프레드시트로 관리할 예정이므로 없어두 됨.
[CreateAssetMenu(fileName = "DefaultItemInfo", menuName = "Item/Default", order = 0)]
public class ItemSO : ScriptableObject
{
    public ItemType itemType;//
    public int ID;
    public Sprite Image;
    public string Name;
    public string Description; //아이템 설명
    public string Location; //아이템 간단 설명
    public bool check_get = false; //true이면 도감UI에 보이도록 설정
}
