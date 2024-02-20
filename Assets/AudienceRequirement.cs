using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudienceRequirement : MonoBehaviour
{
    [SerializeField] float requirement;

    private bool _triggered;

    public UnityEvent OnRequirementReached;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Audience.OnPointsUpdate += CheckRequirement;
    }

    void CheckRequirement(object sender, Audience.OnPointsUpdateEventArgs args)
    {
        if(_triggered) { return; }
        if(args.points > requirement)
        {
            OnRequirementReached?.Invoke();
        }
    }
}
