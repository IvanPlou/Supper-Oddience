using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthContainer : MonoBehaviour
{
    [SerializeField] float health = 10;
    [SerializeField] bool destroyGameObjectOnDie = true;

    [Tooltip("Object to have its parent set to null on death")][SerializeField] GameObject deadObject;
    [SerializeField] bool deadObjectInheretsPosition = true;
    [SerializeField] bool inheretPhysics = true;
    [SerializeField] bool unParentDeadObject = true;

    public UnityEvent OnDamage;
    public UnityEvent OnDie;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Damage(float amount)
    {
        _rb = GetComponent<Rigidbody>();
        health -= amount;
        if (health <= 0)
        {
            Die();
            return;
        }
        OnDamage?.Invoke();
    }

    public void Die()
    {
        if(deadObject != null)
        {
            if(unParentDeadObject) deadObject.transform.parent = null;
            if (deadObjectInheretsPosition) deadObject.transform.position = transform.position;
        }
        
        if (inheretPhysics && deadObject)
        {
            if(_rb == null)
            {
                Debug.LogWarning(gameObject.name + " health container has inheret physics on, but is missing a rigid body component");
            }
            else
            {
                Rigidbody[] childRB = deadObject.GetComponentsInChildren<Rigidbody>();
                for (int i = 0; i < childRB.Length; i++)
                {
                    childRB[i].velocity = _rb.velocity;
                }
            }
        }
        OnDie?.Invoke();
        if (destroyGameObjectOnDie) Destroy(gameObject);
    }
}
