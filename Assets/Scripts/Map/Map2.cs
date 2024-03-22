using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map2 : MonoBehaviour
{
    public List<Button> NPCs;

    private void Start()
    {
        SetButtonKey(); //� NPC�� ������ ���� ä���� ��������
    }

    private void SetButtonKey()
    {
        for (int i = 0; i < NPCs.Count; i++)
        {
            NPCs[i].onClick.RemoveAllListeners();
            NPCs[i].onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        
    }
}
