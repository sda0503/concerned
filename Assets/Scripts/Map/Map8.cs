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
                Utility.Instance.OnClickToFindTriggerItem(7, DataManager.instance.itemCanvas);
                break;
            case 1:
                Utility.Instance.OnClickToFindItem(2, DataManager.instance.itemCanvas);
                break;
            case 2:
                Utility.Instance.OnClickToFindItem(3, DataManager.instance.itemCanvas);
                break;
            case 3:
                Utility.Instance.OnClickToFindItem(4, DataManager.instance.itemCanvas);
                break;
        }
    }
}
