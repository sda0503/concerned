using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUIManager
{
    //���׸����� �޾Ƽ� popupUI ������ UI ��ư ������ UI���� script�־��ֱ�

    //UI �������� �ε� -> ���׸����� �ٲ㼭 �������� ����� �� �ֵ��� ����
    //��ųʸ� key �� �̿��ؼ� ������ �����ϰ� ������ ���� ���� ��ųʸ��� ����
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
        return OpenPopupUI(typeof(T).Name) as T; //script�̸� == resources�̸�
    }

    public PopupUIBase OpenPopupUI(string name)
    {
        var obj = Resources.Load("PopupUI/" + name, typeof(GameObject)) as GameObject;
        if (obj == null) { Debug.Log("UI Load fail"); return null; }
        return MakePopupUI(obj);
    }

    public PopupUIBase MakePopupUI(GameObject prefab)
    {
        if (!popupUI.ContainsKey(prefab.name))
        {
            var obj = Object.Instantiate(prefab); //MonoBehaviour������ ���������ϵ���
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
