using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
public class SettingsController : MonoBehaviour
{
    // Settings
    static int currentResolution = 0;
    static bool fullscreen = true;
    static float volume = 1.0f;
    // UI
    [SerializeField] TMP_Dropdown ResolutionDropdown;
    [SerializeField] Toggle FullscreenToggle;
    [SerializeField] Slider VolumeSlider;
    [SerializeField] TMP_Text VolumePercent;
    void Start()
    {
        ResolutionDropdown.value = currentResolution;
        FullscreenToggle.isOn = fullscreen;
        VolumeSlider.value = volume;
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
        VolumeSlider.onValueChanged.AddListener(delegate
        {
            SetSettings();
            ApplySettings();
        });
    }

    void SetSettings()
    {
        currentResolution = ResolutionDropdown.value;
        fullscreen = FullscreenToggle.isOn;
        volume = VolumeSlider.value;
        VolumePercent.text = ((int)(VolumeSlider.value * 100f)).ToString() + "%";
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
