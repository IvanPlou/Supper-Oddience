using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour
{
    [SerializeField] Audience.ReactionType reactionType;
    [SerializeField] float points;

    [SerializeField] bool oneTime;
    private bool _used;


    public void React()
    {
        if (_used) return;
        if (oneTime) _used = true;

        if(GameManager.Instance) GameManager.Instance.Audience.React(new Audience.Reaction(points, reactionType));
    }
}
