using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberButton : MonoBehaviour
{
    public int number; 
    public SafeBox Password;
    private Button button;

    public void OnButtonClick()
    {
        Password.EnterDigit(number);
        
    }

    private void Start()
    {
        if (button == null)
        {
            button= GetComponent<Button>();
        }
        button.onClick.AddListener(OnButtonClick);
    }
}
