using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private SettingsMenu _settings;

    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        ButtonsInit();
        _settings = transform.parent.GetComponentInChildren<SettingsMenu>(true);
    }

    private void ButtonsInit()
    {
        var buttons = GetComponentsInChildren<Button>();

        for (var i = 0; i < buttons.Length; ++i)
        {
            var button = buttons[i];

            if (button.name.Contains("NewGame"))
                button.onClick.AddListener(NewGame);
            if (button.name.Contains("Settings"))
                button.onClick.AddListener(ToSettings);
            if (button.name.Contains("Exit"))
                button.onClick.AddListener(ExitGame);
        }
    }

    private void NewGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    private void ToSettings()
    {
        gameObject.SetActive(false);
        _settings.Enable(gameObject);
    }

    private void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
