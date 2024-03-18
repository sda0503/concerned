using System.Collections;
using System.Collections.Generic;
using System.Text;
using DataStorage;
using TMPro;
using UnityEngine;

public class ChatLogSet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NameTxt;
    [SerializeField] private TextMeshProUGUI LogTxt;

    public void LogSetting(chatlogData data)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(data.Name);
        sb.Append(" : ");
        NameTxt.text = sb.ToString();
        LogTxt.text = data.Log;
    }
}
