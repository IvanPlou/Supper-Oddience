using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance => _instance;
    private static TaskManager _instance;

    [SerializeField] GameObject taskPrefab;

    [HideInInspector] public List<Task> _tasks = new List<Task>();

    [SerializeField] Vector3 taskListOffset;
    [SerializeField] Vector3 taskStartPosition;
    [SerializeField] Vector3 offScreenOffset;

    [SerializeField] float taskMoveSpeed;

    [SerializeField] Reactor taskFinishedReactor;
    public Reactor TaskReactor => taskFinishedReactor;

    void Start()
    {
        if (!_instance) _instance = this;
        else Destroy(gameObject);
        ScaleOffsets();

        AddNewTask("Grab1", "Left/Right click to grab", 1);
        AddNewTask("Jump1", "Space to jump", 1);
        AddNewTask("Shove1", "Shift to shove", 1);
        AddNewTask("Leave1", "Leave the room", 1);
    }

    void ScaleOffsets()
    {
        taskListOffset *= transform.localScale.x;
        taskStartPosition *= transform.localScale.x;
        offScreenOffset *= transform.localScale.x;

    }

    public Task AddNewTask(string id, string text, int maxProgress)
    {
        Task newTask = Instantiate(taskPrefab, transform).GetComponent<Task>();
        newTask.transform.localPosition = taskStartPosition + (taskListOffset * (_tasks.Count + 1)) + offScreenOffset;
        newTask.Initialize(id, text, maxProgress);

        _tasks.Add(newTask);

        return newTask;

    }

    Task GetTask(string id)
    {
        Task task = null;

        for (int i = 0; i < _tasks.Count; i++)
        {
            if (_tasks[i].Identifier == id)
            {
                task = _tasks[i];
                break;
            }
        }

        if (task == null) return null;

        return task;
    }

    public void CompleteTask(string id)
    {
        Task completeTask = GetTask(id);
        if (completeTask == null) return;
        completeTask.Complete();
    }

    public void FailTask(string id)
    {
        GetTask(id).Fail();
    }

    public void ProgressTask(string id, int progress)
    {
        GetTask(id).Progress(progress);
    }

    // Update is called once per frame
    void Update()
    {
        PositionTasks();

    }

    void PositionTasks()
    {
        for (int i = 0; i < _tasks.Count; i++)
        {
            _tasks[i].transform.localPosition = Vector3.Lerp(_tasks[i].transform.localPosition, taskStartPosition + (taskListOffset * i), taskMoveSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (taskStartPosition * transform.localScale.x), 25f);
        Gizmos.DrawWireSphere(transform.position + (taskStartPosition * transform.localScale.x) + (taskListOffset * transform.localScale.x), 25f);
        Gizmos.DrawWireSphere(transform.position + (taskStartPosition * transform.localScale.x) + (taskListOffset * transform.localScale.x) + (offScreenOffset * transform.localScale.x), 25f);
    }
}
