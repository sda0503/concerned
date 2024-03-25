using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map7 : MonoBehaviour
{
    public List<Button> buttons;

    private void Start()
    {
        SetButtonKey();
    }

    private void SetButtonKey()
    {
        for (int i = 0; i < buttons.Count; i++)
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
            case 0:
                Utility.Instance.OnClickToFindItem(0, GameManager.Instance.itemCanvas);
                break;
            case 2:
                Utility.Instance.OnClickToFindItem(34, GameManager.Instance.itemCanvas);
                break;
            case 3:
                Utility.Instance.OnClickToFindTriggerItem(2, GameManager.Instance.itemCanvas);
                ItemManager.Instance.GetItem(7);
                break;
        }
    }
}
