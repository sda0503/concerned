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


    //���� ����� �� ���� �Ǹ� �۵��ϵ��� ���� �ʿ�. -> �̰� ����� �����Ű�����. ����� ������ ���������� ������ �ϴ���, ȥ�㸻�� ���� �� �հ� �ϴ���, �ڵ��� �˸����� �������.
    private void MeetCharacterCallback()
    {
        if (click_calendarDates.ContainsKey(GameManager.Instance.Playerinformation.date + 1))
        {
            Debug.Log("D - day");
            PopupUIManager.Instance.popupUI["PhoneUI"].SetActive(true);
            PopupUIManager.Instance.popupUI["PhoneUI"].transform.GetChild(8).gameObject.SetActive(true);

            //���� ��ȯ���� ����
            UIManager.Instance.characterComponent.SetActive(true);
            UIManager.Instance.characterComponent.GetComponent<interactableNPC>().TargetName = click_calendarDates[GameManager.Instance.Playerinformation.date + 1] + "- ��ȯ����";
            switch (click_calendarDates[GameManager.Instance.Playerinformation.date + 1])
            {
                case "���ο� ":
                    UIManager.Instance.characterComponent.GetComponent<Image>().sprite = DataManager.Instance.SpriteLoad("Image/Charters/kangminoo_mini2");
                    break;
                case "��ż� Ž�� ":
                    UIManager.Instance.characterComponent.GetComponent<Image>().sprite = DataManager.Instance.SpriteLoad("Image/Charters/detective");
                    break;
                case "������ ":
                    UIManager.Instance.characterComponent.GetComponent<Image>().sprite = DataManager.Instance.SpriteLoad("Image/Charters/KimTaehyun");
                    break;
                case "������ ":
                    UIManager.Instance.characterComponent.GetComponent<Image>().sprite = DataManager.Instance.SpriteLoad("Image/Charters/ShinHyunwoo");
                    break;
            }
        }
    }
    

    private void CheckSchedule(int date)
    {
        Debug.Log(date);
        dateText.text = "��� 4�� " + date.ToString("00") + "��";
        if(click_calendarDates.ContainsKey(date))
        {
            explain.text = click_calendarDates[date] + " ���";
        }
        else { explain.text = "��� ����"; }
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
