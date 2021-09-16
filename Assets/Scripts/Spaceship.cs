using System.Collections;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private float _shieldDuration = 5;

    private GameObject _shield;
    private bool _hasShield = false;

    private void Start()
    {
        _shield = transform.Find("Shield").gameObject;
        _shield?.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            StartCoroutine(ShieldCoroutine());
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
            TakeDamage(other.GetComponent<Enemy>().CollisionDamage);
            Destroy(other.transform.parent.gameObject);
        }
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
            GameManager.Instance.Health -= damage;
    }
}
