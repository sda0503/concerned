using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public GameObject soundOn;
    public GameObject soundOff;
    private bool muted = false;

    private static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Load();
        UpdateBtning();
        AudioListener.pause = muted;
        PlaySceneMusic(SceneManager.GetActiveScene().name);
    }

    public void SoundBtnClick()
    {
        muted = !muted;
        AudioListener.pause = muted;
        Save();
        UpdateBtning();
    }

    private void UpdateBtning()
    {
        soundOn.SetActive(!muted);
        soundOff.SetActive(muted);
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted", 0) == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }

    private void PlaySceneMusic(string sceneName)
    {
        GameObject sceneMusic = GameObject.Find(sceneName + "Music");
        if (sceneMusic != null)
        {
            AudioSource audioSource = sceneMusic.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}