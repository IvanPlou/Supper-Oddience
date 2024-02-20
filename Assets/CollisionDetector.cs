using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] LayerMask whatIsDetect;

    public UnityEvent OnPlayerEnter;
    public UnityEvent OnPlayerExit;

    private void OnCollisionEnter(Collision collision)
    {
        if ((whatIsDetect & (1 << collision.gameObject.layer)) != 0)
            OnPlayerEnter?.Invoke();
    }

    private void OnCollisionExit(Collision collision)
    {
        if ((whatIsDetect & (1 << collision.gameObject.layer)) != 0)
            OnPlayerExit?.Invoke();
    }
}
