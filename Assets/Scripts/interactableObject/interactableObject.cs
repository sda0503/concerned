using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class  interactableObject : MonoBehaviour
{
    protected Button btn;

    protected virtual void Start()
    {
        btn = gameObject.GetComponent<Button>();
    }
    //전체 오브젝트에 공통될 내용만 인터페이스에 작성하고, 분화되는 것들은 다른 클래스로 만들어서 인터페이스 상속하여 사용할 것.
    //버튼, NPC, 아이템이나 퍼즐 
}
