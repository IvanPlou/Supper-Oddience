using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCollisionAffector : MonoBehaviour
{
    // The settings preset
    [SerializeField] CollisionAffectorSettings settings;

    // Options
    bool createAudioSource = true; // Use this if you don't feel like manually adding audio sources ( WON'T Create an audio source if one already exists
    float audioDeadzone = 0.2f; // How fast the game object has to be moving for the audio to trigger
    bool isAbleToRun = true; // fail safe in even of missing options


    // The referances to needed components
    AudioSource audioS;

    // Created stuff
    BoxCollider soundTrigger;

    // Start is called before the first frame update
    void Start()
    {
        if (settings == null) 
        {
            Debug.Log("No settings present on gameobject: " + gameObject.name + " \nScript won't run");
            isAbleToRun = false;
            return;
        }

        // Transcribe from scriptable object
        createAudioSource = settings.createAudioSource;

        audioDeadzone = settings.audioDeadzone;

        // Adds audiosource if one doesn't exist and it's specific in createAudioSource variable
        if (createAudioSource && (audioS = GetComponent<AudioSource>()) == null) 
        {
            audioS = gameObject.AddComponent<AudioSource>();
            audioS.playOnAwake = false;
            audioS.spatialBlend = 1;
            audioS.minDistance = 5f;
            audioS.maxDistance = 60f;
            audioS.volume = 0.8f;
        }


        // Creates a trigger for alloewd for advanced timings
        BoxCollider referanceBox = GetComponent<BoxCollider>();
        soundTrigger = gameObject.AddComponent<BoxCollider>();
        soundTrigger.isTrigger = true;
    }

    private void OnCollisionEnter(Collision other)
    {


        // in the event the constructor never finished
        if (!isAbleToRun || other.impulse.magnitude < audioDeadzone) 
        {
            return;
        }

        // Designations
        float collisionForce = other.impulse.magnitude;
        float pitch = collisionForce * 2;
        float volume = collisionForce * 2;

        pitch = Mathf.Clamp(pitch, 0.5f, 1.5f);
        volume = Mathf.Clamp(pitch, 0.5f, 1f);

        audioS.pitch = pitch;

        audioS.PlayOneShot(settings.GetClip(), volume);
    }
}