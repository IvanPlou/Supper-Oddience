using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TriggerVolume : MonoBehaviour
{
    [SerializeField] LayerMask whatIsDetect;

    public UnityEvent OnPlayerEnter;
    public UnityEvent OnPlayerExit;

    private void OnTriggerEnter(Collider other)
    {
        if ((whatIsDetect & (1 << other.gameObject.layer)) != 0)
            OnPlayerEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if ((whatIsDetect & (1 << other.gameObject.layer)) != 0)
            OnPlayerExit?.Invoke();
    }
}
