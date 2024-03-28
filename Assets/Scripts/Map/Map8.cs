using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map8 : MonoBehaviour
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
                ItemManager.Instance.OnClickToFindTriggerItem(7, DataManager.instance.itemCanvas);
                break;
            case 1:
                ItemManager.Instance.OnClickToFindItem(2, DataManager.instance.itemCanvas);
                break;
            case 2:
                ItemManager.Instance.OnClickToFindItem(3, DataManager.instance.itemCanvas);
                break;
            case 3:
                ItemManager.Instance.OnClickToFindItem(4, DataManager.instance.itemCanvas);
                break;
        }
    }
}
