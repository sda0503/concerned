using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Loadingbar
{
    public Image progress; //로딩바

    public TextMeshProUGUI text; //로딩 텍스트

    private float totalload = 4;

    public float percent=0f;
    public int count = 0;

    public float GetPercent()
    {
        return count / totalload;
    }

    public void UpdateUI(string Text)
    {
        progress.fillAmount = GetPercent();
        text.text = "";
        text.text = Text;
    }
}

public class GameTrigger : MonoBehaviour
{

    public Loadingbar Loadingbar;
    
    //1. 콜백으로 등록해서 처리하기
    void Start()
    {
        DataManager.Instance.init();
        DataManager.Instance.LoadingChange += LoadingUpdate;
        Loadingbar.progress.fillAmount = 0;
    }

    void LoadingUpdate()
    {
        Loadingbar.count++;
        //Loadingbar.UpdateUI();
    }
    
    
}
