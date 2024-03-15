using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour
{
    Button book;

    private void Start()
    {
        book = GetComponent<Button>();
        book.onClick.AddListener(OnClickBook);
    }

    private void OnClickBook()
    {
        Destroy(gameObject);
    }
}
