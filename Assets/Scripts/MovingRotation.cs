using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRotation : MonoBehaviour
{
    private float _rotSpeed = 0.1f;
    private float _maxRot = 45;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Rotate(float direction)
    {
        if (direction > 0)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, _maxRot), Time.time * _rotSpeed);
        else if (direction < 0)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -_maxRot), Time.time * _rotSpeed);
        else
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0), Time.time * _rotSpeed);
    }
}
