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
    private bool[] click_calendarDates = Enumerable.Repeat(true, 30).ToArray();
    private bool onbutton;

    //Awake�� �����ϰ� date�� ���� button�� ��Ȱ��ȭ�ϴ� ���� ���ƺ���.
    private void Awake()
    {
        MakeSlots();
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

    private void MeetCharacter(int date)
    {
        Debug.Log(date);
        Debug.Log(charactername);
        click_calendarDates[date] = false;
        //Ŭ���ϸ� ĳ���� ��� �������� ����. -> ��ȭ�� �ϰ� �ִ� �ι��� ������ �����;���. -> ���? Tagetname �޾ƿͼ� charactername�� ���� -> �̹��� �־��ֱ�.
        //���� ����� �� ���� �Ǹ� �۵��ϵ��� ���� �ʿ�. -> �̰� ����� �����Ű�����. ����� ������ ���������� ������ �ϴ���, ȥ�㸻�� ���� �� �հ� �ϴ���, �ڵ��� �˸����� �������.
    }

    //ä������ �����°�(onbutton == true)�� �ڵ��� Ŭ������ �����°�(onbutton == false)�� ������ ������� �ʿ䰡 ����
    private void OffButton() 
    {
        date = GameManager.Instance.Playerinformation.date; //�̺�Ʈ�� �Ǿ��ֱ� ������ ������ �ϴ� �͵� ������ �ʴٰ� ������.
        if (onbutton)
        {
            //�̹� ������ ���ϰ� ���� ���� �н��ϰ� ���Ӱ� �ϴ°� ���� �� ����
            for (int i = predate; i < date; i++)
            {
                click_calendarDates[i] = false;
                calendarDates[i].GetComponent<Button>().enabled = false;
                //�����ٴ� �ǹ̿��� xǥ�� �ϴ� �̹��� ��쵵�� �ϴ� �͵� ���� �� ����. 
            }
            if (date == 0)
            {
                for (int i = date; i < calendarDates.Length; i++)
                {
                    calendarDates[i].GetComponent<Button>().enabled = click_calendarDates[i];
                }
            }
            else if (date > 0) 
            { 
                for (int i = date - 1; i < calendarDates.Length; i++)
                {
                    calendarDates[i].GetComponent<Button>().enabled = click_calendarDates[i];
                }
            }
        }
        else
        {
            for (int i = predate; i < calendarDates.Length; i++)
            {
                calendarDates[i].GetComponent<Button>().enabled = false;
                //�����ٴ� �ǹ̿��� xǥ�� �ϴ� �̹��� ��쵵�� �ϴ� �͵� ���� �� ����.
            }
        }
        if (predate > 0)
        {
            predate = date - 1;
        }
    }

    public void SetButton(bool b)
    {
        onbutton = b;
        OffButton();
    }
}
