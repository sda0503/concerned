using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class VideoController : MonoBehaviour
{
    public RawImage videoScreen;
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        videoPlayer.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        videoScreen.texture = videoPlayer.targetTexture;
        DataManager.Instance.init();
        videoPlayer.loopPointReached += OnVideoEnd;
        StartCoroutine(PlayVideo());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && DataManager.Instance.canSkip)
        {
            SceneManager.LoadScene("StartScene"); //TODO : 공통된 LoadingScene 넘어가는걸로 바꾸기.
        }
    }

     IEnumerator PlayVideo()
    {
        videoPlayer.url = "https://todaykeywords.kr/game/video1.mp4";
        videoPlayer.Play();
        yield break;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("StartScene");
    }

    void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}