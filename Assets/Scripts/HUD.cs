using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private MultipleElementsLayout _specialsView;
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
        _bindings.Add(new Binding(gameManager, _scoreView, "Score", "Score"));
        _bindings.Add(new Binding(gameManager, _specialsView, "Specials", "Value"));
        _bindings.Add(new Binding(gameManager, _livesView, "Lives", "Value"));
        _bindings.Add(new Binding(gameManager, _healthView, "Health", "value"));
    }
}
