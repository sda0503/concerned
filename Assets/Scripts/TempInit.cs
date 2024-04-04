using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempInit : MonoBehaviour
{
    void Start()
    {
        DataManager.Instance.init();
    }
}
