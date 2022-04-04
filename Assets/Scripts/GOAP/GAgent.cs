using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;

public class SubGoal
{
    public Dictionary<string, int> sGoals;  //List of world states
    public bool isRemovable;

    public SubGoal(string k, int v, bool r)
    {
        sGoals = new Dictionary<string, int>(); 
        sGoals.Add(k, v);
        isRemovable = r;
    }
}

[RequireComponent(typeof(GAgentVisual))]
public class GAgent : MonoBehaviour
{
    //List of all actions that a player can perform
    public List<GAction> actions = new List<GAction>();

    //List of all the agent goals 
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();

    //List of goals whose priority has been changed
    Dictionary<SubGoal, int> defaultPriorities = new Dictionary<SubGoal, int>();

    GPlanner planner;   //Reference to the planner

    [SerializeField]
    Queue<GAction> actionQueue; //To be populated by Planner
    public GAction currentAction; //Current action that is being executed
    SubGoal currentGoal;    //The current goal to achieve

    public WorldStates agentBeliefs = new WorldStates();    //Current Agent States

    private bool invoked = false;

    public GInventory inventory = new GInventory();

    public void Start()
    {
        GAction[] acts = this.GetComponents<GAction>();
        foreach (GAction action in acts)
        {
            actions.Add(action);
        }
    }

    public void LateUpdate()
    {
        if (currentAction != null && currentAction.beingPerformed)
        {

            float distanceToTarget = Vector3.Distance(currentAction.target.transform.position, transform.position);

            if (distanceToTarget <= 2f)
            {
                if (!invoked)
                {
                    print("Trying To Invoke");
                    Invoke("ActionPerformed", currentAction.duration);
                    invoked = true;
                }
            }
            else
            {
                if (transform.name.Contains("Nurse"))
                {
                    Debug.LogWarning(currentAction.agent.hasPath + " " + currentAction.agent.remainingDistance);
                }

            }
            return;
        }

        if (planner == null || actionQueue == null)
        {
            planner = new GPlanner();

            var sortedGoals = from entry in goals orderby entry.Value descending select entry;

            foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
            {
                actionQueue = planner.plan(actions, sg.Key.sGoals, agentBeliefs);
                if (actionQueue != null)
                {
                    currentGoal = sg.Key;
                    break;
                }
            }

            print("No Plan To Execute");
        }

        if (actionQueue != null && actionQueue.Count == 0)
        {
            if (currentGoal.isRemovable)
            {
                goals.Remove(currentGoal);
            }
            planner = null;
        }

        if (actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            print("Current Action:" + currentAction.actionName);
            if (currentAction.PrePerform())
            {
                if (currentAction.target == null && currentAction.targetTag != null)
                {
                    currentAction.target = GameObject.FindGameObjectWithTag(currentAction.targetTag);
                }
                if (currentAction.target != null)
                {
                    currentAction.beingPerformed = true;
                    currentAction.agent.SetDestination(currentAction.target.transform.position);
                }
            }
            else
            {
                actionQueue = null;
            }
        }
    }

    private void ActionPerformed()
    {
        currentAction.beingPerformed = false;
        currentAction.PostPerform();
        invoked = false;
    }

    public SubGoal FindGoalByName(string goalName)
    {
        foreach (KeyValuePair<SubGoal, int> g in goals)
        {
            if(g.Key.sGoals.ContainsKey(goalName))
            {
                return g.Key;
            }
        }
        return null;
    }

    public void ChangeGoalPriority(SubGoal g, int newPriority)
    {
        if(goals.ContainsKey(g))
        {
            if (!defaultPriorities.ContainsKey(g))
            {
                defaultPriorities.Add(g, goals[g]);
            }
            goals[g] = newPriority;
        }
    }

    public void ResetPriority(string goalName)
    {
        foreach (KeyValuePair<SubGoal, int> g in defaultPriorities)
        {
            if (g.Key.sGoals.ContainsKey(goalName))
            {
                ChangeGoalPriority(g.Key, defaultPriorities[g.Key]);
            }
        }
        
    }
}
