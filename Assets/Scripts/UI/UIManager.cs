using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DataStorage;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //TODO : 다채로운 버튼,UI들의 향연 

    /// <summary>
    //TODO : 직접 내가 버튼이랑 UI 다 달아주는것보다 Interface나 컴포넌트를 활용해서 Type으로 찾아오는 편이 더 좋게 먹힐 수 도 있음.
    //TODO : 그리고 event로 콜백 처리하는는 부분들이 UI 같은 경우에는 간단할 수도 있는데, 대화 들어갈 때 뒤에 클릭할 수 있는 애들 꺼지는 부분도 관리 해줘야되는 부분이기때문에
    //TODO : 간단하거나 수가 적은건 callback으로 해결하면 되고 나머지는 인터페이스를 상속해 선조치 후통보 (값 바꿔놓고 그 후에 해당하는 객체들을 돌면서 필요한 동작 실행시키기)
    /// </summary>
    private StringBuilder sb = new StringBuilder();

    [SerializeField] private Button btn;
    [SerializeField] private GameObject Map;
    [SerializeField] public GameObject ObjList;

    public static Dictionary<int, GameObject>
        CanvasGroup = new Dictionary<int, GameObject>(); //TODO : 이거 기준으로 캔버스 세팅해주기? Json하나 있어야될거같음.

    //ID값으로 캔버스 저장. TODO : 프리팹기준으로 빈공간이라도 생성하는거 필요함. 세팅할 때 
    private CanvasDic _canvasDic = new CanvasDic();

    [SerializeField] private List<GameObject> map_List;

    [SerializeField] private GameObject canvasparents;
    [SerializeField] private Canvas bgCanvas;
    private Image bgImage;

    [SerializeField] private TextMeshProUGUI _Datetext;
    

    public GameObject endingCredits;

    private string BGFilePath = "Image/map";

    public Transform itemCanvas;

    public GameObject characterComponent;

    private Information playerinformation;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //TODO : 버튼에 애드리스너해서 메서드 달아주기
        //각 장소가 그 주소의 BG에 해당하는 ID값을 가지고 있어야함.
        //TODO : PosBtn.OnClick.AddListener(() => MovePosition(PosBtn.주소 관련된 스크립트나 컴포넌트.PosID));
        //ID를 받으면 DB를 뒤져서 그에 맞는 파일 Path를 가지고 BG를 갈아끼운다? 그부분은 MovePosition에 있어야될듯.
        btn.onClick.AddListener(OpenMap);
        bgImage = bgCanvas.GetComponentInChildren<Image>();
        GameManager.Instance.OnDateChange += DateUpdate;
        GameManager.Instance.OnDayTimeChange += DateUpdate;
        GameManager.Instance.OnPositionChange += CanvasChange;
        GameManager.Instance.OnPositionChange += itemCanvaschange;
        playerinformation = DataManager.Instance.Playerinformation;
        //CanvasGroup.Clear();
        //Debug.Log(CanvasGroup.Count);
        DataManager.Instance.Playerinformation.canvasObjSet.Clear();
        CanvasGroupSet();
        //Debug.Log($"{CanvasGroup.Count}    1");
        CanvasChange();
    }
    
    public void DeleteListener()
    {
        btn.onClick.RemoveAllListeners();
        bgImage = null;
        GameManager.Instance.OnDateChange -= DateUpdate;
        GameManager.Instance.OnDayTimeChange -= DateUpdate;
        GameManager.Instance.OnPositionChange -= CanvasChange;
        GameManager.Instance.OnPositionChange -= itemCanvaschange;
        playerinformation = DataManager.Instance.Playerinformation;
        DataManager.Instance.Playerinformation.canvasObjSet.Clear();
    }

    private void CanvasGroupSet() //정규식 사용하여 Map들의 이름을 기준으로 CanvasGroup 세팅
    {
        string pattern = @"\d+";
        foreach (var VARIABLE in map_List)
        {
            MatchCollection index = Regex.Matches(VARIABLE.name, pattern);
            
            if (!CanvasGroup.ContainsKey(int.Parse(index[0].Value)))
            {
                CanvasGroup.Add(int.Parse(index[0].Value), VARIABLE);
                
                if (DataManager.Instance.GameState == Game_State.New)
                {
                    DataManager.Instance.Playerinformation.canvasObjSet.Add(int.Parse(index[0].Value),VARIABLE.GetComponent<CanvasOnLoad>().states);
                    //Debug.Log(DataManager.Instance.Playerinformation.canvasObjSet[int.Parse(index[0].Value)].Count);
                }
            }
            VARIABLE.SetActive(false);
        }
    }


    void DateUpdate() //TODO : UI변경에 관한 부분은 모두 UIManager로 넘길 것
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(playerinformation.date.ToString());
        sb.Append($"일차 {GetDayTime()}");
        sb.Append($"\n {playerinformation.position}");
        _Datetext.text = sb.ToString(); //TODO : GameManager에서 옮겨오기.
    }

    void DateUpdate(string pos) //TODO : UI변경에 관한 부분은 모두 UIManager로 넘길 것
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(GameManager.Instance.Playerinformation.date.ToString());
        sb.Append($"일차 {GetDayTime()}");
        sb.Append($"\n {pos}");
        _Datetext.text = sb.ToString(); //TODO : GameManager에서 옮겨오기.
    }


    private string GetDayTime() //정서에 맞게 변환
    {
        switch (GameManager.Instance.Playerinformation.daytime)
        {
            case DayTimeenum.Evening:
                return "오전";
            case DayTimeenum.Afternoon:
                return "오후";
            case DayTimeenum.Night:
                return "저녁";
        }

        return "";
    }

    public void Off_Current_Canvas()
    {
        CanvasGroup[GameManager.Instance.Playerinformation.position].SetActive(false); //현재 캔버스 끄기.
    }

    public void asdf()
    {
        Debug.Log(CanvasGroup.Count);
        if (CanvasGroup.Count != 28)
        {
            foreach (var VARIABLE in CanvasGroup)
            {
                Debug.Log(VARIABLE.Key);
            }
        }
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
        PlaceDB NextPlaceData = new PlaceDB();

        foreach (var VARIABLE in DataManager.Instance.PlaceDBDatas.PlaceDB)
        {
            if (GameManager.Instance.Playerinformation.date == 15 && !(GameManager.Instance.Playerinformation.position == 711)) GameManager.Instance.Playerinformation.position = 711;
            if (VARIABLE.Place_ID == GameManager.Instance.Playerinformation.position)
            {
                NextPlaceData = VARIABLE; //TODO : dic으로 바꾸긴해야함.        
            }
        }

        DateUpdate(NextPlaceData.Place_Name);


        //string Objpath = DataManager.Instance.PlaceDBDatas.PlaceDB[playerinformation.position].Place_OBJ_Path; //TODO : 이건 여기 필요 없음. 미리 깔아둘 때 필요한 거

        // if (!CanvasGroup.ContainsKey(playerinformation.position))
        // {
        //     var obj = Resources.Load<GameObject>($"Prefabs/{path}");
        //     var canvasinstance = Instantiate(obj, canvasparents.transform);
        //     canvasinstance.SetActive(true);
        //     if (canvasinstance.gameObject.TryGetComponent(out CanvasOnLoad canvasOnLoad))
        //     {
        //         CanvasGroup.Add(playerinformation.position, canvasinstance);
        //         _canvasDic.CanvasContorllers.Add(playerinformation.position, canvasOnLoad.states);
        //         canvasOnLoad.ObjectSet(_canvasDic.CanvasContorllers[playerinformation.position]);
        //     }
        // }
        // else
        // {
        foreach (var VARIABLE in CanvasGroup)
        {
            Debug.Log($"{VARIABLE.Key} | {VARIABLE.Value}");
        }
        CanvasGroup[GameManager.Instance.Playerinformation.position].SetActive(true);
        //TODO : Load하는 경우 그에 맞게 데이터로 세팅해주는 것도 필요함.
        //예시 : CanvasGroup의 ID에 맞는 오브젝트의 컴포넌트에 접근해서 ObjectSet이라는 메서드를 실행
        //CanvasGroup[playerinformation.position].GetComponent<CanvasOnLoad>().ObjectSet(_canvasDic.CanvasContorllers[playerinformation.position]);
        //아예 다 하이어라키에 올려놓는 것도 방법이겠지만, 가능하면 위 방법으로 진행하자.
        // }

        if (NextPlaceData.Map_Type == Map_Type.Change)
        {
            //Debug.Log($"{BGFilePath}/{NextPlaceData.Place_BG_Path}_{playerinformation.daytime.ToString()}");
            bgImage.sprite =
                Resources.Load<Sprite>(
                    $"{BGFilePath}/{NextPlaceData.Place_BG_Path}_{playerinformation.daytime.ToString()}");
        }
        else if (NextPlaceData.Map_Type == Map_Type.Unchange)
        {
            bgImage.sprite = Resources.Load<Sprite>($"{BGFilePath}/{NextPlaceData.Place_BG_Path}");
        }
    }

    void itemCanvaschange()
    {
        //itemCanvas = CanvasGroup[playerinformation.position].transform; //TODO : 변경 후 열어주기
    }

    private void OpenMap()
    {
        ObjList.SetActive(false);
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


    IEnumerator EndingCredits()
    {
        canvasparents.SetActive(false);
        Vector3 position = endingCredits.transform.GetChild(2).position;
        Vector3 move = new Vector3(960, position.y, 0);
        while (endingCredits.transform.GetChild(2).position.y < 3500f)
        {
            move.y += 1f;
            endingCredits.transform.GetChild(2).position = move;
            yield return null;
        }
        yield return null;
        //씬 강제 이동
        OutBtnClick();
    }

    public void StartEndingCredits()
    {
        StartCoroutine(EndingCredits());
    }

    public void GUIOn()
    {
        canvasparents.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void OutBtnClick()
    {
        SceneManager.LoadScene("StartScene");
        GameManager.Instance.Playerinformation = new Information();
        CanvasGroup.Clear();
        DeleteListener();
    }
}