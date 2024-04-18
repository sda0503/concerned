using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CalendarUI : PopupUIBase
{
    public Transform dataTransform;
    public GameObject dayPrefab;
    public Text explain;
    public Text dateText;
    private int date;
    private int predate = 0;
    private GameObject[] calendarDates = new GameObject[31];
    private Dictionary<int, string> click_calendarDates = new Dictionary<int, string>();

    Color color = new Color32(152, 152, 152, 255);

    //Awake�� �����ϰ� date�� ���� button�� ��Ȱ��ȭ�ϴ� ���� ���ƺ���.
    //date 0 ���� ����
    private void Awake()
    {
        MakeSlots();
        GameManager.Instance.OnDateChange += MeetCharacterCallback;
    }

    private void OnEnable()
    {
        MoveDate();
        CheckSchedule(GameManager.Instance.Playerinformation.date + 1);
        CharacterMeetSchedule();

    }

    private void MakeSlots()
    {
        for (int i = 0; i < calendarDates.Length; i++)
        {
            calendarDates[i] = Instantiate(dayPrefab, dataTransform);
            calendarDates[i].GetComponentInChildren<Text>().text = i.ToString("00");
            int n = i;
            calendarDates[i].GetComponent<Button>().onClick.AddListener(() => CheckSchedule(n));
        }
        calendarDates[0].transform.GetChild(2).gameObject.SetActive(false);
    }


    //���� ����� �� ���� �Ǹ� �۵��ϵ��� ���� �ʿ�. -> �̰� ����� �����Ű�����. ����� ������ ���������� ������ �ϴ���, ȥ�㸻�� ���� �� �հ� �ϴ���, �ڵ��� �˸����� �������.
    private void MeetCharacterCallback()
    {
        Debug.Log("check");
        if (click_calendarDates.ContainsKey(GameManager.Instance.Playerinformation.date))
        {
            Debug.Log(GameManager.Instance.Playerinformation.date);
            Debug.Log("D - day");
            //DialogueManager.Instance.StartDialogue(click_calendarDates[GameManager.Instance.Playerinformation.date]); //ȥ�㸻 ����
        }
    }
    

    private void CheckSchedule(int date)
    {
        Debug.Log(date);
        dateText.text = "��� 4�� " + date.ToString("00") + "��";
        if(click_calendarDates.ContainsKey(date))
        {
            explain.text = click_calendarDates[date] + "���";
        }
        else { explain.text = "��� ����"; }
    }

    private void MoveDate()
    {
        date = GameManager.Instance.Playerinformation.date + 1;

        for (int i = predate; i < date; i++)
        {
            if(i > 0) calendarDates[i].GetComponentInChildren<Text>().color = color;
            calendarDates[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        calendarDates[date].GetComponentInChildren<Text>().color = Color.white;
        calendarDates[date].transform.GetChild(0).gameObject.SetActive(true);

        predate = date;
    }

    private void CharacterMeetSchedule()
    {
        if(GameManager.Instance.check_date.Count > 0)
        {
            for (int i = 0; i < GameManager.Instance.check_date.Count; i++)
            {
                click_calendarDates.Add(GameManager.Instance.check_date[i], GameManager.Instance.check_name[i]);
                calendarDates[GameManager.Instance.check_date[i]].transform.GetChild(1).gameObject.SetActive(true);
            }
            GameManager.Instance.check_date.Clear();
        }
    }
}
