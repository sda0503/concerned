using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class CanvasOnLoad : MonoBehaviour
{
    //TODO : 버튼 다 달아주기.
    //[SerializeField] private Button asdf;
    private interactableObject[] interactList;
    [SerializeField] [CanBeNull] private GameObject itemObj;
    public List<bool> states; //TODO : 게임매니저에서 통으로 관리예정...일까 초기값이 없다.
    
    public void ObjectSet(List<bool> objectsState)  
    
    {
        for (int i = 0; i < objectsState.Count; i++)
        {
            itemObj.transform.GetChild(i).gameObject.SetActive(!objectsState[i]);
        }
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
