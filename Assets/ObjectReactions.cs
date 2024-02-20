using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReactions : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _rotationSpeed = 8.5f;
    
    Vector3 _eulerAngleVelocity;

    [SerializeField]
    private float _floatingTime = 4.0f;

    [SerializeField]
    private bool _isfloating = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _eulerAngleVelocity = new Vector3(30, 90, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        UpwardRotation();
    }

/*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            Debug.Log("Is Triggered");
        }
    }
 */

    /// <summary>
    /// This function moves and rotates object upward  for 4 seconds and then drop it
    /// </summary>
    public void UpwardRotation()
    {
        _isfloating = true;
        _rb.AddRelativeForce(Vector3.up * _speed);
        Quaternion deltaRotation = Quaternion.Euler(_eulerAngleVelocity*_rotationSpeed * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation* deltaRotation);
        StartCoroutine(ObjectFloatingTime());
    }

    IEnumerator ObjectFloatingTime()
    {
        while(_isfloating)
        {
            
            yield return new WaitForSeconds(4);
            _rb.useGravity = true;
            _isfloating = false;    
            _eulerAngleVelocity = Vector3.zero;
        }    
    }
}
