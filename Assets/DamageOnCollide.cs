using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollide : MonoBehaviour
{
    [SerializeField] float damageAmount;

    [SerializeField] float requiredSpeed;

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out HealthContainer healthContainer))
        {
            if(collision.impulse.magnitude > requiredSpeed) { healthContainer.Damage(damageAmount); }
        }
    }
}
