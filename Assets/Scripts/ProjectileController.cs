using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    private float _zDestroy = 15;
    private Vector3 _direction;

    private void Start()
    {
        if (gameObject.name.Contains("Player"))
            _direction = Vector3.forward;
        else if (gameObject.name.Contains("Enemy"))
            _direction = Vector3.back;
    }

    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);

        if (transform.position.z < -_zDestroy || transform.position.z > _zDestroy)
            Destroy(gameObject);
    }
}
