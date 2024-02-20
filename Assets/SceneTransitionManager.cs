using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    private float _loadFinishedTime;
    private float _transitionFinishedTime;
    private bool _transitioning;
    private bool _loading;
    private int _sceneToLoad;

    [SerializeField] float resetTime;
    private float _resetDelay;

    public class OnResettingArgs : EventArgs
    {
        public float percentage;
    }

    public EventHandler<OnResettingArgs> OnResetting;

    public System.EventHandler OnTransitionStart;
    public System.EventHandler OnLoadFinished;
    public System.EventHandler OnTransitionFinished;

    public UnityEvent OnTransitionStartEvent;
    public UnityEvent OnLoadFinishedEvent;
    public UnityEvent OnTransitionFinishedEvent;

    // Update is called once per frame
    void Update()
    {
        if (_transitioning)
        {
            if(Time.time > _transitionFinishedTime)
            {
                OnTransitionFinishedEvent?.Invoke();
                OnTransitionFinished?.Invoke(this, EventArgs.Empty);
                _transitioning = false;
            }
        }
        if (_loading)
        {
            if (Time.time > _loadFinishedTime)
            {
                _resetDelay = 0;
                OnResetting?.Invoke(this, new OnResettingArgs { percentage = _resetDelay });
                SceneManager.LoadScene(_sceneToLoad);
                OnLoadFinishedEvent?.Invoke();
                OnLoadFinished?.Invoke(this, EventArgs.Empty);
                _loading = false;
            }
        }

        // if the player holds the reset button for long enough, then reload the active scene
        if (Input.GetButton("Reset") && !_loading && !_transitioning && SceneManager.GetActiveScene().buildIndex != 0)
        {
            OnResetting?.Invoke(this, new OnResettingArgs { percentage = _resetDelay / resetTime });
            _resetDelay += Time.deltaTime;
            if (_resetDelay >= resetTime)
            {
                LoadScene(SceneManager.GetActiveScene().buildIndex);
                GameManager.Instance.Audience.Reset();
            }
        }
        else if(_resetDelay > 0)
        {
            OnResetting?.Invoke(this, new OnResettingArgs { percentage = _resetDelay / resetTime });
            _resetDelay = Mathf.Clamp (_resetDelay - Time.deltaTime * 2, 0, resetTime);
        }
    }

    /// <summary>
    /// transition to a new scene
    /// </summary>
    /// <param name="transitionTime">transition time is the total duration of the transition</param>
    /// <param name="loadPoint">load point is the point in the transition (ranging from 0 to 1) where the scene changes</param>
    public void LoadScene(int sceneIndex, float transitionTime = 1.5f, float loadPoint = 0.5f, bool reset = false)
    {
        if (reset) { GameManager.Instance.Audience.Reset(); }

        loadPoint = Mathf.Clamp(loadPoint, 0, 1);

        _loading = true;
        _transitioning = true;

        _loadFinishedTime = Time.time + transitionTime * loadPoint;
        _transitionFinishedTime = Time.time + transitionTime;
        _sceneToLoad = sceneIndex;

        OnTransitionStart?.Invoke(this, EventArgs.Empty);
        OnTransitionStartEvent?.Invoke();
    }
}
