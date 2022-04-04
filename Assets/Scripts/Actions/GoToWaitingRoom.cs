using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToWaitingRoom : GAction
{
    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetWorld().ModifyState("PatientWaiting", 1);
        GWorld.Instance.WaitingPatients.Enqueue(gameObject);
        GetComponent<GAgent>().agentBeliefs.ModifyState("WaitingForNurse", 1);
        return true;
    }
}
