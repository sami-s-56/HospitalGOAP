using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReliefMonitor : GStateMonitor
{
    [SerializeField] GameObject puddleReference;

    private new void Start()
    {
        base.Start();
    }

    protected override void PerformAction(int actionNo) 
    {
        if(actionNo <= timesForEachAction[0])   //Here it can be changed to any value depending on how long we want to perform 1st action
        {
            SubGoal g = _agent.FindGoalByName("Relieved");

            if (g != null)
            {
                _agent.ChangeGoalPriority(g, 10);
            }
        }
        else
        {
            //Perform Secondary Action 
            Debug.LogError("Secondary Action Performed");
            if(puddleReference != null)
            {
                Instantiate(puddleReference, transform.position, transform.rotation);
            }
            _agent.agentBeliefs.RemoveState(stateToMonitor);
        }

        base.PerformAction(actionNo);
    }
}
