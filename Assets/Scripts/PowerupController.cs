using UnityEngine;

public class PowerupController : MonoBehaviour
{
    [SerializeField] private float _zSpeed = 5;
    private float _zDestroy = -17;

    [SerializeField] private float _xSpeed = 5;
    private float _xBound = 26;
    private bool _direction;

    private Transform _circle1;
    private Transform _circle2;

    [SerializeField]private float _circleRotSpeed = 45f;

    private Vector3 _circle1Vector;
    private Vector3 _circle2Vector;

    void Start()
    {
        _circle1 = transform.GetChild(0);
        _circle2 = transform.GetChild(1);
        _circle1Vector = NextVector3();
        _circle2Vector = NextVector3();
    }

    void Update()
    {
        MoveDown();
        MoveSideToSide();
        RotateCircles();
    }

    private void RotateCircles()
    {
        _circle1.Rotate(_circle1Vector, Time.deltaTime * _circleRotSpeed);
        _circle2.Rotate(_circle2Vector, Time.deltaTime * _circleRotSpeed);
    }

    private Vector3 NextVector3()
    {
        var xRot = Random.Range(-1f, 1f);
        var yRot = Random.Range(-1f, 1f);
        var zRot = Random.Range(-1f, 1f);
        return new Vector3(xRot, yRot, zRot);
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
        if (transform.position.x > _xBound || transform.position.x < -_xBound)
            _direction = !_direction;
    }
}
