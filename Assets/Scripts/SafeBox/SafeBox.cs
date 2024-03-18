using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeBox : MonoBehaviour
{
    public string correctPassword = "1791"; 
    private string inputPassword = "";
    public Text passwordText;


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
        }
        else
        {
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
