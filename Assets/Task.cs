using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Task : MonoBehaviour
{
    private string _identifier;
    public string Identifier => _identifier;

    [SerializeField] TextMeshProUGUI textMesh;

    [SerializeField] GameObject finishedIcon;
    [SerializeField] GameObject failedIcon;

    private bool _complete;

    private int _maxProgress;
    private int _progress;

    private TaskManager _manager;
    private Animator _animator;

    private float _destroyTime;
    [SerializeField] float timeToDestroy;

    private bool _failed = false;

    private void Update()
    {
        if(_complete && Time.timeSinceLevelLoad > _destroyTime)
        {
            _manager._tasks.Remove(this);
            Destroy(gameObject);
        }
    }

    public void Initialize(string id, string text, int maxProgress)
    {
        _manager = GetComponentInParent<TaskManager>();
        _animator = GetComponent<Animator>();
        textMesh.text = text;
        _identifier = id;
        _maxProgress = maxProgress;
    }

    public void Progress( int progress)
    {
        if (_complete) return;
        _progress += progress;
        if( _progress >= _maxProgress )
        {
            Complete();
        }
    }

    public void Fail()
    {
        finishedIcon.SetActive(false);
        failedIcon.SetActive(true);
        _failed = true;
        Complete();
    }

    public void Finish()
    {
        finishedIcon.SetActive(true);
        failedIcon.SetActive(false);
        Complete();
    }
    
    public void Complete()
    {
        if(!_failed)
        {
            finishedIcon.SetActive(true);
        }
        _destroyTime = Time.timeSinceLevelLoad + timeToDestroy;
        _manager.TaskReactor.React();

        _animator.Play("Complete");
        _complete = true;
    }
}
