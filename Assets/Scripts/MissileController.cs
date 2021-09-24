using UnityEngine;

public class MissileController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _maxDistance = 20f;

    private GameObject _model;
    private GameObject _blastWave;
    private float _startPosZ;
    private bool _isExploding;

    void Start()
    {
        _model = transform.Find("Model").gameObject;
        _blastWave = transform.Find("BlastWave").gameObject;

        _startPosZ = transform.position.z;
    }

    void Update()
    {
        if (!_isExploding)
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);

            if (transform.position.z - _startPosZ > _maxDistance)
                Explode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            Explode();
    }

    private void Explode()
    {
        _model.SetActive(false);
        _blastWave.SetActive(true);
        _isExploding = true;
    }
}
