using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DataStorage;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //TODO : 다채로운 버튼,UI들의 향연 
     
    /// <summary>
    //TODO : 직접 내가 버튼이랑 UI 다 달아주는것보다 Interface나 컴포넌트를 활용해서 Type으로 찾아오는 편이 더 좋게 먹힐 수 도 있음.
    //TODO : 그리고 event로 콜백 처리하는는 부분들이 UI 같은 경우에는 간단할 수도 있는데, 대화 들어갈 때 뒤에 클릭할 수 있는 애들 꺼지는 부분도 관리 해줘야되는 부분이기때문에
    //TODO : 간단하거나 수가 적은건 callback으로 해결하면 되고 나머지는 인터페이스를 상속해 선조치 후통보 (값 바꿔놓고 그 후에 해당하는 객체들을 돌면서 필요한 동작 실행시키기)
    /// </summary>
    public static UIManager instance;

    private StringBuilder sb = new StringBuilder();

    [SerializeField] private Button btn;
    [SerializeField] private GameObject Map; 
    
    public Dictionary<int, GameObject> CanvasGroup = new Dictionary<int, GameObject>(); 
    //ID값으로 캔버스 저장. TODO : 프리팹기준으로 빈공간이라도 생성하는거 필요함. 세팅할 때 
    private CanvasDic _canvasDic = new CanvasDic();

    [SerializeField] private GameObject canvasparents;
    [SerializeField] private Canvas bgCanvas;
    private Image bgImage;
    
    private string BGFilePath = "Image/map";
    
    public Transform itemCanvas;

    private Information playerinformation = DataManager.instance.Playerinformation;
    
   

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    

    void Start()
    {
        //TODO : 버튼에 애드리스너해서 메서드 달아주기
        //각 장소가 그 주소의 BG에 해당하는 ID값을 가지고 있어야함.
        //TODO : PosBtn.OnClick.AddListener(() => MovePosition(PosBtn.주소 관련된 스크립트나 컴포넌트.PosID));
        //ID를 받으면 DB를 뒤져서 그에 맞는 파일 Path를 가지고 BG를 갈아끼운다? 그부분은 MovePosition에 있어야될듯.
        btn.onClick.AddListener(OpenMap);
        bgImage = bgCanvas.GetComponent<Image>();
        GameManager.Instance.OnDateChange += DateUpdate;
        GameManager.Instance.OnDayTimeChange += DateUpdate;
        GameManager.Instance.OnPositionChange += CanvasChange;
        GameManager.Instance.OnPositionChange += itemCanvaschange;
    }
    
    void DateUpdate() //TODO : UI변경에 관한 부분은 모두 UIManager로 넘길 것
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(playerinformation.date.ToString());
        sb.Append($"일차 {GetDayTime()}");
        //_Datetext.text = sb.ToString(); //TODO : GameManager에서 옮겨오기.
    }
    
    private string GetDayTime() //정서에 맞게 변환
    {
        switch (playerinformation.dayTime)
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
    
    
    /// <summary>
    /// 캔버스 변경할 때 사용하는 메서드
    /// 1. 첫번째 게임의 경우
    /// 해당 장소에 방문한 적이 있다면 재활용, 없다면 새로 생성 (오브젝트에 관한 데이터도 새로 생성)
    /// 2. 불러오기의 경우
    /// 캔버스의 해당 오브젝트들의 끄고 켜지는 데이터를 Load해서 재사용할 예정.
    /// </summary>
    public void CanvasChange() //결국 방 이동하는 것도 캔버스 체인지인가?
    {
        CanvasGroup[playerinformation.position].SetActive(false); //현재 캔버스 끄기.
        //TODO : 캔버스를 세팅하는 부분에 오브젝트에 관한 Bool값을 가지는 데이터를 사용해보자.
        //TODO : 해당하는 캔버스가 없으면 Instanciate, 있으면 꺼져있을테니 다시 키는걸로. => 현재 있는거랑 동일한건지 체크해줘야됨.
        string path = DataManager.instance.PlaceDBDatas.PlaceDB[playerinformation.position].Place_Path; //방끼리 이동.
        if (!CanvasGroup.ContainsKey(playerinformation.position))
        {
            var obj = Resources.Load<GameObject>($"Prefabs/{path}");
            var canvasinstance = Instantiate(obj, canvasparents.transform);
            canvasinstance.SetActive(true);
            if (canvasinstance.gameObject.TryGetComponent(out CanvasOnLoad canvasOnLoad))
            {
                CanvasGroup.Add(playerinformation.position, canvasinstance);
                _canvasDic.CanvasContorllers.Add(playerinformation.position, canvasOnLoad.states);
                canvasOnLoad.ObjectSet(_canvasDic.CanvasContorllers[playerinformation.position]);
            }
        }
        else
        {
            CanvasGroup[playerinformation.position].SetActive(true);
            //TODO : Load하는 경우 그에 맞게 데이터로 세팅해주는 것도 필요함.
            //예시 : CanvasGroup의 ID에 맞는 오브젝트의 컴포넌트에 접근해서 ObjectSet이라는 메서드를 실행
            CanvasGroup[playerinformation.position].GetComponent<CanvasOnLoad>().ObjectSet(_canvasDic.CanvasContorllers[playerinformation.position]);
            //아예 다 하이어라키에 올려놓는 것도 방법이겠지만, 가능하면 위 방법으로 진행하자.
        }

        bgImage.sprite = Resources.Load<Sprite>($"{BGFilePath}/{playerinformation.dayTime.ToString()}/{path}");
        //TODO : 순서대로 배경폴더/시간대폴더/배경이름
        //TODO : 맵 배경 바뀌는 부분은 조건에 상관없이 동작. + 시간대 따져야됨
        //bgImage.sprite = Resources.Load<Sprite>("Image/map/map3"); //맵 배경 바뀌는 부분은 조건에 상관없이 동작.
        //TODO : 리소스는 계속 읽는게 아니라 한 번로드 해놓고 재사용하는 것.
    }
    
    void itemCanvaschange()
    {
        itemCanvas = CanvasGroup[playerinformation.position].transform;
    }

    private void OpenMap()
    {
        Map.SetActive(true);
    }

    void DayTimeChange()
    {
        GameManager.Instance.DayTimeChange();
    }

    // void MovePosition(int PosID)
    // {
    //     
    // }

    void ChangeBG()
    {
        //BG.Sprite = Resources.Load<Sprite>(경로 + DB에서 찾은 파일 이름);
    }
}