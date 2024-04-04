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
            int n = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(n));
        }
    }


    public void OnButtonClick(int index)
    {
        switch (index)
        {
            case 1:
                if (DataManager.Instance.getItems.ContainsKey(33))
                {
                    DataManager.Instance.OnClickToFindItem(15);
                }
                else Debug.Log("nnnn.");
                break;
            case 2:
                DataManager.Instance.OnClickToFindItem(17);
                break;
            case 3:
                DataManager.Instance.OnClickToFindItem(33);
                break;
        }
    }
}
