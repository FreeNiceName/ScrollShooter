using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private SettingsMenu _settings;
    private InGameUI _inGameUI;

    void Start()
    {
        ButtonsInit();
        _settings = transform.parent.GetComponentInChildren<SettingsMenu>(true);
        _inGameUI = GetComponentInParent<InGameUI>();
    }

    private void ButtonsInit()
    {
        var buttons = GetComponentsInChildren<Button>();

        for (var i = 0; i < buttons.Length; ++i)
        {
            var button = buttons[i];

            if (button.name.Contains("Resume"))
                button.onClick.AddListener(Resume);
            if (button.name.Contains("Settings"))
                button.onClick.AddListener(ToSettings);
            if (button.name.Contains("Menu"))
                button.onClick.AddListener(ToMenu);
        }
    }

    private void Resume()
    {
        _inGameUI.Resume();
    }

    private void ToSettings()
    {
        gameObject.SetActive(false);
        _settings.Enable(gameObject);
    }

    private void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
