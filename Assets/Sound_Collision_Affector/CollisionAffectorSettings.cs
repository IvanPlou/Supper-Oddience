using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CollisionAffectorSettings : ScriptableObject
{
    public AudioClip[] clips;

    public AudioClip GetClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    // The settings for the Sound Collision Affector
    public float triggerBeforeCollisionMultiplier = 1f; ///How far you have to be for the collision to kick in
    public float audioDeadzone = 0.5f; // How slow the gameobject has to be going for the sound to trigger

    public bool createAudioSource = true; // Use this if you don't feel like manually adding audio sources ( WON'T Create an audio source if one already exists
}
