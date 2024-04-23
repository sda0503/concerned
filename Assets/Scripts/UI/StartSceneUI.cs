using System.Collections;
using System.Collections.Generic;
using DataStorage;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour
{
    public Button startButton;
    public Button loadButton;
    public Button bookButton;
    public Button optionButton;
    public Button exitButton;

    private void Start()
    {
        startButton.onClick.AddListener(OnStartGame);
        bookButton.onClick.AddListener(OnOpenBook);
        loadButton.onClick.AddListener(OnLoadGame);
    }
    
    //TODO : Load했을 시 데이터 매니저 or 게임매니저에서 사용할 데이터 생성을 분리.

    private void OnStartGame()
    {
        SceneManager.LoadScene("LoadingScene");
        DataManager.Instance.GameState = Game_State.New;
    }

    private void OnLoadGame()
    {
        SceneManager.LoadScene("LoadingScene");
        DataManager.Instance.GameState = Game_State.Load;
    }

    private void OnOpenBook()
    {
        PopupUIManager.Instance.OpenPopupUI<DogamUI>();
    }
}
