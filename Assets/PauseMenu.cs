using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject[] onObjects;
    [SerializeField] GameObject[] offObjects;

    private bool _isPaused;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if(_isPaused)
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        foreach (var obj in onObjects) { obj.SetActive(true); }
        foreach (var obj in offObjects) { obj.SetActive(false); }
        Time.timeScale = 0f;
        _isPaused = !_isPaused;


    }
    public void UnPause()
    {
        foreach (var obj in onObjects) { obj.SetActive(false); }
        foreach (var obj in offObjects) { obj.SetActive(true); }
        Time.timeScale = 1;
        _isPaused = !_isPaused;
    }
}
