using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SettingsController : MonoBehaviour
{
    // Settings
    static int currentResolution = 0;
    static bool fullscreen = true;
    // UI
    [SerializeField] TMP_Dropdown ResolutionDropdown;
    [SerializeField] Toggle FullscreenToggle;
    void Start()
    {
        ResolutionDropdown.value = currentResolution;
        FullscreenToggle.isOn = fullscreen;
        ResolutionDropdown.onValueChanged.AddListener(delegate
        {
            SetSettings();
            ApplySettings();
        });
        FullscreenToggle.onValueChanged.AddListener(delegate
        {
            SetSettings();
            ApplySettings();
        });
    }

    void SetSettings()
    {
        currentResolution = ResolutionDropdown.value;
        fullscreen = FullscreenToggle.isOn;
    }
    void ApplySettings()
    {
        switch (ResolutionDropdown.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, fullscreen);
                break;
            case 1:
                Screen.SetResolution(1440, 900, fullscreen);
                break;
            case 2:
                Screen.SetResolution(1366, 768, fullscreen);
                break;
            case 3:
                Screen.SetResolution(1280, 720, fullscreen);
                break;
            case 4:
                Screen.SetResolution(640, 480, fullscreen);
                break;
        }
    }
}
