using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _zSpeed = 5;
    private float _zDestroy = -17;

    [SerializeField] private float _xSpeed = 5;

    private float _rotSpeed = 2f;
    private float _maxRot = 30;

    private Transform _model;

    public Vector3 XDirection { get; set; }

    void Start()
    {
        var enemy = transform.GetComponentInChildren<Enemy>();
        _model = enemy.gameObject.transform;
        enemy.OnWallCollision += SwitchXDirection;
    }

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
        transform.Translate(XDirection * _xSpeed * Time.deltaTime);
        RotateOnMoving();
    }

    private void RotateOnMoving()
    {
        var targetAngle = -(_maxRot * XDirection.x);

        var euler = Quaternion.Euler(_model.eulerAngles.x, _model.eulerAngles.y, targetAngle);
        _model.rotation = Quaternion.Lerp(_model.rotation, euler, Time.deltaTime * _rotSpeed);
    }

    private void SwitchXDirection()
    {
        if (XDirection == Vector3.right)
            XDirection = Vector3.left;
        else
            XDirection = Vector3.right;
    }
}
