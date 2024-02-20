using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Interactor : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] Color color;
    public Color Color => color;

    FixedJoint _handJoint;
    public FixedJoint HandJoint => _handJoint;

    public UnityEvent OnInteractEvent;
    public EventHandler OnInteract;

    [SerializeField] float pickUpRadius = 0.5f;
    [SerializeField] float interactAttractionStrength;

    [SerializeField] string interactInput;

    private Interactable _previousInteractable;
    private Interactable _focusedInteractable;

    private bool _holding = false;

    public Interactable CurrentInteractable => _focusedInteractable;

    [SerializeField] LayerMask WhatIsInteractable;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // check for nearby interactables
    void FixedUpdate()
    {
        // if an object is already being held, do not check for more 
        // if the object is destroyed well being held, then reset the holding state.

        if (_holding)
        {
            if (_focusedInteractable == null) 
            {
                Drop();
            } 
            else return;
        }

        Collider[] colliders =  Physics.OverlapSphere(transform.position + transform.up * pickUpRadius, pickUpRadius, WhatIsInteractable, QueryTriggerInteraction.UseGlobal);

        float shortestDistance = Mathf.Infinity;

        Interactable possibleFocus;

        int nearestIndex = -1;


        // find the nearest interactable to the interactor
        for (int i = 0; i < colliders.Length; i++)
        {
            if (Vector2.Distance(colliders[i].transform.position, transform.position) < shortestDistance && colliders[i].GetComponent<Interactable>() != null)
            {
                nearestIndex = i;
            }
            else
            {
                continue;
            }
        }

        // no interactables were found, so set the focused to none
        if(nearestIndex == -1)
        {
            if (_focusedInteractable) _previousInteractable = _focusedInteractable;
            _focusedInteractable = null;
            if(_previousInteractable) _previousInteractable.UpdateFocus(this);
            _previousInteractable = null;
        }

        // update the focus of the previous interactable and the new focused interactable
        else if (colliders[nearestIndex].TryGetComponent(out possibleFocus) && possibleFocus != _focusedInteractable)
        {
            if(_focusedInteractable) _previousInteractable = _focusedInteractable;
            _focusedInteractable = possibleFocus;

            if(_previousInteractable)_previousInteractable.UpdateFocus(this);
            _focusedInteractable.UpdateFocus(this);
            _previousInteractable = null;
        }

        if(_focusedInteractable != null)
        {
            Vector3 attractDirection = (_focusedInteractable.transform.position - transform.position).normalized;
            _rb.AddForce(attractDirection * interactAttractionStrength * Time.fixedDeltaTime);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown(interactInput) && _focusedInteractable)
        {
            Pickup(_focusedInteractable);
        }
        if(Input.GetButtonUp(interactInput) && _focusedInteractable)
        {
            Drop();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.up * pickUpRadius, pickUpRadius);
    }

    public void Pickup(Interactable interactable)
    {
        if (TaskManager.Instance) TaskManager.Instance.CompleteTask("Grab1");

        _holding = true;
        _handJoint = gameObject.AddComponent<FixedJoint>();
        _handJoint.connectedBody = interactable.RB;
        _focusedInteractable.Interact(this);
    }

    public void Drop()
    {
        _holding = false;
        Destroy(_handJoint);
        _focusedInteractable.ReleaseInteract(this);
    }
}
