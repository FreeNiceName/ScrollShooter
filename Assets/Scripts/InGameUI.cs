using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InGameUI : MonoBehaviour
{
    private PlayerController _playerController;
    private GameObject _pauseMenu;
    private GameObject _gameOverWindow;
    private bool _isPaused = false;
    private bool _isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();

        _pauseMenu = transform.Find("PauseMenu").gameObject;
        _gameOverWindow = transform.Find("GameOverWindow").gameObject;

        var gameManager = FindObjectOfType<GameManager>();
        gameManager.GameOver += OnGameOver;

        ButtonsInit();
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

    private void ButtonsInit()
    {
        var buttons = GetComponentsInChildren<Button>();

        for (var i = 0; i < buttons.Length; ++i)
        {
            var button = buttons[i];

            if (button.name.Contains("Pause"))
                button.onClick.AddListener(Pause);
            if (button.name.Contains("Missile"))
                button.onClick.AddListener(_playerController.ShootMissile);
            if (button.name.Contains("Fire"))
            {
                var eventTrigger = button.GetComponent<EventTrigger>();
                var trigger = new EventTrigger.Entry();
                trigger.eventID = EventTriggerType.PointerDown;
                trigger.callback.AddListener((data) => _playerController.IsFiring = true);
                eventTrigger.triggers.Add(trigger);

                trigger = new EventTrigger.Entry();
                trigger.eventID = EventTriggerType.PointerUp;
                trigger.callback.AddListener((data) => _playerController.IsFiring = false);
                eventTrigger.triggers.Add(trigger);
            }
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
