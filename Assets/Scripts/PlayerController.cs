using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _speed = 10;
    private float _zBound = 14f;
    private float _xBound = 26f;

    private float _rotSpeed = 2f;
    private float _maxRot = 30;

    private Transform _spaceship;

    // Start is called before the first frame update
    void Start()
    {
        _spaceship = transform.GetChild(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
        ConstraintPlayerPosition();
    }

    //Moves the player on input
    private void MovePlayer()
    {
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * _speed * verticalInput);
        transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
        RotateOnMoving(horizontalInput);
    }

    //Constraint player from moving out of bounds
    private void ConstraintPlayerPosition()
    {
        if (transform.position.z <= -_zBound)
            transform.position = new Vector3(transform.position.x, transform.position.y, -_zBound);
        if (transform.position.z >= _zBound)
            transform.position = new Vector3(transform.position.x, transform.position.y, _zBound);

        if (transform.position.x <= -_xBound)
            transform.position = new Vector3(-_xBound, transform.position.y, transform.position.z);
        if (transform.position.x >= _xBound)
            transform.position = new Vector3(_xBound, transform.position.y, transform.position.z);
    }

    private void RotateOnMoving(float direction)
    {
        var targetAngle = 0f;

        if (direction > 0)
            targetAngle = -_maxRot;
        else if (direction < 0)
            targetAngle = _maxRot;

        var euler = Quaternion.Euler(_spaceship.eulerAngles.x, _spaceship.eulerAngles.y, targetAngle);
        _spaceship.rotation = Quaternion.Lerp(_spaceship.rotation, euler, Time.deltaTime * _rotSpeed);
    }
}
