using UnityEngine;

public class MovingRotation : MonoBehaviour
{
    private float _rotSpeed = 0.1f;
    private float _maxRot = 45;

    public void Rotate(float direction)
    {
        if (direction > 0)
            transform.rotation = Quaternion.Lerp(transform.rotation, 
                Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, _maxRot), 
                Time.time * _rotSpeed);
        else if (direction < 0)
            transform.rotation = Quaternion.Lerp(transform.rotation, 
                Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -_maxRot), 
                Time.time * _rotSpeed);
        else
            transform.rotation = Quaternion.Lerp(transform.rotation, 
                Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0), 
                Time.time * _rotSpeed);
    }
}
