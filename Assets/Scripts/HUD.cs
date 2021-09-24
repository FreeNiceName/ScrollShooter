using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private MultipleElementsLayout _missilesView;
    [SerializeField] private MultipleElementsLayout _livesView;
    [SerializeField] private Slider _healthView;

    private List<Binding> _bindings = new List<Binding>();

    void Start()
    {
        InitBindings();
    }

    private void InitBindings()
    {
        var gameManager = FindObjectOfType<GameManager>();
        _bindings.Add(new Binding(gameManager, _scoreView, nameof(gameManager.Score), nameof(_scoreView.Score)));
        _bindings.Add(new Binding(gameManager, _missilesView, nameof(gameManager.Missiles), nameof(_missilesView.Value)));
        _bindings.Add(new Binding(gameManager, _livesView, nameof(gameManager.Lives), nameof(_livesView.Value)));
        _bindings.Add(new Binding(gameManager, _healthView, nameof(gameManager.Health), nameof(_healthView.value)));
    }
}
