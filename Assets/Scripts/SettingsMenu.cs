using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    private static int _resolutionIndex;
    private static float _volume;
    private static int _qualityIndex;
    private static bool _isFullscreen;

    private Dropdown _resolutionDropdown;
    private Toggle _fullscreenToggle;
    private Dropdown _qualityDropdown;
    private Slider _volumeSlider;

    private GameObject _prevMenu;
    private static Resolution[] _resolutions;

    void Start()
    {
        ButtonsInit();
        DropdownsInit();
        SlidersInit();

        _volume = AudioListener.volume;
        _isFullscreen = Screen.fullScreen;

        _fullscreenToggle = GetComponentInChildren<Toggle>();
        _fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        _fullscreenToggle.isOn = _isFullscreen;
    }

    public void Enable(GameObject prevMenu)
    {
        _prevMenu = prevMenu;
        gameObject.SetActive(true);
    }

    private void Disable()
    {
        _prevMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ButtonsInit()
    {
        var buttons = GetComponentsInChildren<Button>();

        for (var i = 0; i < buttons.Length; ++i)
        {
            var button = buttons[i];

            if (button.name.Contains("Back"))
                button.onClick.AddListener(BackOnClick);
            if(button.name.Contains("Save"))
                button.onClick.AddListener(SaveOnClick);
        }
    }

    private void DropdownsInit()
    {
        var dropdowns = GetComponentsInChildren<Dropdown>();
        for (var i = 0; i < dropdowns.Length; ++i)
        {
            var dropdown = dropdowns[i];
            if (dropdown.name.Contains("Resolution"))
                ResolutionDropdownInit(dropdown);
            if (dropdown.name.Contains("Quality"))
                QualityDropdownInit(dropdown);
        }
    }

    private void SlidersInit()
    {
        var sliders = GetComponentsInChildren<Slider>();
        for(var i = 0; i < sliders.Length; ++i)
        {
            var slider = sliders[i];
            if (slider.name.Contains("Volume"))
            {
                slider.value = AudioListener.volume;
                slider.onValueChanged.AddListener(SetVolume);
                _volumeSlider = slider;
            }
        }
    }

    private void ResolutionDropdownInit(Dropdown dropdown)
    {
        _resolutionDropdown = dropdown;

        dropdown.onValueChanged.AddListener(SetResolution);

        dropdown.ClearOptions();

        _resolutions = Screen.resolutions;
        var options = new List<string>();

        for (var i = 0; i < _resolutions.Length; ++i)
        {
            string option = _resolutions[i].ToString();
            options.Add(option);

            if (_resolutions[i].width == Screen.width
                && _resolutions[i].height == Screen.height)
                _resolutionIndex = i;
        }

        dropdown.AddOptions(options);
        dropdown.value = _resolutionIndex;
        dropdown.RefreshShownValue();
    }

    private void QualityDropdownInit(Dropdown dropdown)
    {
        _qualityDropdown = dropdown;

        dropdown.onValueChanged.AddListener(SetQuality);

        dropdown.ClearOptions();

        var qualities = QualitySettings.names;
        dropdown.AddOptions(new List<string>(qualities));

        _qualityIndex = QualitySettings.GetQualityLevel();
        dropdown.value = _qualityIndex;
        dropdown.RefreshShownValue();
    }

    private void BackOnClick()
    {
        SetResolution(_resolutionIndex);
        _resolutionDropdown.value = _resolutionIndex;

        SetFullscreen(_isFullscreen);
        _fullscreenToggle.isOn = _isFullscreen;

        SetQuality(_qualityIndex);
        _qualityDropdown.value = _qualityIndex;

        SetVolume(_volume);
        _volumeSlider.value = _volume;

        Disable();
    }

    private void SaveOnClick()
    {
        _resolutionIndex = _resolutionDropdown.value;
        _isFullscreen = _fullscreenToggle.isOn;
        _qualityIndex = _qualityDropdown.value;
        _volume = _volumeSlider.value;

        PlayerPrefs.SetFloat("volume", _volume);

        Disable();
    }

    private void SetResolution(int resolutionIndex)
    {
        var resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    private void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    private void SetVolume(float value)
    {
        AudioListener.volume = value;
    }
}
