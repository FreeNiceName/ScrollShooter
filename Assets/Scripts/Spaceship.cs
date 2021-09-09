using System.Collections;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    private float _buffDuration = 5;
    private bool _hasInvulerability = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            StartCoroutine(BuffCoroutine());
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Projectile"))
        {
            if (other.gameObject.name.Contains("Enemy"))
            {
                GameManager.Instance.Health -= other.GetComponent<ProjectileController>().Damage;
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            GameManager.Instance.Health -= other.GetComponent<Enemy>().CollisionDamage;
            Destroy(other.transform.parent.gameObject);
        }
    }

    private IEnumerator BuffCoroutine()
    {
        _hasInvulerability = true;
        yield return new WaitForSeconds(_buffDuration);
        _hasInvulerability = false;
    }
}
