using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void Option()
    {
        SceneManager.LoadScene("Option");
    }

    public void STARTScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    // public void StartGame()
    // {
    //     SceneManager.LoadScene("TestScene");
    // }
}
