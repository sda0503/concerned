using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        bookButton.onClick.AddListener(OnOpenBook);
    }

    private void OnOpenBook()
    {
        PopupUIManager.Instance.OpenPopupUI<DogamUI>();
    }
}
