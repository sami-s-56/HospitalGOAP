using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestMonitor : GStateMonitor
{
    private new void Start()
    {
        base.Start();
    }

    protected override void PerformAction(int actionNo) 
    {

        SubGoal g = _agent.FindGoalByName("Rested");
        if(g != null)
        {
            _agent.ChangeGoalPriority(g, 10);
        }
        _agent.agentBeliefs.RemoveState(stateToMonitor);

        base.PerformAction(actionNo);
    }
}
