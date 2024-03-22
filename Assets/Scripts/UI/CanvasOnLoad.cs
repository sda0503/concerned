using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasOnLoad : MonoBehaviour
{
    //TODO : 버튼 다 달아주기.
    [SerializeField] private Button asdf;
    private INteractableObject[] interactList;

    private void OnEnable()
    {
           interactList = gameObject.GetComponentsInChildren<INteractableObject>();
           //하위에 있는것만 찾으려면 GetComponentInChildern
    }

    /// <summary>
    /// TODO : bool 리스트 순서대로 오브젝트가 있고, 해당하는 오브젝트가 동작할 때 bool리스트의 값을 변경해줘야됨
    /// 오브젝트의 ON/Off 여부를 담당하는 메서드
    /// </summary>
    /// <param name="objectsState"></param>
    public void ObjectSet(List<bool> objectsState) 
    {
        for (int i = 0; i < interactList.Length; i++)
        {
            if (!objectsState[i])
            {
                if (interactList[i].gameObject.TryGetComponent(out INteractableObject _))//컨벤션적으로 _는 안쓰는 변수를 의미
                // TODO : INteractableObject 말고 하위에 있는 Character나 button 등을 가져올 것.
                {
                    //TODO : interactList[i].GetComponent<Button>().onClick.AddListener(() => 사용할 메서드(매개변수 : out에 있는 데이터를 가지고 있는 스크립트에서 꺼내쓸 것));
                }
                interactList[i].gameObject.SetActive(true);
                
            }
            else
            {
                interactList[i].gameObject.SetActive(false);
            }
        }
        //TODO : 리스트에 오브젝트들 추가. 
    }

    public List<bool> StateSet()
    {
        return new List<bool>(interactList.Length);
    }
    
    //TODO : 초기 세팅이랑 재활용하는 부분에대해서 작성
    
    
    
    // public void OnDisable()
    // {
    //     //TODO : 버튼 리스너 해제
    //     foreach (var VARIABLE in interactList)
    //     {
    //         if (VARIABLE.TryGetComponent(캐릭터))
    //         {
    //             //VARIABLE.gameObject.GetComponent<Button>().onClick.RemoveListener();
    //             return;
    //         }
    //         else if (VARIABLE.TryGetComponent(퍼즐))
    //         {
    //             //VARIABLE.gameObject.GetComponent<Button>().onClick.RemoveListener();
    //         }
    //         else if (VARIABLE.TryGetComponent(모시깽))
    //         {
    //             //VARIABLE.gameObject.GetComponent<Button>().onClick.RemoveListener();
    //         }
    //     }
    // }
}
