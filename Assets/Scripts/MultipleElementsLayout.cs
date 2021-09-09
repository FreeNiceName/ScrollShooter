using UnityEngine;
using UnityEngine.UI;

public class MultipleElementsLayout : MonoBehaviour
{
    [SerializeField] private GameObject _layoutElementPrefab;
    [SerializeField] private GameObject _multiplierPrefab;
    [SerializeField] private int _initialValue;

    private int _maxElementsWithoutMultiplier; //maximum number of displayed elements without multiplier
    private GameObject[] _elements;
    private GameObject _multiplier;
    private Text _multiplierText;
    private bool _isReversed;
    private int _value;

    public int Value
    {
        get => _value;
        set
        {
            _value = value;
            UpdateView();
        }
    }

    void Start()
    {
        _isReversed = GetComponent<HorizontalLayoutGroup>().reverseArrangement;

        _multiplier = Instantiate(_multiplierPrefab);
        _multiplier.transform.SetParent(transform, false);
        _multiplierText = _multiplier.GetComponent<Text>();

        _maxElementsWithoutMultiplier = CalculateMaxElements();
        _elements = new GameObject[_maxElementsWithoutMultiplier];

        for(var i = 0; i < _maxElementsWithoutMultiplier; ++i)
        {
            var element = Instantiate(_layoutElementPrefab);
            element.transform.SetParent(transform, false);
            _elements[i] = element;
        }

        CreateEmptySpace().transform.SetParent(transform, false);

        Bind();
    }

    //GameObject with this script must have same name as game manager property to bind with
    private void Bind()
    {
        var binding = new Binding(GameManager.Instance, this, gameObject.name, nameof(Value));
        GameManager.Instance.Bindings.Add(binding);
    }

    private int CalculateMaxElements()
    {
        var width = GetComponent<RectTransform>().rect.width;
        var layout = GetComponent<HorizontalLayoutGroup>();
        var layoutElement = _layoutElementPrefab.GetComponent<LayoutElement>();
        var freeWidth = width - layout.padding.left - layout.padding.right;
        var elementWidth = layoutElement.minWidth + layout.spacing;
        return (int)(freeWidth / elementWidth);
    }

    private GameObject CreateEmptySpace()
    {
        var emptySpace = new GameObject("EmptySpace", typeof(RectTransform));
        emptySpace.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
        var layoutElement = emptySpace.AddComponent<LayoutElement>();
        layoutElement.preferredWidth = GetComponent<RectTransform>().rect.width;
        layoutElement.layoutPriority++;
        return emptySpace;
    }

    private void UpdateView()
    {
        if (_value > _maxElementsWithoutMultiplier)
        {
            _elements[0].SetActive(true);
            for (var i = 1; i < _maxElementsWithoutMultiplier; ++i)
                _elements[i].SetActive(false);

            _multiplier.SetActive(true);
            if (_isReversed)
                _multiplierText.text = $"x {_value}";
            else
                _multiplierText.text = $"{_value} x";
        }
        else
        {
            _multiplier.SetActive(false);
            for (var i = 0; i < _maxElementsWithoutMultiplier; ++i)
            {
                if (i < _value)
                    _elements[i].SetActive(true);
                else
                    _elements[i].SetActive(false);
            }
        }
    }
}
