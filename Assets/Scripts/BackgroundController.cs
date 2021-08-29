using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField]private float _speed = 1;
    private Vector3 _startPos;
    private float _repeatLength;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _repeatLength = GetComponent<BoxCollider>().size.z * transform.localScale.z;
        var startOffset = Random.Range(0, _repeatLength);
        transform.Translate(Vector3.back * startOffset);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < _startPos.z - _repeatLength)
            transform.position = _startPos;
        transform.Translate(Vector3.back * Time.deltaTime * _speed);
    }
}
