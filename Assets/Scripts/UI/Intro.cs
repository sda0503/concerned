using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageController : MonoBehaviour
{
    public Image image1;
    public Image[] images;
    private int currentIndex = 0;

    void Start()
    {
        DisableAllImages();
        image1.enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            image1.enabled = false;
            images[currentIndex].enabled = false;
            currentIndex++;

            if (currentIndex >= images.Length)
            {
                LoadNextScene();
                return;
            }
            images[currentIndex].enabled = true;
        }
    }

    void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex <= SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene("StartScene");
        }
    }

    void DisableAllImages()
    {
        foreach (Image image in images)
        {
            image.enabled = false;
        }
    }
}
