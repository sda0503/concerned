using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class Utility
{
    private static Utility instance;
    //������Ƽ�� �̿��� �̱��� -> AddComponent������ �ʾƵ� ����� ������.
    public static Utility Instance
    {
        get
        {
            if (instance == null)
                instance = new Utility();

            return instance;
        }
    }

    public void OnClickToFindItem(int index)
    {
        var obj = Resources.Load("Item") as GameObject;
        
        obj.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("image"); //index�� ���缭 �̹��� �ε�ǵ��� ����

        Object.Instantiate(obj);
    }
}
