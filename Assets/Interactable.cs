using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

// defines what an interactable must be

public interface IInteractable
{


    public virtual void UpdateFocus(Interactor interactor)
    {

    }

    public virtual void Interact(Interactor interactor)
    {

    }

    public virtual void OnInteractRelease(Interactor interactor)
    {

    }
}

// can be interacted with, and triggers events when focus is updated or is interacted with

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour, IInteractable
{
    private Rigidbody _rb;
    private Outline _outline;
    [SerializeField] Outline outline;

    private List<Interactor> _interactors;

    private float _outlineWidth = 5f;
    public Rigidbody RB => _rb;

    public class OnInteractArgs : EventArgs
    {
        public Interactor interactor;
    }

    public EventHandler<OnInteractArgs> OnFocusUpdate;
    public EventHandler<OnInteractArgs> OnInteract;
    public EventHandler<OnInteractArgs> OnInteractRelease;

    public UnityEvent OnInteractEvent;
    public UnityEvent OnFocusUpdateEvent;
    public UnityEvent OnInteractReleaseEvent;

    void Start()
    {
        _rb     = GetComponent<Rigidbody>();
        if (!outline) _outline = GetComponent<Outline>();
        else _outline = outline;
        _outline.OutlineWidth = _outlineWidth;
        _outline.enabled = false;
    }

    public virtual void UpdateFocus(Interactor interactor)
    {
        _outline.OutlineColor = interactor.Color;
        if (interactor.CurrentInteractable != null && interactor.CurrentInteractable == this) _outline.enabled = true;
        else _outline.enabled = false;

        OnFocusUpdate?.Invoke(this, new OnInteractArgs { interactor = interactor });
        OnFocusUpdateEvent?.Invoke();
    }

    public virtual void Interact(Interactor interactor)
    {
        OnInteract?.Invoke(this, new OnInteractArgs { interactor = interactor });
        OnInteractEvent?.Invoke();
    }

    public virtual void ReleaseInteract(Interactor interactor)
    {
        OnInteractRelease?.Invoke(this, new OnInteractArgs { interactor = interactor });
        OnInteractReleaseEvent?.Invoke();
    }
}
