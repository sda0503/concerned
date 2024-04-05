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
                DataManager.Instance.OnClickToFindItem(51);
                break;
            case 1:
                DataManager.Instance.OnClickToFindItem(2);
                break;
            case 2:
                DataManager.Instance.OnClickToFindItem(3);
                break;
            case 3:
                DataManager.Instance.OnClickToFindItem(4);
                break;
        }
    }
}
