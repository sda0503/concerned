using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    public Sprite[] images = new Sprite[4];

    private void Start()
    {
        gameObject.GetComponent<Image>().sprite = images[Random.Range(0,4)];
        gameObject.GetComponent<Button>().onClick.AddListener(OnClickBook);
    }

    private void OnClickBook()
    {
        Destroy(gameObject);
    }
}
