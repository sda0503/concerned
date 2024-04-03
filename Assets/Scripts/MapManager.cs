using System.Collections.Generic;
using DataStorage;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    private GraphicRaycaster _gr;
    private PointerEventData _ped;
    private List<RaycastResult> _rrList;
    private bool _isMouseOver = false;

    private GameObject _obj;

    private PlaceDBDatas _placeDB;

    public GameObject _marker;
    public GameObject _popup;
    public List<(Vector2,string,int)> _mapDate = new List<(Vector2,string,int)>();

    public void Awake()
    {
        _gr = GetComponent<GraphicRaycaster>();
        _ped = new PointerEventData(null);
        _rrList = new List<RaycastResult>();

         // var obj = Resources.Load(모시깽 DB 파일 이름);
         // _placeDB = JsonConvert.DeserializeObject<PlaceDB>(obj);
        
        //TODO : Value를 바꿔야될수도? _placeDB의 Name값 가져와서 세팅 
        _mapDate.Add((new Vector2(396, 1080 + -539), "경찰서",700));
        _mapDate.Add((new Vector2(1684, 1080 + -363), "인적 드문 숲",300));
        _mapDate.Add((new Vector2(1245, 1080 + -446), "택배회사",900));
        _mapDate.Add((new Vector2(594, 1080 + -756), "빗테크 오피스",210));
        _mapDate.Add((new Vector2(646, 1080 + -270), "빗테크 오피스텔",100));
        _mapDate.Add((new Vector2(692, 1080 + -885), "병원 & 장례식장",600));
        _mapDate.Add((new Vector2(331, 1080 + -950), "양현서의 집",110));
        _mapDate.Add((new Vector2(505, 1080 + -322), "변호사 사무실",500));
        _mapDate.Add((new Vector2(1731, 1080 + -578), "신현우의 집",800));
        _mapDate.Add((new Vector2(548, 1080 + -171), "탐정사무소",400));


        //자기 있는 곳에 또 다시 방문할 수도 있음.
        for (int i = 0; i < _mapDate.Count; i++)
        {
            var _newMark = Instantiate(_marker, _mapDate[i].Item1, Quaternion.identity, gameObject.transform);
            _newMark.GetComponent<Button>().onClick.AddListener(ClickMarker);
            _newMark.transform.GetChild(0).gameObject.SetActive(false);
            _newMark.transform.GetChild(1).gameObject.SetActive(false);
            _newMark.transform.GetChild(2).gameObject.SetActive(false);
            _newMark.transform.GetChild(1).GetComponent<Text>().text = _mapDate[i].Item2;
            _newMark.transform.GetChild(2).GetComponent<Text>().text = _mapDate[i].Item3.ToString();
        }
        var _pp = Instantiate(_popup, new Vector3(960,540,0), Quaternion.identity, gameObject.transform);
        _pp.SetActive(false);
    }
    
    void Update()
    {
        _ped.position = Input.mousePosition;
        OnPointerOver();
        OnPointerExit();
    }

    public GameObject GetClickedUIObject()
    {
        _rrList.Clear();

        _gr.Raycast(_ped, _rrList);

        if (_rrList.Count == 0)
        {
            return null;
        }
        return _rrList[0].gameObject;
    }
        
    public void OnPointerOver()
    {
        if (!_isMouseOver)
        {
            _obj = GetClickedUIObject();
            if(_obj != null && _obj.tag != "Popup") 
            { 
                _obj.transform.GetChild(0).gameObject.SetActive(true);
                _obj.transform.GetChild(1).gameObject.SetActive(true);
                _obj.transform.GetChild(2).gameObject.SetActive(true);
                _isMouseOver = true;
            }
        }
    }

    public void OnPointerExit()
    {
        if (_isMouseOver && _obj != null)
        {
            if (GetClickedUIObject() != null) return;
            _obj.transform.GetChild(0).gameObject.SetActive(false);
            _obj.transform.GetChild(1).gameObject.SetActive(false);
            _obj.transform.GetChild(2).gameObject.SetActive(false);
            _isMouseOver = false;
        }
    }

    public void ClickMarker() //여기가 PopUp띄우는부분.
    {
        var popupUI = transform.GetChild(12).gameObject;
        popupUI.SetActive(true);
        if (!popupUI.TryGetComponent(out PopupBtn popupBtn))
        {
            Debug.Log("에러 발생 : 컴포넌트가 없음.");
            return;
        }
        popupBtn.posID = int.Parse(_obj.transform.GetChild(2).GetComponent<Text>().text);
        string _popupText = _obj.transform.GetChild(1).gameObject.GetComponent<Text>().text;
        popupUI.transform.GetChild(1).GetComponent<Text>().text = _popupText;
    }
}
