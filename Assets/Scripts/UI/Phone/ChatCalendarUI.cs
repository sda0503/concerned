using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatCalendarUI : PopupUIBase
{
    public Transform dataTransform;
    public GameObject dayPrefab;
    private int date;
    private int predate = 0;
    public string charactername;
    private GameObject[] calendarDates = new GameObject[31];

    //Awake�� �����ϰ� date�� ���� button�� ��Ȱ��ȭ�ϴ� ���� ���ƺ���.
    //date 0 ���� ����
    private void Awake()
    {
        MakeSlots();
    }

    private void OnEnable()
    {
        OffButton();
    }

    private void MakeSlots()
    {
        for (int i = 0; i < calendarDates.Length; i++)
        {
            calendarDates[i] = Instantiate(dayPrefab, dataTransform);
            calendarDates[i].GetComponentInChildren<TextMeshProUGUI>().text = i.ToString("00");
            if(i < 16)
            {
                int n = i;
                calendarDates[i].GetComponent<Button>().onClick.AddListener(() => MeetCharacter(n));
            }
            else
            {
                calendarDates[i].transform.GetChild(1).gameObject.SetActive(true);
                calendarDates[i].transform.GetChild(1).GetComponent<Image>().color = Color.black;
            }
        }
        calendarDates[0].transform.GetChild(2).gameObject.SetActive(false);
    }

    private void MeetCharacter(int date)
    {
        Debug.Log("Click");
        GameManager.Instance.CharacterMeetDate(date, charactername);
        calendarDates[date].GetComponent<Button>().enabled = false;
        gameObject.SetActive(false);
    }

    private void OffButton()
    {
        date = GameManager.Instance.Playerinformation.date + 1;

        for (int i = predate; i < date; i++)
        {
            calendarDates[i].transform.GetChild(0).gameObject.SetActive(false);
            calendarDates[i].GetComponent<Button>().enabled = false;
        }

        calendarDates[date].GetComponent<Button>().enabled = false;
        calendarDates[date].transform.GetChild(0).gameObject.SetActive(true);

        predate = date;
    }
}
