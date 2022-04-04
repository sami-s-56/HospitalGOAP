using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToToilet : GAction
{
    GAgent gAgent;

    public override bool PrePerform()
    {
        gAgent = GetComponent<GAgent>();
        target = GWorld.RemoveToilet();

        gAgent.agentBeliefs.RemoveState("isFull");

        //if (gAgent.agentBeliefs.HasState("WaitingForNurse"))
        //{
        //    gAgent.agentBeliefs.RemoveState("WaitingForNurse");
        //    GWorld.Instance.GetWorld().ModifyState("PatientWaiting", 1);
        //    GWorld.Instance.WaitingPatients.(gameObject);
        //}
        

        if (target == null)
        {
            return false;
        }
        gAgent.inventory.AddItem(target);
        
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.AddToilet(target);
        gAgent.inventory.RemoveItem(target);

        return true;
    }

}
