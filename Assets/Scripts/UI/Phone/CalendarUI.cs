using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CalendarUI : PopupUIBase
{
    public Transform dataTransform;
    private int date;
    private int predate = 0;
    public string charactername;
    private GameObject[] calendarDates = new GameObject[30];
    private Dictionary<int, string> click_calendarDates = new Dictionary<int, string>();
    private bool onbutton;

    //Awake로 생성하고 date에 맞춰 button을 비활성화하는 것이 좋아보임.
    private void Awake()
    {
        MakeSlots();
        GameManager.Instance.OnDateChange += MeetCharacterCallback;
    }

    private void MakeSlots()
    {
        GameObject obj = Utility.Instance.GameObjectLoad("Prefabs/Date");
        for (int i = 0; i < calendarDates.Length; i++)
        {
            calendarDates[i] = Instantiate(obj, dataTransform);
            int n = i;
            calendarDates[i].GetComponent<Button>().onClick.AddListener(() => MeetCharacter(n));
        }
    }

    private void MeetCharacterCallback()
    {
        Debug.Log("check");
        if (click_calendarDates.ContainsKey(GameManager.Instance.Playerinformation.date))
        {
            Debug.Log(GameManager.Instance.Playerinformation.date);
            Debug.Log("D - day");
            DialogueManager.instance.StartDialogue(click_calendarDates[GameManager.Instance.Playerinformation.date]); //혼잣말 버전
            //gameObject.SetActive(true); //날짜 바뀌면 UI보여줌.
        }
    }

    private void MeetCharacter(int date)
    {
        Debug.Log(date);
        Debug.Log(charactername);
        click_calendarDates.Add(date, charactername);
        calendarDates[date].transform.Find("character").gameObject.SetActive(true);
        //클릭하면 캐릭터 약속 잡히도록 설정. -> 대화를 하고 있는 인물의 정보를 가져와야함. -> 어떻게? Tagetname 받아와서 charactername에 저장 -> 이미지 넣어주기.
        //잡힌 약속은 그 날이 되면 작동하도록 설정 필요. -> 이건 어렵지 않을거같긴함. 약속이 있으면 경찰서에서 시작을 하던가, 혼잣말로 나올 수 잇게 하던가, 핸드폰 알림으로 만들던가.
        gameObject.SetActive(false);
        GameManager.Instance.DateChange(); //임시
    }

    //채팅으로 열리는거(onbutton == true)랑 핸드폰 클릭으로 열리는거(onbutton == false)랑 구분을 지어야할 필요가 있음
    private void OffButton() 
    {
        date = GameManager.Instance.Playerinformation.date;

        for (int i = predate; i < date; i++)
        {
            calendarDates[i].GetComponent<Button>().enabled = false;
            calendarDates[i].transform.Find("pass").gameObject.SetActive(true); //지났다는 의미에서 x표시 하는 이미지 띄우도록 하는 것도 좋을 것 같음. 
        }

        calendarDates[date].GetComponent<Button>().enabled = false;

        if (onbutton)
        {
            for (int i = date + 1; i < calendarDates.Length; i++)
            {
                if (click_calendarDates.ContainsKey(i))
                {
                    calendarDates[i].GetComponent<Button>().enabled = false;
                }
                else
                {
                    calendarDates[i].GetComponent<Button>().enabled = true;
                }
            }
        }
        else
        {
            for (int i = date; i < calendarDates.Length; i++)
            {
                calendarDates[i].GetComponent<Button>().enabled = false;
            }
        }
        predate = date;
    }

    public void SetButton(bool b)
    {
        onbutton = b;
        OffButton();
    }
}
