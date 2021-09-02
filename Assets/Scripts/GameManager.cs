using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private MultipleElementsLayout _livesLayout;
    [SerializeField] private MultipleElementsLayout _specialsLayout;

    [SerializeField] private int _initialLives;
    [SerializeField] private int _initialSpecials;

    private string _scoreFormat = "0000000000";
    private int _score;
    private int _lives;
    private int _specials;

    void Start()
    {
        AddLives(_initialLives);
        AddSpecials(_initialSpecials);
    }

    public void AddScore(int value)
    {
        _score += value;
        _scoreText.text = _score.ToString(_scoreFormat);
    }

    public void UpdateHealth(int value)
    {
        _healthSlider.value = value;
    }

    public void AddLives(int value)
    {
        _lives += value;
        _livesLayout.UpdateValue(_lives);
    }

    public void AddSpecials(int value)
    {
        _specials += value;
        _specialsLayout.UpdateValue(_specials);
    }
}
