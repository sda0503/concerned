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
        // JSON 파일 읽기 (예: Resources 폴더에 위치한 Place_DB.json 파일)
        string jsonText = Resources.Load<TextAsset>("Place_DB").text;
        LocationData locationData = JsonUtility.FromJson<LocationData>(jsonText);

        // Scene 이름에 해당하는 장소 찾기
        LocationInfo location = locationData.locations.Find(x => x.Place_Name == sceneName);
        if (location != null)
        {
            // 장소에 맞는 노래 재생
            AudioClip song = Resources.Load<AudioClip>(location.Music_Name);
            if (song != null)
            {
                AudioSource.PlayClipAtPoint(song, Vector3.zero); // 임시로 재생 (위치 중요하지 않음)
            }
            else
            {
                Debug.LogWarning("Song not found for location: " + sceneName);
            }
        }
        else
        {
            Debug.LogWarning("Location not found: " + sceneName);
        }
    }

    [System.Serializable]
    public class LocationInfo
    {
        public string Place_ID;
        public string Place_Name;
        public string Place_BG_Path;
        public string Place_OBJ_Path;
        public string Map_Type;
        public string Music_Name;
    }
    [System.Serializable]
    public class LocationData
    {
        public List<LocationInfo> locations;
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