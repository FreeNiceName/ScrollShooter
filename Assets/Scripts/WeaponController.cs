using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _cooldown;
    [SerializeField] private int _damage;
    [SerializeField] private AudioClip _shootSound;

    private AudioSource _weaponAudio;

    void Start()
    {
        _weaponAudio = GetComponent<AudioSource>();
        InvokeRepeating("Shoot", 0, _cooldown);
    }

    private void Shoot()
    {
        var projectile = Instantiate(_projectilePrefab, transform.position, _projectilePrefab.transform.rotation);
        projectile.GetComponent<ProjectileController>().Damage = _damage;
        //_weaponAudio.PlayOneShot(_shootSound);
    }
}
