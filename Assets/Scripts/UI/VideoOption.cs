using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VideoOption : MonoBehaviour
{
    FullScreenMode screenMode;
    public Dropdown resolutionDropdown;
    public Toggle fullscreenBtn;
    List<Resolution> resolutions = new List<Resolution>();
    int resolutionNum;
    public Camera mainCamera;
    const string ResolutionKey = "SavedResolution";
    const string FullscreenKey = "SavedFullscreen";

    void Start()
    {
        InitUI();
    }

    void InitUI()
    {
        LoadSavedSettings();

        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 60)
                resolutions.Add(Screen.resolutions[i]);
        }
        resolutionDropdown.options.Clear();

        int optionNum = 0;
        foreach (Resolution item in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + "x" + item.height + "" + item.refreshRate + "hz";
            resolutionDropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
                resolutionDropdown.value = optionNum;
            optionNum++;
        }
        resolutionDropdown.RefreshShownValue();

        Debug.Log(PlayerPrefs.GetInt(FullscreenKey, 0));
        Debug.Log(fullscreenBtn);
        if (PlayerPrefs.GetInt(FullscreenKey, 0) != null)
        {
            //fullscreenBtn.isOn = PlayerPrefs.GetInt(FullscreenKey, 0) != 1 ?  true : false;
        }
    }

    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
    }

    public void FullScreenBtn(Toggle isFull)
    {
        screenMode = isFull.isOn ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void OkBtnClick()
    {
        SaveSettings(); 
        Screen.SetResolution(resolutions[resolutionNum].width,
            resolutions[resolutionNum].height,
            screenMode);

        UpdateCameraAspectRatio(resolutions[resolutionNum]);
    }

    void SaveSettings()
    {
        PlayerPrefs.SetInt(ResolutionKey, resolutionNum);
        PlayerPrefs.SetInt(FullscreenKey, screenMode == FullScreenMode.FullScreenWindow ? 1 : 0);
        PlayerPrefs.Save();
    }

    void LoadSavedSettings()
    {
        resolutionNum = PlayerPrefs.GetInt(ResolutionKey, 0); 
    }

    void UpdateCameraAspectRatio(Resolution resolution)
    {
        float targetAspect = (float)resolution.width / resolution.height;
        mainCamera.aspect = targetAspect;
    }

    public void OutBtnClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}