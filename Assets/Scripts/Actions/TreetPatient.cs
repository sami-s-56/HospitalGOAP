using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreetPatient : GAction
{
    Nurse gAgent;

    public override bool PrePerform()
    {
        duration = Random.Range(8f, 15f);
        GetComponent<Nurse>().AssignedPatient.GetComponent<GAgent>().currentAction.duration = duration;

        GameObject cubicle = GetComponent<GAgent>().inventory.FindItemWithTag("Cubicle");
        
        if (cubicle != null)
        {
            target = cubicle;
            return true;
        }

        return false;
    }

    public override bool PostPerform()
    {
        GWorld.AddCubical(target);
        gAgent = GetComponent<Nurse>();
        gAgent.inventory.RemoveItem(target);
        gAgent.AssignedPatient = null;
        return true;
    }

}
