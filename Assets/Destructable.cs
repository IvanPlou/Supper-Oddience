using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthContainer))]
public class Destructable : MonoBehaviour
{
    [SerializeField] float impulseDamageThreshold;
    [SerializeField] float damageMultiplier;

    private HealthContainer _healthContainer;

    private void Start()
    {
        _healthContainer = GetComponent<HealthContainer>();
    }

    // deal damage to a health container if the collision impulse passes the threshold
    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < collision.contacts.Length; i++) 
        {
            if (collision.contacts[i].impulse.magnitude > impulseDamageThreshold)
            {
                _healthContainer.Damage(collision.contacts[i].impulse.magnitude * damageMultiplier);
            }
        }
    }
}
