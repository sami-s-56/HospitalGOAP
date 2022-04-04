using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTreated : GAction
{
    GAgent gAgent;
    public override bool PrePerform()
    {
        gAgent = GetComponent<GAgent>();
        gAgent.agentBeliefs.RemoveState("AtHospital");

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
        //GWorld.Instance.GetWorld().ModifyState("IsTreated", 1);
        
        
        gAgent.agentBeliefs.ModifyState("isCured", 1);
        gAgent.inventory.RemoveItem(target);
        return true;
    }
}
