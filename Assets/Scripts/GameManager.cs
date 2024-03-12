using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //test
    public Button button1;
    public Button button2;

    private void Awake()
    {
        ItemDataManager.Instance.LoadDefaultData();
        ItemManager.Instance.getItems.Clear();
    }

    private void Start()
    {
        button1.onClick.AddListener(() => Utility.Instance.OnClickToFindItem(1));
        button2.onClick.AddListener(() => ItemDataManager.Instance.SaveData());
    }
}
