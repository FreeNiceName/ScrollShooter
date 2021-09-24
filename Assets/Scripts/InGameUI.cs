using UnityEngine;

public class InGameUI : MonoBehaviour
{
    private GameObject _pauseMenu;
    private GameObject _gameOverWindow;
    private bool _isPaused = false;
    private bool _isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        _pauseMenu = transform.Find("PauseMenu").gameObject;
        _gameOverWindow = transform.Find("GameOverWindow").gameObject;

        var gameManager = FindObjectOfType<GameManager>();
        gameManager.GameOver += OnGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPaused && !_isGameOver)
                Pause();
            else if (_pauseMenu.activeSelf)
                Resume();
        }
    }

    public void OnGameOver()
    {
        _gameOverWindow.SetActive(true);
        Time.timeScale = 0f;
        _isGameOver = true;
    }

    public void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }
}
