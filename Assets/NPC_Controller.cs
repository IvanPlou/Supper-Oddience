using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(HealthContainer))]
public class NPC_Controller : MonoBehaviour
{
    private Character _character;
    public Character Character => _character;

    private HealthContainer _healthContainer;
    public HealthContainer HealthContainer => _healthContainer;

    private bool _isFollowingTarget;
    private Vector3 _targetPosition;
    private Vector3 _lookAtPosition;

    private float _nextJerkTime;

    [SerializeField] float minJerkTime;
    [SerializeField] float maxJerkTime;
    [SerializeField] float jerkStrength;

    [SerializeField] bool followPlayer;
    [SerializeField] float detectionRadius;
    [SerializeField] LayerMask whatIsPlayer;

    void Start()
    {
        _character = GetComponent<Character>();
        _healthContainer = GetComponent<HealthContainer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            bool foundPlayer = Physics.CheckSphere(transform.position, detectionRadius, whatIsPlayer);

            if (foundPlayer)
            {
                { _targetPosition = Physics.OverlapSphere(transform.position, detectionRadius, whatIsPlayer)[0].transform.position; }
                _isFollowingTarget = true;
            }
        }

        if (_isFollowingTarget)
        {
            Vector3 moveDir = _targetPosition - transform.position;
            
            _character.Move(moveDir.normalized);
            _character.Look((_lookAtPosition - transform.position).normalized);
        }
        else
        {
            _character.Move(Vector3.zero);
            if(_nextJerkTime < Time.timeSinceLevelLoad)
            {
                _lookAtPosition = transform.position + (Random.insideUnitSphere * 10);
                _character.RB.AddForce(Random.insideUnitSphere * jerkStrength);
                _nextJerkTime = Time.timeSinceLevelLoad + Random.Range(minJerkTime, maxJerkTime);
            }
        }
        _character.Look((_lookAtPosition - transform.position).normalized);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}
