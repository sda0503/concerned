using System.Collections;
using System.Collections.Generic;

using DataStorage;
using TMPro;
using UnityEngine;

public class ChatLogSet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NameTxt;
    [SerializeField] private TextMeshProUGUI LogTxt;

    public void LogSetting(string name, string log)
    {
        NameTxt.text = name;
        LogTxt.text = log;
    }
}
