using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemInfo", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    public int ID;
    public string Name;
    public string Description; //������ ����
    public string Location; //������ ���� ����
}
