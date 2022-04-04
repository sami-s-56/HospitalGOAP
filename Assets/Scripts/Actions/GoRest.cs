using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoRest : GAction
{

    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
        GetComponent<GAgent>().agentBeliefs.RemoveState("isTired");
        GetComponent<GAgent>().ResetPriority("Rested");
        return true;
    }
}
