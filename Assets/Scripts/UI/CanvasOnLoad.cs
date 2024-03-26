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
    public List<bool> states;

    private void OnEnable()
    {
           interactList = gameObject.GetComponentsInChildren<interactableObject>();
           states = new List<bool>(interactList.Length);
           for (int i=0;i<states.Capacity;i++)
           {
               states.Add(false);
           }
           //하위에 있는것만 찾으려면 GetComponentInChildern
    }

    /// <summary>
    /// TODO : bool 리스트 순서대로 오브젝트가 있고, 해당하는 오브젝트가 동작할 때 bool리스트의 값을 변경해줘야됨
    /// 오브젝트의 ON/Off 여부를 담당하는 메서드
    /// </summary>
    /// <param name="objectsState"></param>
    public void ObjectSet(List<bool> objectsState)  
    /* TODO : 세팅할 때 Bool을 어떻게 정의, 관리해야하는가.     
     * NPC : On/Off 조건은 이 사람이 계속 있을 수도 있고, 아니면 시간대에 따라 바뀔 수도 있음. 그 조건 설정을 어떻게 연계시킬까
     * Item : 클릭해서 확인하거나, 인벤토리에 들어가거나 하면 꺼지는걸로?      
     * NPC 세팅 자체를 더 세분화 해야될 것 같음. 따로 오더가 없어도 조건을 걸어서 알아서 On/Off 될 수 있도록 설정.
     * 아이템의 경우는 사용하면 true값으로 바꿔주면 될 것 같음.
     * 그러면 List를 분리해서 받아야되나?
     */
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
                        if (interactableItem.ItemType == ItemType.Normal) //일반 아이템은 회수하면 더 이상 등장하지 않음.
                        {
                            interactableItem.gameObject.GetComponent<Button>().onClick.AddListener(()=>Utility.Instance.OnClickToFindItem(interactableItem.ItemId,DataManager.instance.itemCanvas));
                            
                        }
                        else if (interactableItem.ItemType == ItemType.Trigger) //트리거 아이템은 다시 등장함.
                        {
                            interactableItem.gameObject.GetComponent<Button>().onClick.AddListener(()=>Utility.Instance.OnClickToFindTriggerItem(interactableItem.ItemId,DataManager.instance.itemCanvas));
                            
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
    
    
    
    public void OnDisable()
    {
        //TODO : 버튼 리스너 해제
        foreach (var VARIABLE in interactList)
        {
            if (VARIABLE.gameObject.TryGetComponent(out interactableNPC interactableNpc))
            {
                interactableNpc.gameObject.GetComponent<Button>().onClick.RemoveListener(()=> DialogueManager.instance.StartDialogue(interactableNpc.TargetName));
            }
            else if (VARIABLE.gameObject.TryGetComponent(out interactableItem interactableItem))
            {
                interactableItem.gameObject.GetComponent<Button>().onClick.RemoveListener(()=>Utility.Instance.OnClickToFindItem(interactableItem.ItemId,DataManager.instance.itemCanvas));
            }
        }
    }
}
