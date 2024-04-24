using System;
using DataStorage;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class Loadingbar
{
    public Image progress; //로딩바

    public TextMeshProUGUI text; //로딩 텍스트

    private float totalload = 1;

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
                text.text = "플레이어 정보 세팅";       
                break;
            case 1:
                text.text ="완료";
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
        if(DataManager.Instance.GameState == Game_State.New)
            StartCoroutine(DataManager.Instance.SetPlayerData());
        else DataManager.Instance.LoadPlayerData();
        DataManager.Instance.LoadingChange += LoadingUpdate;
        Loadingbar.progress.fillAmount = 0;
    }

    void LoadingUpdate()
    {
        Loadingbar.count++;
        if (Loadingbar.count == 1)
        {
            SceneManager.LoadScene("TutoScene");
        }
        Loadingbar.UpdateUI();
        
    }
}
