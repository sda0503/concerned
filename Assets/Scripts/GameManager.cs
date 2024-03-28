using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DataStorage;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    

    #region 뭔지 몰라도 조건이 많을것같아서 만듬

    //TODO :관련된 UI들 값 변경할수있도록 연결할 것
    public event Action OnDateChange; //날짜 변경 
    public event Action OnDayTimeChange; //시간 변경
    public event Action OnPositionChange; //장소 변경
    
    #endregion
    public Dictionary<int, GameObject> CanvasGroup = new Dictionary<int, GameObject>(); //ID값으로 캔버스 저장.
    private CanvasDic _canvasDic = new CanvasDic();

    [SerializeField] private GameObject canvasparents;
    [SerializeField] private Canvas bgCanvas;
    private Image bgImage; 
    
    [SerializeField] private GameObject origin;
    [SerializeField] private Text _Datetext;

    private string BGFilePath = "Image/map";

    //TODO : Save 관련된 기능들도 event로 묶어서 관리

    [SerializeField] private Canvas _canvas;

    public Information Playerinformation;

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

    
    private void Start()
    {
        CanvasOnLoad canvasOnLoad = _canvas.GetComponent<CanvasOnLoad>();
        canvasOnLoad.ObjectSet(new List<bool>{false,false,false});
        bgImage = bgCanvas.GetComponent<Image>();
        DateUpdate();
        //test        
        CanvasGroup.Add(-1,origin);
        Playerinformation.position = -1;
    }

    public void DateChange()
    {
        Playerinformation.date++;
        OnDateChange?.Invoke(); //UIUpdate (날짜표시)
        DateUpdate();
        if (Playerinformation.date == 30)
        {
            //TODO : 선택하는 씬(엔딩)으로 넘어가야됨
        }
    }

    void DateUpdate() //TODO : UI변경에 관한 부분은 모두 UIManager로 넘길 것
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(Playerinformation.date.ToString());
        sb.Append($"일차 {GetDayTime()}");
        _Datetext.text = sb.ToString();
    }

    public void DayTimeChange()
    {
        //TODO : DayTime 기준으로 저녁이면 집밖에 못가거나 강제로 보내거나 결정해야함.
        //TODO : 일단 지도 상으로 이동했을 때 시간 증가하는걸로 
        
        int daytime = (int)Playerinformation.dayTime + 1;
        if (daytime == 3)
            DateChange();
        Playerinformation.dayTime = (Information.DayTimeenum)(daytime % 3);
        DateUpdate();
        //나머지연산자로 하면 if없이 순환시킬 수 있다.

        OnDayTimeChange?.Invoke(); //UIUpdate //시간대 (조명, 휴대폰이나 다른 UI)
    }
    
    private string GetDayTime() //정서에 맞게 변환
    {
        switch (Playerinformation.dayTime)
        {
            case Information.DayTimeenum.Evening:
                return "오전";
            case Information.DayTimeenum.Afternoon:
                return "오후";
            case Information.DayTimeenum.Night:
                return "저녁";
        }
        return "";
    }

    public void PositionChange(int PosID) 
        //엑셀로 관리 : 기획자나 다른사람이 관리하기에 편함 협업에 유리 . 코드로 관리 : 개발자가 전체 관리하면 해도됨 근데 컴파일 되는거신경써야됨.
    {
        origin.gameObject.SetActive(false);
        CanvasGroup[Playerinformation.position].SetActive(false);
        Playerinformation.position = PosID;
        CanvasChange();
        //OnPositionChange?.Invoke(); //UIUpdate 아마 BG 바꾸는 용도로 사용될 듯.
        //TODO : 위치 바뀌면 시간대 바뀌는걸로 추가.
    }
    //TODO : 이걸 맵 내부에서 이동하는거에 재사용 하려면? 1. Map 켜서 이동하는 PopupBtn에 DayTimeChange 달아주어서 분리하면 되긴 함.
    // 2. 1번이 제일 나은 듯. 굳이 이게 어디서 왔는지 따져서 갈라줄 필요는 없다고 생각함.

    /// <summary>
    /// 캔버스 변경할 때 사용하는 메서드
    /// 1. 첫번째 게임의 경우
    /// 해당 장소에 방문한 적이 있다면 재활용, 없다면 새로 생성 (오브젝트에 관한 데이터도 새로 생성)
    /// 2. 불러오기의 경우
    /// 캔버스의 해당 오브젝트들의 끄고 켜지는 데이터를 Load해서 재사용할 예정.
    /// </summary>
    public void CanvasChange() //결국 방 이동하는 것도 캔버스 체인지인가?
    {
        //TODO : 캔버스를 세팅하는 부분에 오브젝트에 관한 Bool값을 가지는 데이터를 사용해보자.
        //TODO : 해당하는 캔버스가 없으면 Instanciate, 있으면 꺼져있을테니 다시 키는걸로. => 현재 있는거랑 동일한건지 체크해줘야됨.
        string path = DataManager.instance.PlaceDBDatas.PlaceDB[Playerinformation.position].Place_Path; //방끼리 이동.
        if (!CanvasGroup.ContainsKey(Playerinformation.position)) 
            //캔버스 그룹이라는 실제 오브젝트가 존재하는 dic에 ID값을 가진 캔버스가 없으면, TODO : 로직에 맞게 수정 필요
        {
            var obj = Resources.Load<GameObject>($"Prefabs/{path}");
            var canvasinstance = Instantiate(obj, canvasparents.transform);
            canvasinstance.SetActive(true);
             if (canvasinstance.gameObject.TryGetComponent(out CanvasOnLoad canvasOnLoad))
             {
                 CanvasGroup.Add(Playerinformation.position, canvasinstance);
                 _canvasDic.CanvasContorllers.Add(Playerinformation.position, canvasOnLoad.states);
                 canvasOnLoad.ObjectSet(_canvasDic.CanvasContorllers[Playerinformation.position]);
             }
        }
        else
        {
            CanvasGroup[Playerinformation.position].SetActive(true);
            //TODO : Load하는 경우 그에 맞게 데이터로 세팅해주는 것도 필요함.
            //예시 : CanvasGroup의 ID에 맞는 오브젝트의 컴포넌트에 접근해서 ObjectSet이라는 메서드를 실행
            CanvasGroup[Playerinformation.position].GetComponent<CanvasOnLoad>().ObjectSet(_canvasDic.CanvasContorllers[Playerinformation.position]);
            //아예 다 하이어라키에 올려놓는 것도 방법이겠지만, 가능하면 위 방법으로 진행하자.
        }

        bgImage.sprite = Resources.Load<Sprite>($"{BGFilePath}/{Playerinformation.dayTime.ToString()}/{path}");
        //TODO : 순서대로 배경폴더/시간대폴더/배경이름
        //TODO : 맵 배경 바뀌는 부분은 조건에 상관없이 동작. + 시간대 따져야됨
        //bgImage.sprite = Resources.Load<Sprite>("Image/map/map3"); //맵 배경 바뀌는 부분은 조건에 상관없이 동작.
        //TODO : 리소스는 계속 읽는게 아니라 한 번로드 해놓고 재사용하는 것.
    }

  

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