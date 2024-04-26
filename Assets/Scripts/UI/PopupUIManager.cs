using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUIManager
{
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

    public T OpenPopupUI<T>(Transform t = null) where T : PopupUIBase
    {
        return OpenPopupUI(typeof(T).Name, t) as T; //script�̸� == resources�̸�
    }

    private PopupUIBase OpenPopupUI(string name, Transform t = null)
    {
        var obj = Resources.Load("Prefabs/PopupUI/" + name, typeof(GameObject)) as GameObject;
        if (obj == null) { Debug.Log("UI Load fail"); return null; }
        return MakePopupUI(obj, t);
    }

    private PopupUIBase MakePopupUI(GameObject prefab, Transform t = null)
    {
        if (!popupUI.ContainsKey(prefab.name))
        {
            var obj = Object.Instantiate(prefab, t); //MonoBehaviour������ ���������ϵ���
            popupUI.Add(prefab.name, obj);
        }
        popupUI[prefab.name].SetActive(true);
        return GetComponentPopupUI(popupUI[prefab.name]);
    }

    private PopupUIBase GetComponentPopupUI(GameObject clone)
    {
        var script = clone.GetComponent<PopupUIBase>();
        return script;
    }
}
