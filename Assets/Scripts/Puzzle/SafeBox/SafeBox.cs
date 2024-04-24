using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SafeBox : MonoBehaviour
{
    public string correctPassword = ""; 
    private string inputPassword = "";
    public TextMeshProUGUI passwordText;

    public int item;


    public void EnterDigit(int digit)
    {
        if (inputPassword.Length < 4)
        {
            inputPassword += digit.ToString();
            UpdatePasswordText();
        }

        if (inputPassword.Length == 4)
        {
            CheckPassword();
        }
    }

    private void UpdatePasswordText()
    {
        passwordText.text = inputPassword;
    }

    private void CheckPassword()
    {
       
        if (inputPassword == correctPassword)
        {
            passwordText.text = "<color=#4682B4>OPEN</color>";
            DataManager.Instance.OnClickToFindItem(item);
        }
        else
        {
            GameManager.Instance.PuzzleCount--;
            passwordText.text = "ERROR";
            inputPassword = "";
        }
    }

    public void ClearPassword()
    {
        inputPassword = "";
        passwordText.text = "";
    }
}
