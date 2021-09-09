using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    private const string ScoreFormat = "0000000000";
    private Text _scoreText;
    private int _score;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            _scoreText.text = _score.ToString(ScoreFormat);
        }
    }

    void Start()
    {
        _scoreText = GetComponentInChildren<Text>();
    }
}
