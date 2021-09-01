using UnityEngine;

public class InGameUI : MonoBehaviour
{
    private GameObject _pauseMenu;
    private bool _isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        _pauseMenu = transform.Find("PauseMenu").gameObject;
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
