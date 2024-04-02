using UnityEngine;

public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour //제약조건.
{
    //게임 트리거를 이용해 관리.
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                string objname = typeof(T).FullName;
                GameObject go = new GameObject(objname);
                _instance = go.AddComponent<T>();
                
                DontDestroyOnLoad(go);
            }
            
            return _instance;
        }
    }

    public void init()
    {
        Debug.Log(transform.name + "is Activate.");
    }
}
