using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private Material _flashMaterial;

    private MeshRenderer _renderer;
    private Material _originalMaterial;
    private float _flashDuration = 0.1f;

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _originalMaterial = _renderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            if (other.gameObject.name.Contains("Player"))
            {
                Destroy(other.gameObject);
                _health--;
                StartCoroutine(Blink());

                if (_health <= 0)
                {
                    Destroy(transform.parent.gameObject);
                    Debug.Log("Enemy collided with player projectile");
                }
            }
        }
    }

    private IEnumerator Blink()
    {
        _renderer.material = _flashMaterial;
        yield return new WaitForSeconds(_flashDuration);
        _renderer.material = _originalMaterial;
    }
}
