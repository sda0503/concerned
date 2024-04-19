using DataStorage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public Button[] buttons = new Button[4];
    public Image blinkImage;

    private int select_character;

    private void Start()
    {
        // 구독을 통해서 TargetName이 바뀌면 페이드 인 아웃 되도록 설정. OnSelectCharacter 안에 구독 넣으면 될 거 같음!
        DialogueManager.Instance.TargetNameChange += OnEnding;

        for (int i = 0; i < buttons.Length; i++)
        {
            int n = i;
            buttons[i].onClick.AddListener(() => OnSelectCharacter(n));
        }
        Invoke("test", 3f);
    }

    private void test()
    {
        DialogueManager.Instance.StartDialogue("Ending");
    }

    private void OnSelectCharacter(int i)
    {
        select_character = i;
        for(int j = 0; j < buttons.Length; j++)
        {
            buttons[j].gameObject.SetActive(false);
        }
        DialogueManager.Instance.ChatOff();
        StartCoroutine(PadeInPadeOut());
    }

    private void OnSetBadEnding()
    {
        //if문을 이용해서 얻은 아이템 갯수와 대화 갯수 80% 이상인지 확인
        switch (select_character)
        {
            case 0:
            case 1:
                DialogueManager.Instance.StartDialogue("BadEnding");
                break;
            case 2:
            case 3:
                if (DataManager.Instance.getItems.Count / DataManager.Instance.itemsData.Count > 0.8f) //&&
                {
                    DialogueManager.Instance.StartDialogue("RealEnding");
                }
                else DialogueManager.Instance.StartDialogue("NomalEnding");
                break;
        }
    }

    private void OnEnding()
    {
        Debug.Log("End");
    }
    
    IEnumerator PadeInPadeOut()
    {
        Color color = blinkImage.color;
        while (blinkImage.color.a <= 1)
        {
            color.a += 3f / 255f;
            blinkImage.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        OnSetBadEnding();
    }
}
