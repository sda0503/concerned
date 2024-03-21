using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class map6 : MonoBehaviour
{
    public List<Button> buttons;

    private void Start()
    {
        SetButtonKey();
    }

    private void SetButtonKey()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            buttons[i].onClick.RemoveAllListeners();
            int n = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(n));
        }
    }


    private void OnButtonClick(int index)
    {
        switch (index)
        {
            case 1:
                if (ItemManager.Instance.getItems.ContainsKey(33))
                {
                    Utility.Instance.OnClickToFindItem(15, GameManager.Instance.itemCanvas);
                }
                else Debug.Log("����ִ�.");
                //������ ����ִٴ� ���� ������ �ϱ�.
                break;
            case 2:
                Utility.Instance.OnClickToFindItem(17, GameManager.Instance.itemCanvas);
                break;
            case 3:
                //Utility.Instance.OnClickToFindItem(33, GameManager.Instance.itemCanvas); ���� ��� �� 33���� ���� Json�� �����Ƿ� ����
                Debug.Log("����");
                break;
        }
    }
}
