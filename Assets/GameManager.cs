using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // singleton pattern
    public static GameManager Instance;
    private SceneTransitionManager _sceneManager;
    [SerializeField] AudioSource uiSounds;
    public AudioSource UiSounds => uiSounds;
    [HideInInspector] public SceneTransitionManager SceneManager => _sceneManager;

    private Audience _audience;
    [HideInInspector] public Audience Audience => _audience;
    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(gameObject);
        if(!_sceneManager) _sceneManager = GetComponent<SceneTransitionManager>();
        if (!_audience) _audience = GetComponent<Audience>(); 
    }

    public void PlayUIAudio(AudioClip clip)
    {
        uiSounds.PlayOneShot(clip);
    }
}
