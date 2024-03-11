using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class Utility
{
    private static Utility instance;
    //프로퍼티를 이용한 싱글턴 -> AddComponent해주지 않아도 사용이 가능함.
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
        
        obj.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("image"); //index에 맞춰서 이미지 로드되도록 설정

        Object.Instantiate(obj);
    }
}
