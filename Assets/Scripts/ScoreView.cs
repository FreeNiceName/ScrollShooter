using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    private const string ScoreFormat = "0000000000";
    private Text _scoreText;
    private uint _score;

    public uint Score
    {
        get => _score;
        set
        {
            if (_score == value)
                return;

            _score = value;
            _scoreText.text = _score.ToString(ScoreFormat);
        }
    }

    void Start()
    {
        _scoreText = GetComponentInChildren<Text>();
    }
}
