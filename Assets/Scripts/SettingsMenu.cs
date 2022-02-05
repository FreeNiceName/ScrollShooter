using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    private static float _volume;
    private static int _qualityIndex;
    private static bool _isAutofire = true;

    private Toggle _autofireToggle;
    private Dropdown _qualityDropdown;
    private Slider _volumeSlider;

    private GameObject _prevMenu;

    public static bool IsAutofire
    {
        get => _isAutofire;
        private set
        { 
            _isAutofire = value;
            OnAutofireChanged?.Invoke(typeof(SettingsMenu), new PropertyChangedEventArgs("IsAutofire"));
        }
    }

    public static event PropertyChangedEventHandler OnAutofireChanged;

    void Start()
    {
        ButtonsInit();
        DropdownsInit();
        SlidersInit();

        _volume = AudioListener.volume;

        _autofireToggle = GetComponentInChildren<Toggle>();
        _autofireToggle.isOn = _isAutofire;
        //_fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        //_fullscreenToggle.isOn = _isFullscreen;
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
        _autofireToggle.isOn = _isAutofire;
        //SetFullscreen(_isFullscreen);
        //_fullscreenToggle.isOn = _isFullscreen;

        SetQuality(_qualityIndex);
        _qualityDropdown.value = _qualityIndex;

        SetVolume(_volume);
        _volumeSlider.value = _volume;

        Disable();
    }

    private void SaveOnClick()
    {
        IsAutofire = _autofireToggle.isOn;
        _qualityIndex = _qualityDropdown.value;
        _volume = _volumeSlider.value;

        PlayerPrefs.SetFloat("volume", _volume);

        Disable();
    }

    private void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    private void SetVolume(float value)
    {
        AudioListener.volume = value;
    }
}
