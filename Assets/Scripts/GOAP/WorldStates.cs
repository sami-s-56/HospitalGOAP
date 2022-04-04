using System;
using System.Collections.Generic;
using UnityEngine;

/** This is to easily add states into World States list which basically is a dictionary of these two variables */
[System.Serializable]
public class WorldState
{
    public string key;
    public int value;
}

/** Container for all the world states (Basically a dictionary but with few methods for convinience) */
public class WorldStates
{
    public Dictionary<string, int> states;

    public WorldStates()
    {
        states = new Dictionary<string, int>();
    }

    public bool HasState(string key)
    {
        return states.ContainsKey(key);
    }

    public void AddState(string key, int val)
    {
        states.Add(key, val);
    }

    public void ModifyState(string key, int val)
    {
        if (states.ContainsKey(key))
        {
            states[key] += val;
            if (states[key] <= 0)
                RemoveState(key);
        }
        else
        {
            states.Add(key, val);
        }
    }

    public void RemoveState(string key)
    {
        if (states.ContainsKey(key))
        {
            states.Remove(key);
        }
    }

    public void SetState(string key, int val)
    {
        if (states.ContainsKey(key))
        {
            states[key] = val;
        }
        else
        {
            states.Add(key, val);
        }
    }

    public int GetStateValue(string stateName)
    {
        if (states.ContainsKey(stateName))
        {
            return states[stateName];
        }

        return 0;
    }

    public Dictionary<string, int> GetStates()
    {
        return states;
    }
}
