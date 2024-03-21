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
                else Debug.Log("잠겨있다.");
                //없으면 잠겨있다는 문구 나오게 하기.
                break;
            case 2:
                Utility.Instance.OnClickToFindItem(17, GameManager.Instance.itemCanvas);
                break;
            case 3:
                //Utility.Instance.OnClickToFindItem(33, GameManager.Instance.itemCanvas); 열쇠 얻는 곳 33번은 아직 Json에 없으므로 생략
                Debug.Log("열쇠");
                break;
        }
    }
}
