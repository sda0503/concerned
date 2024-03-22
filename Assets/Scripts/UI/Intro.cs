using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ImageController : MonoBehaviour
{
    public RawImage videoScreen;
    public VideoClip videoClip;
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        videoPlayer.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        videoScreen.texture = videoPlayer.targetTexture;
        PlayVideo();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("StartScene");
        }
    }

    void PlayVideo()
    {
        videoPlayer.clip = videoClip;
        videoPlayer.Play();
    }
}