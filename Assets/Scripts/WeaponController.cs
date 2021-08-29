using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _cooldown;
    [SerializeField] private AudioClip _shootSound;

    private AudioSource _weaponAudio;

    // Start is called before the first frame update
    void Start()
    {
        _weaponAudio = GetComponent<AudioSource>();
        InvokeRepeating("Shoot", 0, _cooldown);
    }

    private void Shoot()
    {
        Instantiate(_projectilePrefab, transform.position, _projectilePrefab.transform.rotation);
        //_weaponAudio.PlayOneShot(_shootSound);
    }
}
