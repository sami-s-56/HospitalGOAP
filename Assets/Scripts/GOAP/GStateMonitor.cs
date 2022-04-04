using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonitorType
{
    TimeBased,  //Based on time after precondition is true
    StateBased  //Based on value of state in agent beliefs 
}

/// <summary>
/// This class can be used to monitor state of the agent and perform certain tasks if agent is holding certain befief for some duration
/// An e.g. of that would be increasing priority of some goals overtime whose preconditions are met but agent didint achieved that because of other goals 
/// </summary>
public class GStateMonitor : MonoBehaviour
{
    protected GAgent _agent;  //Agent whose state is being monitored

    [SerializeField] protected string stateToMonitor;
    [SerializeField] protected float stateDecayTime;
    [SerializeField] protected float stateDecayRate;

    [SerializeField] protected MonitorType mType;

    float currentTime = 0f;

    int aNo = 1;

    //Value of state that should be there for triggering actio (like Value 2 for 1st action 3-5 for second action, and so on 
    [SerializeField] protected int[] timesForEachAction;

    protected void Start()
    {
        _agent = GetComponent<GAgent>();
    }

    private void LateUpdate()
    {
        if(mType == MonitorType.TimeBased)
        {
            if (_agent.agentBeliefs.HasState(stateToMonitor))
            {
                if (_agent.currentAction != null && _agent.currentAction.beingPerformed)
                {
                    return;
                }
                else
                {
                    currentTime += stateDecayRate;
                    if (currentTime >= stateDecayTime)
                    {
                        PerformAction(_agent.agentBeliefs.GetStateValue(stateToMonitor));   
                    }
                }
            }
        }
        else
        {
            if (_agent.currentAction != null && _agent.currentAction.beingPerformed)
            {
                return;
            }
            PerformAction(_agent.agentBeliefs.GetStateValue(stateToMonitor));
        }
    }

    protected virtual void PerformAction(int actionNo)
    {
        currentTime = 0f;
        
    }

}
