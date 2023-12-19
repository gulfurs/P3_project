using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private Transform _bottomPoint = null, _topPoint = null;
    [SerializeField] private bool _enabled = false;

    void FixedUpdate()
    {
        if (_enabled == true)
        {
            if(_bottomPoint != null)
            {
                if (transform.position != _bottomPoint.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, _bottomPoint.position, _speed * Time.deltaTime);
                    if (transform.position == _bottomPoint.position)
                    {
                        _enabled = !_enabled;
                    }
                }
            }
        }
        else
        {
            if (_topPoint != null)
            {
                if(transform.position != _topPoint.position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, _topPoint.position, _speed * Time.deltaTime);
                    if (transform.position == _topPoint.position)
                    {
                        _enabled = !_enabled;
                    }
                }
            }
        }
        
    }
}
