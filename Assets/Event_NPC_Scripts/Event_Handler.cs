using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Handler
{
    static int taskComplete = 0;
    static int tasksInCurrentObjective = 0;
    static int tasksCompletedInObjective = 0;
    public static void CompleteTask() 
    {
        tasksCompletedInObjective++;

        if (tasksCompletedInObjective >= tasksInCurrentObjective) 
        {
            taskComplete++;
            tasksCompletedInObjective = 1;
            tasksCompletedInObjective = 0;
        }        
    }

    public static int GetTaskOn() 
    {
        return taskComplete;
    }

    public static void CheckIn () 
    {
        tasksInCurrentObjective++;
    }
}
