using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _zSpeed = 5;
    private float _zDestroy = -17;

    [SerializeField] private float _xSpeed = 5;
    private float _xBound = 26;
    private bool _direction;

    private float _rotSpeed = 2f;
    private float _maxRot = 30;

    private Transform _model;

    // Start is called before the first frame update
    void Start()
    {
        _model = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(_zSpeed !=0)
            MoveDown();

        if(_xSpeed != 0)
            MoveSideToSide();
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.back * _zSpeed * Time.deltaTime);

        if (transform.position.z < _zDestroy)
            Destroy(gameObject);
    }

    private void MoveSideToSide()
    {
        transform.Translate(Vector3.right * (_direction ? 1 : -1) * _xSpeed * Time.deltaTime);
        if ((transform.position.x > _xBound) && _direction 
            || (transform.position.x < -_xBound) && !_direction)
            _direction = !_direction;
        RotateOnMoving();
    }

    private void RotateOnMoving()
    {
        var targetAngle = 0f;

        if (_direction)
            targetAngle = -_maxRot;
        else
            targetAngle = _maxRot;

        var euler = Quaternion.Euler(_model.eulerAngles.x, _model.eulerAngles.y, targetAngle);
        _model.rotation = Quaternion.Lerp(_model.rotation, euler, Time.deltaTime * _rotSpeed);
    }
}
