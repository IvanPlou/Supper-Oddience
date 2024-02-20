using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterReactions : MonoBehaviour
{
    [SerializeField]
    private bool _isSoundActive = false;

    [SerializeField]
    private bool _isHurtActive = false;
   
    private Vector3 _lookAtPosition;

    [SerializeField]
    private GameObject _loudNoiseEmittor;

    private Character _character;

    private Animator _animator;

    private float _animationTimer = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        _character = GetComponent<Character>();
        if (_character == null)
        {
            Debug.LogError("Characteer is NULL");
        }

        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isSoundActive)
        {
            LoudNoiseReaction();
        }
        
        if (_isHurtActive) 
        {
            AngryReaction();
        }
       
    }
    /// <summary>
    /// Using this method NPC will look towards the noise direction if issoundactive(bool) is true
    /// It also activate surprise animation
    /// </summary>
    public void LoudNoiseReaction()
    {
        _lookAtPosition = _loudNoiseEmittor.transform.position;
        _character.Look((_lookAtPosition - transform.position).normalized);
        //  _animator.Play("Expression_Surprised");
        _animator.Play("Expression_Surprised");
    }

    /// <summary>
    /// It will activate angry animation when you hurt any NPC
    /// Using isHurtActive Variable to activate it
    /// </summary>
    public  void AngryReaction()
    {
        _animator.Play("Expression_Angry");
    }
}
