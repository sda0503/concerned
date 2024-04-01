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
                ItemManager.Instance.OnClickToFindItem(0, UIManager.instance.itemCanvas);
                break;
            case 2:
                ItemManager.Instance.OnClickToFindItem(34, UIManager.instance.itemCanvas);
                break;
            case 3:
                ItemManager.Instance.OnClickToFindTriggerItem(2, UIManager.instance.itemCanvas);

                ItemManager.Instance.GetItem(7);
                break;
        }
    }
}
