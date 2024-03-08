using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���������Ʈ�� ������ �����̹Ƿ� ����� ��.
[CreateAssetMenu(fileName = "DefaultItemInfo", menuName = "Item/Default", order = 0)]
public class ItemSO : ScriptableObject
{
    public ItemType itemType;//
    public int ID;
    public Sprite Image;
    public string Name;
    public string Description; //������ ����
    public string Location; //������ ���� ����
    public bool check_get = false; //true�̸� ����UI�� ���̵��� ����
}
