using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map11 : MonoBehaviour
{
    public Button onIdentificationList;
    public GameObject identificationList;

    private void Start()
    {
        onIdentificationList.onClick.AddListener(() => { identificationList.SetActive(true); });
    }


}
