using UnityEngine;

public class BlastWaveController : MonoBehaviour
{
    [SerializeField] private float _maxScale = 10f;
    [SerializeField] private float _blastWaveSpeed = 10f;
    [SerializeField] private int _damage = 10;

    public int Damage { get => _damage; }

    void Update()
    {
        transform.localScale += Vector3.one * Time.deltaTime * _blastWaveSpeed;
        if (transform.localScale.x > _maxScale)
            Destroy(this);
    }

    private void OnDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
