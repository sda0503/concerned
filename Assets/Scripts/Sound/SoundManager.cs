using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : SingletonBase<SoundManager>
{
    public GameObject soundOn;
    public GameObject soundOff;
    private bool muted = false;
    //TODO : public List<AudioSource> musicList = new List<AudioSource>(); //사용할 배경음악 파일 먹여서 필요에 따라 실행시키기.

    public override void init()
    {
        base.init();
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
        GameObject sceneMusic = GameObject.Find(sceneName + "Music"); //TODO : 수정사항. 현재 씬에 필요한 노래가 뭔지 판단하고 재생시키는 것까지.
        //AudioSource sceneMusic = musicList[SceneManager.GetActiveScene().buildIndex]; //TODO : 빌드 인덱스에 따라 노래 다르게 재생.
        if (sceneMusic != null)
        {
            AudioSource audioSource = sceneMusic.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlaySceneMusic(scene.name);
    }
}