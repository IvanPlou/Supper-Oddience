using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetVisuals : MonoBehaviour
{
    private SceneTransitionManager _sceneManager;
    [SerializeField] Image resetDial;

    private void Start()
    {
        _sceneManager = GameManager.Instance.SceneManager;
        _sceneManager.OnResetting += OnResettingDial;
    }

    private void OnDestroy()
    {
        _sceneManager.OnResetting -= OnResettingDial;
    }

    void OnResettingDial(object sender, SceneTransitionManager.OnResettingArgs args)
    {
        resetDial.fillAmount = args.percentage;
    }
}
