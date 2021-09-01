using System.Collections;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    private float _buffDuration = 5;
    public bool HasInvulerability { get; private set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            StartCoroutine(BuffCoroutine());
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Projectile"))
        {
            if (other.gameObject.name.Contains("Enemy"))
            {
                Destroy(other.gameObject);
                Debug.Log("Player collided with enemy projectile");
            }
        }

        if (other.CompareTag("Enemy"))
        {
            Destroy(other.transform.parent.gameObject);
            Debug.Log("Player collided with enemy");
        }
    }

    private IEnumerator BuffCoroutine()
    {
        HasInvulerability = true;
        yield return new WaitForSeconds(_buffDuration);
        HasInvulerability = false;
    }
}
