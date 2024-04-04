using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class Loadingbar
{
    public Image progress; //로딩바

    public TextMeshProUGUI text; //로딩 텍스트

    private float totalload = 3;

    public float percent=0f;
    public int count = 0;

    public float GetPercent()
    {
        return count / totalload;
    }

    public void UpdateUI()
    {
        progress.fillAmount = GetPercent();
        text.text = "";
        
        switch (count)
        {
            case 0:
                text.text = "다이얼로그 세팅";       
                break;
            case 1:
                text.text ="장소 세팅";
                break;
            case 2:
                text.text = "아이템 세팅";
                break;
            case 3:
                text.text = "도감 세팅";
                break;
        }
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
        if (Loadingbar.count == 3)
        {
            SceneManager.LoadScene("StartScene");
        }
        Loadingbar.UpdateUI();
    }
    
    
}
