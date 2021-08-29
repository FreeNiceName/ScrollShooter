using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (name.Contains("NewGame"))
            GetComponent<Button>().onClick.AddListener(NewGameOnClick);
        if(name.Contains("Settings"))
            GetComponent<Button>().onClick.AddListener(SettingsOnClick);
        if(name.Contains("Exit"))
            GetComponent<Button>().onClick.AddListener(ExitOnClick);


    }

    private void NewGameOnClick()
    {
        SceneManager.LoadScene("Game");
    }

    private void SettingsOnClick()
    {

    }

    private void ExitOnClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
