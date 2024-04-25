using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : SingletonBase<SoundManager>
{
    public GameObject soundOn;
    public GameObject soundOff;
    private bool muted = false;

    // 음악 파일 리스트
    public List<AudioClip> musicList = new List<AudioClip>();

    // 현재 재생 중인 노래
    private AudioSource currentMusic;

    // JSON 데이터를 담을 구조체
    [System.Serializable]
    public struct PlaceData
    {
        public string Place_ID;
        public string Music_Name;
    }

    // JSON 데이터를 파싱하여 저장할 리스트
    public List<PlaceData> placeDataList = new List<PlaceData>();

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
        // sceneName에 해당하는 Place_ID를 찾아서 해당하는 Music_Name을 가져옴
        string musicName = "";
        foreach (var placeData in placeDataList)
        {
            if (placeData.Place_ID == sceneName)
            {
                musicName = placeData.Music_Name;
                break;
            }
        }

        if (musicName != "")
        {
            // Music_Name에 해당하는 AudioClip을 찾아서 재생
            AudioClip clipToPlay = musicList.Find(x => x.name == musicName);
            if (clipToPlay != null)
            {
                if (currentMusic != null && currentMusic.clip.name == clipToPlay.name)
                {
                    // 현재 재생 중인 노래와 같은 노래라면 다시 재생하지 않음
                    return;
                }

                // 이전에 재생 중이던 노래를 정지하고 새로운 노래를 재생
                if (currentMusic != null)
                {
                    currentMusic.Stop();
                }

                currentMusic = gameObject.AddComponent<AudioSource>();
                currentMusic.clip = clipToPlay;
                currentMusic.loop = true;
                currentMusic.Play();
            }
            else
            {
                Debug.LogWarning("Music clip not found: " + musicName);
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
