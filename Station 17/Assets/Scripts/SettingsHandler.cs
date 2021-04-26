/************************************
 * Author: Emmett Hale
 * 
 * Purpose: Handler script for settings
 * menu
 ************************************/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class SettingsHandler : MonoBehaviour
{
    public AudioMixer mixer;

    public static SettingsHandler instance;

    public LookScript playerLook;

    public Resolution[] resolutions;

    public TMP_Dropdown resolutionDropdown;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.Log("Two settings handlers!");
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;

            resolutions = Screen.resolutions.Where(resolution => resolution.refreshRate == 60).ToArray();

            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();

            int current = 0;

            //Get all resolutions for the available display
            for(int i = 0; i < resolutions.Length; i++)
            {
                options.Add(resolutions[i].width + " x " + resolutions[i].height);

                if(resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    current = i;
                }
            }

            resolutionDropdown.ClearOptions();
            //Adds options
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = current;
            resolutionDropdown.RefreshShownValue();
        }

        Application.targetFrameRate = 60;

        DontDestroyOnLoad(gameObject);
        //Load Settings
    }

    public void SetVolume(float value)
    {
        mixer.SetFloat("volume", value);
    }

    public void SetSensitivity(float value)
    {
        playerLook.mouse_sensitivity = value;
    }

    public void SetRenderDistance(float value)
    {
        playerLook.SetRenderDistance(value);
    }

    public float GetVolume()
    {
        float temp;

        mixer.GetFloat("volume", out temp);

        return  temp;
    }

    public float GetSensitivity()
    {
        return playerLook.mouse_sensitivity;
    }

    public float GetRenderDistance()
    {
        return playerLook.gameObject.GetComponent<Camera>().farClipPlane;
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetWindowedMode(int index)
    {
        switch (index)
        { 
            case 1:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;
            case 3:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }
}
