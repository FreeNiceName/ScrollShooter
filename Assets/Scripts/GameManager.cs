using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>, INotifyPropertyChanged
{
    [SerializeField] private int _initialScore = 0;
    [SerializeField] private int _initialLives = 5;
    [SerializeField] private int _initialSpecials = 5;
    [SerializeField] private int _initialHealth = 100;

    private int _score;
    private int _lives;
    private int _specials;
    private int _health;

    public readonly List<Binding> Bindings = new List<Binding>();

    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            NotifyPropertyChanged(nameof(Score));
        }
    }

    public int Lives
    {
        get => _lives;
        set
        {
            _lives = value;
            NotifyPropertyChanged(nameof(Lives));
        }
    }

    public int Specials
    {
        get => _specials;
        set
        {
            _specials = value;
            NotifyPropertyChanged(nameof(Specials));
        }
    }

    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            NotifyPropertyChanged(nameof(Health));
        }
    }

    private void Awake()
    {
        Reset();
    }

    public void Reset()
    {
        Score = _initialScore;
        Lives = _initialLives;
        Specials = _initialSpecials;
        Health = _initialHealth;
    }
}
