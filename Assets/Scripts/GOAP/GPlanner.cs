using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Since Planner generally consists of graphs of action and performs algorithmic operations on that Action Graph to choose best set of actions
public class Node
{
    public Node parent;
    public float cost;
    public Dictionary<string, int> state;   //Though it is a dictionary, it is used as single state
    public GAction gAction;

    public Node(Node p, float c, Dictionary<string, int> allStates, GAction action)
    {
        parent = p;
        cost = c;
        state = new Dictionary<string, int>(allStates);
        gAction = action;
    }

    public Node(Node p, float c, Dictionary<string, int> allStates, Dictionary<string, int> agentBelieves, GAction action)
    {
        parent = p;
        cost = c;
        state = new Dictionary<string, int>(allStates);
        foreach (KeyValuePair<string, int> belief in agentBelieves)
        {
            state.Add(belief.Key, belief.Value);
        }
        gAction = action;
    }
}

public class GPlanner 
{
    //Will be used to populate Queue in Agents
    public Queue<GAction> plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates agentStates)
    {
        List<GAction> usableActions = new List<GAction>();
        foreach (GAction a in actions)
        {
            if (a.IsAchievable())
            {
                usableActions.Add(a);
            }
        }
    
        List<Node> Leaves = new List<Node>();

        Node start = new Node(null, 0, GWorld.Instance.GetWorld().GetStates(), agentStates.GetStates(), null);

        bool success = BuildGraph(start, Leaves, usableActions, goal);

        if (!success)
        {
            Debug.LogWarning("No Plan Found");
            return null;
        }

        Node cheapest = null;
        foreach (Node leaf in Leaves)
        {
            if (cheapest == null)
                cheapest = leaf;
            else
            {
                if (leaf.cost < cheapest.cost)
                    cheapest = leaf;
            }
                
        }

        List<GAction> result = new List<GAction>();
        Node n = cheapest;

        while (n!= null)
        {
            if(n.gAction != null)
            {
                result.Insert(0, n.gAction);
            }
            n = n.parent;
        }

        Queue<GAction> queue = new Queue<GAction>();

        string pln = "Plan: ";

        foreach (GAction a in result)
        {
            queue.Enqueue(a);
            pln += (" : " + a.actionName);
        }

        Debug.Log(pln);
        
        return queue;
    }

    private bool BuildGraph(Node parent, List<Node> leaves, List<GAction> usableActions, Dictionary<string, int> goal)
    {
        bool foundPath = false;
        foreach(GAction action in usableActions)
        {
            if (action.IsAchievableGiven(parent.state)) 
            {
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                foreach (KeyValuePair<string, int> effect in action.effect)
                {
                    if (!currentState.ContainsKey(effect.Key))
                    {
                        currentState.Add(effect.Key, effect.Value);
                    }
                }
                Node node = new Node(parent, parent.cost + action.cost, currentState, action);
                if(GoalAchieved(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    List<GAction> subset = ActionSubset(usableActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal);
                    if (found)
                        foundPath = true;
                }
            }
        }

        return foundPath;
    }

    private List<GAction> ActionSubset(List<GAction> actions, GAction action)
    {
        List<GAction> subset = new List<GAction>();
        foreach (GAction  a in actions)
        {
            if (!a.Equals(action))
                subset.Add(a);
        }
        return subset;
    }

    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
    {
        foreach(KeyValuePair<string, int> g in goal)
        {
            if (!state.ContainsKey(g.Key))
                return false;
        }
        return true;
    }


}
