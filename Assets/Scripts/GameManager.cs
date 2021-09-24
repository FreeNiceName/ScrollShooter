using System;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour, INotifyPropertyChanged
{
    [SerializeField] private uint _initialScore = 0;
    [SerializeField] private int _initialLives = 5;
    [SerializeField] private int _initialMissiles = 5;
    [SerializeField] private int _initialHealth = 100;
    [SerializeField] private int _maxHealth = 100;

    private uint _score;
    private int _lives;
    private int _missiles;
    private int _health;

    public event PropertyChangedEventHandler PropertyChanged;
    public event Action Death;
    public event Action GameOver;

    private void NotifyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public uint Score
    {
        get => _score;
        set
        {
            if (_score == value)
                return;

            _score = value;
            NotifyPropertyChanged(nameof(Score));
        }
    }

    public int Lives
    {
        get => _lives;
        set
        {
            if (_lives == value)
                return;

            _lives = value;
            NotifyPropertyChanged(nameof(Lives));

            if (_lives < 0)
                GameOver?.Invoke();
        }
    }

    public int Missiles
    {
        get => _missiles;
        set
        {
            if (_missiles == value)
                return;

            if (value < 0)
                value = 0;

            _missiles = value;
            NotifyPropertyChanged(nameof(Missiles));
        }
    }

    public int Health
    {
        get => _health;
        set
        {
            if (_health == value)
                return;

            if (value > _maxHealth)
                value = _maxHealth;
            else if(value < 0)
                value = 0;

            _health = value;
            NotifyPropertyChanged(nameof(Health));

            if (_health == 0)
                Death?.Invoke();
        }
    }

    private void Start()
    {
        InitValues();
        Death += OnDeath;
    }

    private void InitValues()
    {
        Score = _initialScore;
        Lives = _initialLives;
        Missiles = _initialMissiles;
        Health = _initialHealth;
    }

    private void OnDeath()
    {
        Lives--;
        Health = _maxHealth;
    }
}
