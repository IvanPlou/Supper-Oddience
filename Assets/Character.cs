using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{

    private Rigidbody _rb;
    public Rigidbody RB => _rb;
    private float _unLimpPoint;
    private bool _limp;

    public bool Limp => _limp;

    private bool _knockedOut = false;

    [Header("Movement Properties")]
    [SerializeField] float turnSpeed;
    [SerializeField] float speed;
    [SerializeField] float acceleration;
    [SerializeField] float decelleration;

    [Header("Limp Properties")]
    [SerializeField] float unLimpSpeed;
    [SerializeField] float unLimpTime;

    [Header("Standing and Spring properties")]
    [SerializeField] float targetHeight;
    [SerializeField] float groundDistance;
    [SerializeField] float springStrength;
    [SerializeField] float springDamper;
    [SerializeField] LayerMask whatIsGround;
    private Vector3 _previousLookDir;

    bool _grounded;

    bool _dead;

    public bool Grounded => _grounded;

    float _maxGroundAngle = 50;

    Vector3 _previousMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Die()
    {
        GoLimp();
        _unLimpPoint = Mathf.Infinity;
        _dead = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_dead)
        if(_limp && _rb.velocity.magnitude < unLimpSpeed && !_knockedOut)
        {
            if(Time.timeSinceLevelLoad > _unLimpPoint)
            {
                UnLimp();
            }
        }
        else if(_limp)
        {
            _unLimpPoint = Time.timeSinceLevelLoad + unLimpTime;
        }
        else
        {
            Stand();
        }
    }

    // Make the character stand up if they are above solid ground.
    void Stand()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_previousLookDir, Vector2.up), turnSpeed * Time.deltaTime);
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector2.down);

        Physics.Raycast(ray, out hit, groundDistance, whatIsGround, QueryTriggerInteraction.UseGlobal);

        bool rayDidHit = hit.collider != null;

        if(rayDidHit && IsGround(hit.normal))
        {
            _grounded = true;
            SpringForce(hit);
        }
        else
        {
            _grounded = false;
        }
    }

    // spring the player to a target height above the ground
    void SpringForce(RaycastHit hit)
    {
        Vector3 springDir = Vector3.up;
        float offset = targetHeight - hit.distance;
        float speed = Vector3.Dot(springDir, _rb.velocity);
        float force = (offset * springStrength) - (speed * springDamper);

        _rb.AddForce(springDir * force);
    }

    // check normal to see if it is viable ground
    bool IsGround(Vector3 normal)
    {
        return Vector3.Angle(Vector3.up, normal) < _maxGroundAngle;
    }

    /// <summary>
    /// move and orient the character in the direction of movement
    /// </summary>
    /// <param name="direction">the direction to move the character in</param>
    public void Move(Vector3 direction)
    {
        if (_limp) return;
        direction *= acceleration;
        if (direction.magnitude > 0.01f)
        {
            if((_rb.velocity + (direction * Time.deltaTime)).magnitude < speed)
            {
                _rb.velocity += direction * Time.deltaTime;
            }
            _previousMoveDirection = transform.forward;
        }
        else if (_rb.velocity.magnitude > 0.01f && _grounded)
        {
            _rb.velocity -= _rb.velocity * decelleration * Time.deltaTime;
        }
    }

    public void Look(Vector3 direction)
    {
        _previousLookDir = new Vector3(direction.x, 0, direction.z).normalized;
    }

    /// <summary>
    /// make the character become ragdoll
    /// </summary>
    public void GoLimp()
    {
        _unLimpPoint = Time.timeSinceLevelLoad + unLimpTime;
        _grounded = false;
        _limp = true;
        _rb.freezeRotation = false;
    }

    public void UnLimp()
    {
        _limp = false;
        _rb.freezeRotation = true;
        transform.forward = _previousMoveDirection;
    }

    private void OnDrawGizmos()
    {
        // visualize the standing spring properties
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position + (Vector3.down * targetHeight / 2), new Vector3(0.1f, targetHeight, 0.1f));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * groundDistance));
    }

    public void KnockOut()
    {

    }
}
