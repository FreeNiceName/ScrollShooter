using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _cooldown;
    [SerializeField] private int _damage;
    [SerializeField] private AudioClip _shootSound;

    private float _fireTimeout;
    private AudioSource _weaponAudio;

    void Start()
    {
        _weaponAudio = GetComponent<AudioSource>();
        //InvokeRepeating("Shoot", 0, _cooldown);
    }

    private void Update()
    {
        _fireTimeout -= Time.deltaTime;
    }

    public void Shoot()
    {
        if (_fireTimeout <= 0)
        {
            var projectile = Instantiate(_projectilePrefab, transform.position, _projectilePrefab.transform.rotation);
            projectile.GetComponent<ProjectileController>().Damage = _damage;
            _fireTimeout = _cooldown;
        }
        //_weaponAudio.PlayOneShot(_shootSound);
    }
}
