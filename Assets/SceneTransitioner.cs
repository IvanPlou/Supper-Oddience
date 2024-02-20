using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitioner : MonoBehaviour
{
    [SerializeField] float transitionDuration = 1.5f;
    [SerializeField] float loadPoint = 0.5f;
    [SerializeField] int sceneIndex;
    [SerializeField] bool reset = false;
    public void TransitionScene()
    {
        GameManager.Instance.SceneManager.LoadScene(sceneIndex, transitionDuration, loadPoint, reset);
    }
}
