using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private MultipleElementsLayout _specialsView;
    [SerializeField] private MultipleElementsLayout _livesView;
    [SerializeField] private Slider _healthView;

    private GameObject _pauseMenu;
    private bool _isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        _pauseMenu = transform.Find("PauseMenu").gameObject;
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
        var bindings = new List<Binding>();
        bindings.Add(new Binding(GameManager.Instance, _scoreView, "Score", "Score"));
        bindings.Add(new Binding(GameManager.Instance, _specialsView, "Specials", "Value"));
        bindings.Add(new Binding(GameManager.Instance, _livesView, "Lives", "Value"));
        bindings.Add(new Binding(GameManager.Instance, _healthView, "Health", "value"));
        GameManager.Instance.Bindings.AddRange(bindings);
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
