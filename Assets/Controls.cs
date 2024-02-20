using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField] Character character;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpTorque;

    [SerializeField] Transform handTarget;

    [SerializeField] float handSpeed;
    [SerializeField] Rigidbody[] handRigidbodies; 

    private Vector3 _inputDir;
    private Vector3 _previousInputDir;

    private bool _shoving;

    [Header("Shove Stuff")]

    [SerializeField] float shoveCooldown;
    private float _nextShoveTime = 0;

    [SerializeField] float shoveTime;
    private float _shoveFinishedTime = 0;
    [SerializeField] float playerShoveForce;
    [SerializeField] float shoveStrength;
    [SerializeField] Transform shoveTransform;
    [SerializeField] float shoveRadius;
    [SerializeField] float shoveDamage;

    [SerializeField] GameObject shoveParticle;

    private void Update()
    {
        Inputs();
        character.Move(_inputDir.normalized);
        if(_inputDir.magnitude > 0.01)character.Look(_inputDir.normalized);
        if (Input.GetButtonDown("Jump") && character.Grounded)
        {
            if (TaskManager.Instance) TaskManager.Instance.CompleteTask("Jump1");
            character.RB.AddForce(Vector3.up * jumpForce);
            character.RB.AddTorque(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)) * jumpTorque);
        }

        if(Input.GetButtonDown("Shove") && !_shoving && Time.timeSinceLevelLoad > _nextShoveTime)
        {
            StartShove();
            if (TaskManager.Instance) TaskManager.Instance.CompleteTask("Shove1");
        }
        if (Time.timeSinceLevelLoad > _shoveFinishedTime && _shoving) CancelShove();

        if (_shoving)
        {
            ShoveCheck();
        }
    }

    private void FixedUpdate()
    {
        if (_inputDir.magnitude < 0.01) return; 
        for (int i = 0; i < handRigidbodies.Length; i++)
        {
            handRigidbodies[i].AddForce((handTarget.position - handRigidbodies[i].position).normalized * handSpeed * Time.fixedDeltaTime);
        }
    }

    void Inputs()
    {
        _inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if(_inputDir.magnitude > 0.01f) _previousInputDir = _inputDir; 
    }

    void StartShove()
    {
        _shoving = true;
        _shoveFinishedTime = Time.timeSinceLevelLoad + shoveTime;
        _nextShoveTime = Time.timeSinceLevelLoad + shoveCooldown;
        character.RB.AddForce(_previousInputDir * playerShoveForce, ForceMode.Impulse);

        for (int i = 0; i < handRigidbodies.Length; i++)
        {
            handRigidbodies[i].AddForce(_previousInputDir * playerShoveForce * 0.4f, ForceMode.Impulse);
        }
    }

    void ShoveCheck()
    {
        Collider[] shoveColliders = Physics.OverlapSphere(shoveTransform.position, shoveRadius);
        if (shoveColliders.Length < 1) return;

        for(int i = 0;i < shoveColliders.Length;i++)
        {
            NPC_Controller characterToShove = shoveColliders[i].GetComponentInParent<NPC_Controller>();
            if (characterToShove != null)
            {
                characterToShove.Character.RB.AddForce(_previousInputDir * shoveStrength, ForceMode.Impulse);
                characterToShove.Character.GoLimp();
                characterToShove.HealthContainer.Damage(shoveDamage);
                if (shoveParticle) Instantiate(shoveParticle, characterToShove.transform.position, Quaternion.identity);
                CancelShove();
                break;
            }
        }

    }

    void CancelShove()
    {
        _shoving = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(shoveTransform.position, shoveRadius);
    }
}
