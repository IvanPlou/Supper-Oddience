using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskFinisher : MonoBehaviour
{
    [SerializeField] int progressionAmount;
    [SerializeField] string id;

    [SerializeField] bool oneTime;
    bool _used;

    public void ProgressTask()
    {
        if (_used) return;
        if (oneTime) _used = true;
        TaskManager.Instance.ProgressTask(id, progressionAmount);
    }
}
