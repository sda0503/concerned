using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //test
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(() => Utility.Instance.OnClickToFindItem(1));
    }
}
