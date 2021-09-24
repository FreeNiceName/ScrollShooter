using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private Material _flashMaterial;
    [SerializeField] private int _collisionDamage;
    [SerializeField] private uint _scoreValue;

    private GameManager _gameManager;
    private MeshRenderer _renderer;
    private Material _originalMaterial;
    private float _flashDuration = 0.1f;

    public event Action OnWallCollision;
    public int CollisionDamage { get => _collisionDamage; }

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _originalMaterial = _renderer.material;
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            OnWallCollision?.Invoke();
        }
        else if (other.CompareTag("Projectile"))
        {
            if (other.gameObject.name.Contains("Player"))
            {
                Destroy(other.gameObject);

                var damage = other.GetComponent<ProjectileController>().Damage;
                TakeDamage(damage);
            }
        }
        else if(other.CompareTag("BlastWave"))
        {
            var damage = other.GetComponent<BlastWaveController>().Damage;
            TakeDamage(damage);
        }
    }

    private void TakeDamage(int damage)
    {
        StartCoroutine(Flash());
        _health -= damage;
        if (_health <= 0)
        {
            _gameManager.Score += _scoreValue;
            Destroy(this);
        }
    }

    private IEnumerator Flash()
    {
        _renderer.material = _flashMaterial;
        yield return new WaitForSeconds(_flashDuration);
        _renderer.material = _originalMaterial;
    }

    private void OnDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
