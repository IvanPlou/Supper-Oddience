using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskAssigner : MonoBehaviour
{
    [SerializeField] string taskId;
    [SerializeField] string taskText;
    [SerializeField] int maxProgress;

    public void AssignTask()
    {
        if(TaskManager.Instance != null) 
        {
            TaskManager.Instance.AddNewTask(taskId, taskText, maxProgress);
            Destroy(this);
        }
    }
}
