using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DataStorage;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : SingletonBase<GameManager>
{
    #region 뭔지 몰라도 조건이 많을것같아서 만듬

    //TODO :관련된 UI들 값 변경할수있도록 연결할 것
    public event Action OnDateChange; //날짜 변경 
    public event Action OnDayTimeChange; //시간 변경
    public event Action OnPositionChange; //장소 변경
    
    #endregion
    
   
    
    [SerializeField] private GameObject origin;
  

   
    
   
    //TODO : Save 관련된 기능들도 event로 묶어서 관리
    public Information Playerinformation = new Information();
    
    public override void init()
    {
        //CanvasOnLoad canvasOnLoad = _canvas.GetComponent<CanvasOnLoad>();
        //canvasOnLoad.ObjectSet(new List<bool>{false,false,false});
        
        //OnDateChange.Invoke(); //TODO : 나중에 정리되면 삭제
        //CanvasGroup.Add(-1,origin);
        //Playerinformation.position = -1;
    }

    public void DateChange()
    {
        Playerinformation.date++;
        OnDateChange?.Invoke(); //UIUpdate (날짜표시)
        if (Playerinformation.date == 30)
        {
            //TODO : 선택하는 씬(엔딩)으로 넘어가야됨
        }
    }

   

    public void DayTimeChange()
    {
        //TODO : DayTime 기준으로 저녁이면 집밖에 못가거나 강제로 보내거나 결정해야함.
        //TODO : 일단 지도 상으로 이동했을 때 시간 증가하는걸로 
        
        int daytime = (int)Playerinformation.dayTime + 1;
        if (daytime == 3)
            DateChange();
        Playerinformation.dayTime = (Information.DayTimeenum)(daytime % 3);
        //나머지연산자로 하면 if없이 순환시킬 수 있다.

        OnDayTimeChange?.Invoke(); //UIUpdate //시간대 (조명, 휴대폰이나 다른 UI)
    }
    
   

    public void PositionChange(int PosID) 
        //엑셀로 관리 : 기획자나 다른사람이 관리하기에 편함 협업에 유리 . 코드로 관리 : 개발자가 전체 관리하면 해도됨 근데 컴파일 되는거신경써야됨.
    {
        Playerinformation.position = PosID;
        OnPositionChange?.Invoke(); //UIUpdate 아마 BG 바꾸는 용도로 사용될 듯.
        //TODO : 위치 바뀌면 시간대 바뀌는걸로 추가.
    }
    //TODO : 이걸 맵 내부에서 이동하는거에 재사용 하려면? 1. Map 켜서 이동하는 PopupBtn에 DayTimeChange 달아주어서 분리하면 되긴 함.
    // 2. 1번이 제일 나은 듯. 굳이 이게 어디서 왔는지 따져서 갈라줄 필요는 없다고 생각함.

    
   

  

    //전체 갈수있는 방을 List로 넣어놓고 ex)왼쪽으로 -1, 오른쪽+1
    //TODO : 캔버스 불러오는거 작성, 캔버스에 들어갈 스크립트도 짜야되고, 각 버튼들 스크립트도 생각해야됨. 캔버스에 직렬화해서 버튼들 달아주고 enabled랑 disabled일때
    //TODO : 방 이동하는 건 버튼에 스크립트에 public으로 int달아줘서 맵 주소 달아주기.
    //TODO : 제일 최근생각 : 어차피 버튼에 동적으로 달건 달고, 아닌건 안함 => 장소끼리 이동하는 경우는 0번 위치로만 가고 내부 이동은 버튼에 미리 path 다 달아서 사용하자.
    //Path : 아파트/아파트입구[0] // 낮/점심/저녁
    //Resources.Load<Sprite>($"{Playerinformation.dayTime.ToString()}/{PositionDB[Playerinformation.position]}/0"); //이런 느낌으로 Path 지정
    
    //각각 동작 달아주기.
    //TODO : 선결조건이 달성안되면 안 나온게한다. 특정 인물이 회피한다? Random 떄려야될듯.
    //TODO : 신현우(택배기사) 얘가 안나오는 날이 시작됬다. count++ => 1; 2; if (count ==2) => 무조건 출근; count=0;
    //TODO : 시간대별로 NPC 위치 ON/OFF 하는 거 만들어야됨. enum으로 넣기? 3개 아침 점심 저녁 아침 점심 => 회사 / 저녁 => 집
}