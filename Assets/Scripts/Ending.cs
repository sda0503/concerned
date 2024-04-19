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

    private int select_character = 0;

    event Action on_select_character;

    private void Start()
    {
        // ������ ���ؼ� TargetName�� �ٲ�� ���̵� �� �ƿ� �ǵ��� ����. OnSelectCharacter �ȿ� ���� ������ �� �� ����!
        for (int i = 0; i < buttons.Length; i++)
        {
            int n = i;
            buttons[i].onClick.AddListener(() => OnSelectCharacter(n));
        }
    }

    private void OnSelectCharacter(int i)
    {
        DialogueManager.Instance.StartDialogue("Ending");
        select_character = i;
        on_select_character += Coroutione;
    }

    private void OnSetBadEnding()
    {
        //if���� �̿��ؼ� ���� ������ ������ ��ȭ ���� 80% �̻����� Ȯ��
        switch (select_character)
        {
            case 1:
            case 2:
                DialogueManager.Instance.StartDialogue("BadEnding");
                break;
            case 3:
            case 4:
                if (DataManager.Instance.getItems.Count / DataManager.Instance.itemsData.Count > 0.8f) //&&
                {
                    DialogueManager.Instance.StartDialogue("RealEnding");
                }
                else DialogueManager.Instance.StartDialogue("NomalEnding");
                break;
        }
    }

    private void Coroutione()
    {
        StartCoroutine(PadeInPadeOut());
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
