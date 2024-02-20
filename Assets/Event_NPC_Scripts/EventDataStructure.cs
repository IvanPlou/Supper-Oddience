using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDataStructure
{
    BoxCollider triggerZone;
    Event_Item triggerItem;


    public EventDataStructure(Event_Item ei) 
    {
        triggerItem = ei;
    }


    public void AddTriggerZone(BoxCollider bc) 
    {
        triggerZone = bc;
    }
}
