using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class map5 : MonoBehaviour
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
                    Utility.Instance.OnClickToFindItem(15, DataManager.instance.itemCanvas);
                }
                else Debug.Log("nnnn.");
                break;
            case 2:
                Utility.Instance.OnClickToFindItem(17, DataManager.instance.itemCanvas);
                break;
            case 3:
                Utility.Instance.OnClickToFindItem(33, DataManager.instance.itemCanvas);
                break;
        }
    }
}
