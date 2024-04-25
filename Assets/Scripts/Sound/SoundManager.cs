using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public GameObject soundOn;
    public GameObject soundOff;
    private bool muted = false;

    // 음악 파일 리스트
    public List<AudioClip> musicList = new List<AudioClip>();

    // 현재 재생 중인 노래
    [SerializeField] private AudioSource currentMusic;

    // JSON 데이터를 담을 구조체
    [System.Serializable]
    public struct PlaceData
    {
        public string Place_ID;
        public string Music_Name;
    }

    // JSON 데이터를 파싱하여 저장할 리스트
    public List<PlaceData> placeDataList = new List<PlaceData>();

    public void Start()
    {
        Load();
        UpdateBtning();
        AudioListener.pause = muted;
        GameManager.Instance.OnPositionChange += PlaySceneMusic;
        PlaySceneMusic();
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

    private void PlaySceneMusic()
    {
        
        currentMusic.Stop();
        currentMusic.clip = musicList[int.Parse(GameManager.Instance.Playerinformation.position.ToString()[0].ToString())-1];
        currentMusic.Play();
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
        PlaySceneMusic();
    }
}
