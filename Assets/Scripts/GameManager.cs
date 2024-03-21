using System;
using System.Collections;
using System.Collections.Generic;
using DataStorage;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private Information Playerinformation;  //TODO : 일단은 New

    #region 뭔지 몰라도 조건이 많을것같아서 만듬
    //TODO :관련된 UI들 값 변경할수있도록 연결할 것
    public event Action OnDateChange; //날짜 변경 
    public event Action OnDayTimeChange; //시간 변경
    public event Action OnPositionChange; //장소 변경

    #endregion
     
    //TODO : Save 관련된 기능들도 event로 묶어서 관리

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    public Transform itemCanvas;

    //test
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;

    public ItemDataList saveGetItems = new ItemDataList();

    private void Awake()
    {
        ItemDataManager.Instance.LoadDefaultData();
        ItemManager.Instance.getItems.Clear();
    }

    private void Start()
    {
        button1.onClick.AddListener(() => Utility.Instance.OnClickToFindTriggerItem(1, itemCanvas));
        button3.onClick.AddListener(() => Utility.Instance.OnClickToFindItem(1, itemCanvas));
        button4.onClick.AddListener(() => Utility.Instance.OnClickToFindItem(5, itemCanvas));
        button5.onClick.AddListener(() => Utility.Instance.OnClickToFindItem(2, itemCanvas));
        button2.onClick.AddListener(() => OffGame());

        //TODO : 여기서 로드할지 말지 결정. 새로하기 버튼이나 그런걸로 하면 될듯
    }

    private void OffGame()
    {
        Utility.Instance.SaveData(saveGetItems, "SaveItem");
    }

    public void DateChange()
    {
        Playerinformation.date++;
        OnDateChange?.Invoke(); //UIUpdate (날짜표시)
        if (Playerinformation.date == 30)
        {
            //TODO : 선택하는 씬으로 넘어가야됨
        }
    }

    public void DayTimeChange()
    {
        //TODO : DayTime 기준으로 저녁이면 집밖에 못가거나 강제로 보내거나 결정해야함.

        int daytime = (int)Playerinformation.dayTime + 1;
        if (daytime == 3)
            DateChange();
        Playerinformation.dayTime = (Information.DayTimeenum)(daytime % 3);
        //나머지연산자로 하면 if없이 순환시킬 수 있다.
        
        
        OnDayTimeChange?.Invoke(); //UIUpdate //시간대 (조명, 휴대폰이나 다른 UI)
    }

    public void PositionChange(int PosID) //엑셀로 관리 : 기획자나 다른사람이 관리하기에 편함 협업에 유리 . 코드로 관리 : 개발자가 전체 관리하면 해도됨 근데 컴파일 되는거신경써야됨.
    {
        Playerinformation.position = PosID;
        OnPositionChange?.Invoke(); //UIUpdate 아마 BG 바꾸는 용도로 사용될 듯.
    }
    
    
}