using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Propeller : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] float propelTime;
    private float _stopPropelTime;
    [SerializeField] float propelStrength;
    [SerializeField] Vector3 torqueDirection;

    [SerializeField] Vector3 propelDirection;

    bool propelling;

    private void Start()
    {
        _rb= GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (propelling && Time.timeSinceLevelLoad > _stopPropelTime)
        {
            _rb.AddForce(transform.rotation * propelDirection.normalized * propelStrength);
            _rb.AddTorque(transform.rotation * torqueDirection.normalized * propelStrength);
        }
        else
        {
            this.enabled = false;
        }
    }

    public void Propel()
    {
        propelling = true;
        _stopPropelTime = Time.timeSinceLevelLoad + propelTime;
    }


}
