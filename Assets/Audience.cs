using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Audience : MonoBehaviour
{
    private float _totalPoints;
    public float TotalPoints => _totalPoints;

    [SerializeField] float pointGoal;

    [SerializeField] float pointDecay;

    [SerializeField] AudioClip[] clapClips;
    [SerializeField] AudioClip[] cheerClips;
    [SerializeField] AudioClip[] laughClips;

    [SerializeField] AudioClip[] unEnthusedClips;

    [SerializeField] AudioClip[] idleClips;

    [SerializeField] float minPitch;
    [SerializeField] float maxPitch;

    [SerializeField] AudioSource[] sources;

    private float _nextIdleAudioTime;
    [SerializeField] float minIdleWaitTime;
    [SerializeField] float maxIdleWaitTime;

    private Reaction[] _reactions = new Reaction[3];

    public EventHandler<OnPointsUpdateEventArgs> OnPointsUpdate;

    public class OnPointsUpdateEventArgs : EventArgs
    {
        public float points;
        public float goal;
    }

    public enum ReactionType
    {
        Clap,
        Cheer,
        Laugh,
    }

    public class Reaction
    {
        public float value;
        public ReactionType type;

        public Reaction(float value, ReactionType type)
        {
            this.value = value;
            this.type = type;
        }
    }

    private void Update()
    {
        DecayPoints();
        ClampPoints();
        IdleAudio();
    }

    private void Start()
    {
        ResetIdleCountdown();
    }

    public void React(Reaction reaction)
    {

        for (int i = _reactions.Length - 1; i > 0; i--)
        {
            _reactions[i - 1] = _reactions[i];
        }
        _reactions[_reactions.Length - 1] = reaction;

        float pointsToAdd = reaction.value;
        bool playClip = true;

        if (_reactions[_reactions.Length - 2] != null && _reactions[_reactions.Length - 2].type == reaction.type)
        {
            pointsToAdd /= 2;
            PlayUnenthusedClip();
            playClip = false;
        }
        else if (reaction.type != ReactionType.Clap && _reactions[_reactions.Length - 3] != null && _reactions[_reactions.Length - 3].type == reaction.type)
        {
            pointsToAdd /= 3;
            playClip = false;
        }
        
        if(playClip) PlayClip(reaction.type);

        ResetIdleCountdown();

        _totalPoints += pointsToAdd * 3;
    }

    public void Reset()
    {
        _totalPoints = 0;
        OnPointsUpdate?.Invoke(this, new OnPointsUpdateEventArgs { goal = pointGoal, points = _totalPoints });
        for (int i = 0; i < _reactions.Length; i++)
        {
            _reactions[i] = null;
        }
    }

    void PlayClip(ReactionType type)
    {
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].pitch = UnityEngine.Random.Range(minPitch, maxPitch);
        }
        switch (type)
        {
            case ReactionType.Clap:
                sources[0].clip = clapClips[UnityEngine.Random.Range(0, clapClips.Length)];
                sources[0].Play();
                // code block
                break;
            case ReactionType.Cheer:
                sources[1].clip = cheerClips[UnityEngine.Random.Range(0, cheerClips.Length)];
                sources[1].Play();
                // code block
                break;
            case ReactionType.Laugh:
                sources[2].clip = laughClips[UnityEngine.Random.Range(0, laughClips.Length)];
                sources[2].Play();
                // code block
                break;
        }
    }

    void PlayUnenthusedClip()
    {
        sources[3].PlayOneShot(unEnthusedClips[UnityEngine.Random.Range(0, unEnthusedClips.Length)]);
        // play an unenthused audio clip
    }

    void PlayIdleClip()
    {
        sources[4].clip = idleClips[UnityEngine.Random.Range(0, idleClips.Length)];
        sources[4].Play();
        // play an intermittent audio clip
    }

    void DecayPoints()
    {
        _totalPoints -= Time.deltaTime * pointDecay;
    }

    void ClampPoints()
    {
        _totalPoints = Mathf.Clamp(_totalPoints, 0, pointGoal);
        OnPointsUpdate?.Invoke(this, new OnPointsUpdateEventArgs { points = _totalPoints, goal = pointGoal});
    }

    void IdleAudio()
    {
        if(Time.timeSinceLevelLoad > _nextIdleAudioTime)
        {
            PlayIdleClip();
            ResetIdleCountdown();
        }
    }

    void ResetIdleCountdown()
    {
        _nextIdleAudioTime = Time.timeSinceLevelLoad + UnityEngine.Random.Range(minIdleWaitTime, maxIdleWaitTime);
    }

}
