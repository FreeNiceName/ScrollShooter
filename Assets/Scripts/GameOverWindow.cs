using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverWindow : MonoBehaviour
{
    private void Start()
    {
        ButtonsInit();
        var score = transform.Find("Score");
        var scoreText = score.GetComponent<Text>();

        var gameManager = FindObjectOfType<GameManager>();
        scoreText.text = gameManager.Score.ToString();
    }

    private void ButtonsInit()
    {
        var buttons = GetComponentsInChildren<Button>();

        for (var i = 0; i < buttons.Length; ++i)
        {
            var button = buttons[i];
            
            if (button.name.Contains("Restart"))
                button.onClick.AddListener(Restart);
            if (button.name.Contains("Menu"))
                button.onClick.AddListener(ToMenu);
        }
    }

    private void Restart()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
        Time.timeScale = 1f;
    }

    private void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
