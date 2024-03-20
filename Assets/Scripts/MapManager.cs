using System.Collections;
using System.Collections.Generic;
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

    public GameObject _marker;
    public GameObject _popup;
    public List<(Vector2,string)> _mapDate = new List<(Vector2,string)>();

    public void Awake()
    {
        _gr = GetComponent<GraphicRaycaster>();
        _ped = new PointerEventData(null);
        _rrList = new List<RaycastResult>();

        _mapDate.Add((new Vector2(396, 1080 + -539), "경찰서"));
        _mapDate.Add((new Vector2(1684, 1080 + -363), "인적 드문 숲"));
        _mapDate.Add((new Vector2(1245, 1080 + -446), "택배회사"));
        _mapDate.Add((new Vector2(594, 1080 + -756), "빗테크 오피스"));
        _mapDate.Add((new Vector2(646, 1080 + -270), "빗테크 오피스텔"));
        _mapDate.Add((new Vector2(692, 1080 + -885), "병원 & 장례식장"));
        _mapDate.Add((new Vector2(331, 1080 + -950), "양현서의 집"));
        _mapDate.Add((new Vector2(505, 1080 + -322), "변호사 사무실"));
        _mapDate.Add((new Vector2(1731, 1080 + -578), "신현우의 집"));
        _mapDate.Add((new Vector2(548, 1080 + -171), "탐정사무소"));


        for (int i = 0; i < _mapDate.Count; i++)
        {
            var _newMark = Instantiate(_marker, _mapDate[i].Item1, Quaternion.identity, gameObject.transform);
            _newMark.GetComponent<Button>().onClick.AddListener(ClickMarker);
            _newMark.transform.GetChild(0).gameObject.SetActive(false);
            _newMark.transform.GetChild(1).gameObject.SetActive(false);
            _newMark.transform.GetChild(1).GetComponent<Text>().text = _mapDate[i].Item2;
        }
        var _pp = Instantiate(_popup, new Vector3(960,540,0), Quaternion.identity, gameObject.transform);
        _pp.SetActive(false);
    }

    private void Start()
    {
        
    }


    // Update is called once per frame
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
            _isMouseOver = false;
        }
    }

    public void ClickMarker()
    {
        transform.GetChild(12).gameObject.SetActive(true);
        string _popupText = _obj.transform.GetChild(1).gameObject.GetComponent<Text>().text;
        transform.GetChild(12).gameObject.transform.GetChild(1).GetComponent<Text>().text = _popupText;
    }
}
