using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUIManager
{
    //제네릭으로 받아서 popupUI 생성만 UI 버튼 내용은 UI마다 script넣어주기

    //UI 프리팹을 로드 -> 제네릭으로 바꿔서 공용으로 사용할 수 있도록 수정
    //딕셔너리 key 값 이용해서 없으면 생성하고 있으면 새로 만들어서 딕셔너리에 저장
    public Dictionary<string, GameObject> popupUI = new Dictionary<string, GameObject>();

    private static PopupUIManager instance;
    public static PopupUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PopupUIManager();
            }
            return instance;
        }
    }

    public T OpenPopupUI<T>() where T : PopupUIBase
    {
        return OpenPopupUI(typeof(T).Name) as T; //script이름 == resources이름
    }

    public PopupUIBase OpenPopupUI(string name)
    {
        var obj = Resources.Load("PopupUI/" + name, typeof(GameObject)) as GameObject;
        if (obj == null) { Debug.Log("UI Load 실패"); return null; }
        return MakePopupUI(obj);
    }

    public PopupUIBase MakePopupUI(GameObject prefab)
    {
        if (!popupUI.ContainsKey(prefab.name))
        {
            var obj = Object.Instantiate(prefab); //MonoBehaviour없더라도 생성가능하도록
            popupUI.Add(prefab.name, obj);
        }
        popupUI[prefab.name].SetActive(true);
        return GetComponentPopupUI(popupUI[prefab.name]);
    }

    public PopupUIBase GetComponentPopupUI(GameObject clone)
    {
        var script = clone.GetComponent<PopupUIBase>();
        return script;
    }
}
