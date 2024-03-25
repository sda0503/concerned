using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map0 : MonoBehaviour
{
    public List<Button> NPCs = new List<Button>();

    private void Start()
    {
        SetButtonKey();
    }

    private void SetButtonKey()
    {
        for (int i = 0; i < NPCs.Count; i++)
        {
            NPCs[i].onClick.RemoveAllListeners();
            int n = i;
            NPCs[i].onClick.AddListener(() => OnButtonClick(n));
        }
    }


    private void OnButtonClick(int index)
    {
        switch (index)
        {
            case 0:
            case 1:

                break;
            case 2:

                break;
            case 3:
            case 4:

                break;
            case 5:
                break;
            case 6:
                break;
        }
    }
}
