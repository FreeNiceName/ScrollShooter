using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    private float _zDestroy = 15;
    private sbyte _direction;

    private void Start()
    {
        if (gameObject.name.Contains("Player"))
            _direction = 1;
        else if (gameObject.name.Contains("Enemy"))
            _direction = -1;

    }

    void Update()
    {
        transform.Translate(Vector3.forward * _speed * _direction * Time.deltaTime);

        if (transform.position.z < -_zDestroy || transform.position.z > _zDestroy)
            Destroy(gameObject);
    }
}
