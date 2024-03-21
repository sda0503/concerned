using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //TODO : 다채로운 버튼,UI들의 향연 
     
    /// <summary>
    //TODO : 직접 내가 버튼이랑 UI 다 달아주는것보다 Interface나 컴포넌트를 활용해서 Type으로 찾아오는 편이 더 좋게 먹힐 수 도 있음.
    //TODO : 그리고 event로 콜백 처리하는는 부분들이 UI 같은 경우에는 간단할 수도 있는데, 대화 들어갈 때 뒤에 클릭할 수 있는 애들 꺼지는 부분도 관리 해줘야되는 부분이기때문에
    //TODO : 간단하거나 수가 적은건 callback으로 해결하면 되고 나머지는 인터페이스를 상속해 선조치 후통보 (값 바꿔놓고 그 후에 해당하는 객체들을 돌면서 필요한 동작 실행시키기)
    /// </summary>
    public static UIManager instance;

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