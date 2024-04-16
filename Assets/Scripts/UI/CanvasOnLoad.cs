using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasOnLoad : MonoBehaviour
{
    //TODO : 버튼 다 달아주기.
    //[SerializeField] private Button asdf;
    private interactableObject[] interactList;
    public List<bool> states; //TODO : 게임매니저에서 통으로 관리예정...일까 초기값이 없다.

    private void OnEnable()
    {
        if (states == null)
        {
            interactList = gameObject.GetComponentsInChildren<interactableObject>();
            states = new List<bool>(interactList.Length);
            for (int i=0;i<states.Capacity;i++)
            {
                states.Add(false);
            }
            //하위에 있는것만 찾으려면 GetComponentInChildern    
        }
    }
    
    public void ObjectSet(List<bool> objectsState)  
    
    {
        for (int i = 0; i < interactList.Length; i++)
        {
            if (!objectsState[i])
            {
                if (interactList[i].gameObject.TryGetComponent(out interactableObject _))//컨벤션적으로 _는 안쓰는 변수를 의미
                // TODO : INteractableObject 말고 하위에 있는 Character나 button 등을 가져올 것.
                {
                    //interactList[i].GetComponent<Button>().onClick.AddListener(() => 사용할 메서드(매개변수 : out에 있는 데이터를 가지고 있는 스크립트에서 꺼내쓸 것));
                    if (interactList[i].gameObject.TryGetComponent(out interactableNPC interactableNpc))
                    {
                        //interactableNpc.gameObject.GetComponent<Button>().onClick.AddListener(()=> DialogueManager.instance.StartDialogue(interactableNpc.TargetName));
                    }
                    else if (interactList[i].gameObject.TryGetComponent(out interactableItem interactableItem))
                    {
                        //아이템 넣는거 한번만 실행되려면 얘는 동적으로 해주는게 맞음.
                        interactableItem.gameObject.GetComponent<Button>().onClick.AddListener(() => DataManager.Instance.OnClickToFindItem(interactableItem.ItemId));
                        //일반 아이템은 회수하면 더 이상 등장하지 않음.
                        if (interactableItem.ItemType == ItemType.Trigger) //트리거 아이템은 다시 등장함.
                        {
                            int a = i; //TODO : 임시 테스트용 나중에 변경할 것.
                            interactableItem.gameObject.GetComponent<Button>().onClick.AddListener(()=> StateChange(a));
                        }
                    }
                    
                    //TODO : 대화가 필요하면 DialogueManager의 StartDialogue와 연결, Utility의 OnClickToFindItem이랑 연결, 트리거 아이템은 또 다르게 연결해야함. 분별점 찾을 것.
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

    private void StateChange(int i)
    {
        states[i] = true;
    }

    public List<bool> StateSet()
    {
        List<bool> state = new List<bool>(interactList.Length);
        return state;
    }
    
    //TODO : 초기 세팅이랑 재활용하는 부분에대해서 작성
    
    
    
    // public void OnDisable()
    // {
    //     //TODO : 버튼 리스너 해제
    //     foreach (var VARIABLE in interactList)
    //     {
    //         if (VARIABLE.gameObject.TryGetComponent(out interactableNPC interactableNpc))
    //         {
    //             interactableNpc.gameObject.GetComponent<Button>().onClick.RemoveListener(()=> DialogueManager.instance.StartDialogue(interactableNpc.TargetName));
    //         }
    //         else if (VARIABLE.gameObject.TryGetComponent(out interactableItem interactableItem))
    //         {
    //             interactableItem.gameObject.GetComponent<Button>().onClick.RemoveListener(()=>Utility.Instance.OnClickToFindItem(interactableItem.ItemId,DataManager.instance.itemCanvas));
    //         }
    //     }
    // }
}
