using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private MultipleElementsLayout _specialsView;
    [SerializeField] private MultipleElementsLayout _livesView;
    [SerializeField] private Slider _healthView;

    private GameManager _gameManager;
    private GameObject _pauseMenu;
    private bool _isPaused = false;

    private List<Binding> _bindings = new List<Binding>();

    // Start is called before the first frame update
    void Start()
    {
        _pauseMenu = transform.Find("PauseMenu").gameObject;
        _gameManager = FindObjectOfType<GameManager>();

        InitHudBindings();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPaused)
                Pause();
            else if (_pauseMenu.activeSelf)
                Resume();
        }
    }

    private void InitHudBindings()
    {
        _bindings.Add(new Binding(_gameManager, _scoreView, "Score", "Score"));
        _bindings.Add(new Binding(_gameManager, _specialsView, "Specials", "Value"));
        _bindings.Add(new Binding(_gameManager, _livesView, "Lives", "Value"));
        _bindings.Add(new Binding(_gameManager, _healthView, "Health", "value"));
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
