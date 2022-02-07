using System.Collections;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private float _shieldDuration = 5;

    private GameManager _gameManager;
    private GameObject _shield;
    private bool _hasShield = false;

    private void Start()
    {
        _shield = transform.Find("Shield").gameObject;
        _shield?.SetActive(false);
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.Death += ActivateShield;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            if (other.gameObject.name.Contains("Shield"))
                ActivateShield();
            else if (other.gameObject.name.Contains("Missile"))
                _gameManager.Missiles++;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Projectile"))
        {
            if (other.gameObject.name.Contains("Enemy"))
            {
                TakeDamage(other.GetComponent<ProjectileController>().Damage);
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            TakeDamage(enemy.CollisionDamage);
            Destroy(other.gameObject);
        }
    }

    private void ActivateShield()
    {
        StartCoroutine(ShieldCoroutine());
    }

    private IEnumerator ShieldCoroutine()
    {
        _hasShield = true;
        _shield?.SetActive(true);
        yield return new WaitForSeconds(_shieldDuration);
        _hasShield = false;
        _shield?.SetActive(false);
    }

    private void TakeDamage(int damage)
    {
        if(!_hasShield)
            _gameManager.Health -= damage;
    }
}
