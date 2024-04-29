using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalendarUI : MonoBehaviour
{
    public Transform dataTransform;
    public GameObject dayPrefab;
    public TextMeshProUGUI explain;
    public TextMeshProUGUI dateText;
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
            calendarDates[i].GetComponentInChildren<TextMeshProUGUI>().text = i.ToString("00");
            if(i < 16)
            {
                int n = i;
                calendarDates[i].GetComponent<Button>().onClick.AddListener(() => CheckSchedule(n));
            }
            else
            {
                calendarDates[i].transform.GetChild(1).gameObject.SetActive(true);
                calendarDates[i].transform.GetChild(1).GetComponent<Image>().color = Color.black;
            }
        }
        calendarDates[0].transform.GetChild(2).gameObject.SetActive(false);
    }


    //잡힌 약속은 그 날이 되면 작동하도록 설정 필요. -> 이건 어렵지 않을거같긴함. 약속이 있으면 경찰서에서 시작을 하던가, 혼잣말로 나올 수 잇게 하던가, 핸드폰 알림으로 만들던가.
    private void MeetCharacterCallback()
    {
        if (click_calendarDates.ContainsKey(GameManager.Instance.Playerinformation.date + 1))
        {
            Debug.Log("D - day");
            PopupUIManager.Instance.popupUI["PhoneUI"].SetActive(true);
            PopupUIManager.Instance.popupUI["PhoneUI"].transform.GetChild(8).gameObject.SetActive(true);

            //경찰 소환조사 세팅
            UIManager.Instance.characterComponent.SetActive(true);
            UIManager.Instance.characterComponent.GetComponent<interactableNPC>().TargetName = click_calendarDates[GameManager.Instance.Playerinformation.date + 1] + "- 소환조사";
            switch (click_calendarDates[GameManager.Instance.Playerinformation.date + 1])
            {
                case "강민우 ":
                    UIManager.Instance.characterComponent.GetComponent<Image>().sprite = DataManager.Instance.SpriteLoad("Image/Charters/kangminoo_mini2");
                    break;
                case "흥신소 탐정 ":
                    UIManager.Instance.characterComponent.GetComponent<Image>().sprite = DataManager.Instance.SpriteLoad("Image/Charters/detective");
                    break;
                case "김태현 ":
                    UIManager.Instance.characterComponent.GetComponent<Image>().sprite = DataManager.Instance.SpriteLoad("Image/Charters/KimTaehyun");
                    break;
                case "신현우 ":
                    UIManager.Instance.characterComponent.GetComponent<Image>().sprite = DataManager.Instance.SpriteLoad("Image/Charters/ShinHyunwoo");
                    break;
            }
        }
    }
    

    private void CheckSchedule(int date)
    {
        Debug.Log(date);
        dateText.text = "양력 4월 " + date.ToString("00") + "일";
        if(click_calendarDates.ContainsKey(date))
        {
            explain.text = click_calendarDates[date] + " 약속";
        }
        else { explain.text = "약속 없음"; }
    }

    private void MoveDate()
    {
        date = GameManager.Instance.Playerinformation.date + 1;

        for (int i = predate; i < date; i++)
        {
            if(i > 0) calendarDates[i].GetComponentInChildren<TextMeshProUGUI>().color = color;
            calendarDates[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        calendarDates[date].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
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
            GameManager.Instance.check_name.Clear();
        }
    }
}
