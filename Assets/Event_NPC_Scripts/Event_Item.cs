using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Event_Item : MonoBehaviour
{
    // perameters
    [SerializeField] GameObject target;
    [SerializeField] int itemInOrder;
    EventDataStructure eDS;

    // checkers
    bool hasCheckIn = false;

    public EventDataStructure GetEEventDataStructure() 
    {
        return eDS;
    }

    private void Update()
    {
        if (hasCheckIn) return;

        Event_Handler.CheckIn();
        hasCheckIn = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals(target.name) && Event_Handler.GetTaskOn() == itemInOrder) 
        {
            Event_Handler.CompleteTask();
        }
    }
}
