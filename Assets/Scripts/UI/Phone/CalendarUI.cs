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

    //Awake로 생성하고 date에 맞춰 button을 비활성화하는 것이 좋아보임.
    //date 0 부터 시작
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


    //잡힌 약속은 그 날이 되면 작동하도록 설정 필요. -> 이건 어렵지 않을거같긴함. 약속이 있으면 경찰서에서 시작을 하던가, 혼잣말로 나올 수 잇게 하던가, 핸드폰 알림으로 만들던가.
    private void MeetCharacterCallback()
    {
        Debug.Log("check");
        if (click_calendarDates.ContainsKey(GameManager.Instance.Playerinformation.date))
        {
            Debug.Log(GameManager.Instance.Playerinformation.date);
            Debug.Log("D - day");
            //DialogueManager.Instance.StartDialogue(click_calendarDates[GameManager.Instance.Playerinformation.date]); //혼잣말 버전
        }
    }
    

    private void CheckSchedule(int date)
    {
        Debug.Log(date);
        dateText.text = "양력 4월 " + date.ToString("00") + "일";
        if(click_calendarDates.ContainsKey(date))
        {
            explain.text = click_calendarDates[date] + "약속";
        }
        else { explain.text = "약속 없음"; }
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
