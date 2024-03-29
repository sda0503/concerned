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

    //Awake�� �����ϰ� date�� ���� button�� ��Ȱ��ȭ�ϴ� ���� ���ƺ���.
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
            DialogueManager.instance.StartDialogue(click_calendarDates[GameManager.Instance.Playerinformation.date]); //ȥ�㸻 ����
            //gameObject.SetActive(true); //��¥ �ٲ�� UI������.
        }
    }

    private void MeetCharacter(int date)
    {
        Debug.Log(date);
        Debug.Log(charactername);
        click_calendarDates.Add(date, charactername);
        calendarDates[date].transform.Find("character").gameObject.SetActive(true);
        //Ŭ���ϸ� ĳ���� ��� �������� ����. -> ��ȭ�� �ϰ� �ִ� �ι��� ������ �����;���. -> ���? Tagetname �޾ƿͼ� charactername�� ���� -> �̹��� �־��ֱ�.
        //���� ����� �� ���� �Ǹ� �۵��ϵ��� ���� �ʿ�. -> �̰� ����� �����Ű�����. ����� ������ ���������� ������ �ϴ���, ȥ�㸻�� ���� �� �հ� �ϴ���, �ڵ��� �˸����� �������.
        gameObject.SetActive(false);
        GameManager.Instance.DateChange(); //�ӽ�
    }

    //ä������ �����°�(onbutton == true)�� �ڵ��� Ŭ������ �����°�(onbutton == false)�� ������ ������� �ʿ䰡 ����
    private void OffButton() 
    {
        date = GameManager.Instance.Playerinformation.date;

        for (int i = predate; i < date; i++)
        {
            calendarDates[i].GetComponent<Button>().enabled = false;
            calendarDates[i].transform.Find("pass").gameObject.SetActive(true); //�����ٴ� �ǹ̿��� xǥ�� �ϴ� �̹��� ��쵵�� �ϴ� �͵� ���� �� ����. 
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
