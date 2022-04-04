using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : MonoBehaviour
{
    public string actionName = "Action";
    public float cost = 1.0f;
    public GameObject target;
    public string targetTag;
    public float duration = 0;
    public WorldState[] preConditions;
    public WorldState[] afterEffects;
    public NavMeshAgent agent;

    public Dictionary<string, int> precondition;    //Selected Precondition
    public Dictionary<string, int> effect;  //Selected Result

    public bool beingPerformed = false;

    public GInventory inventory;

    public GAction()
    {
        precondition = new Dictionary<string, int>();
        effect = new Dictionary<string, int>();
    }

    public void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        if(preConditions != null)
        {
            foreach (WorldState w in preConditions)
            {
                precondition.Add(w.key, w.value);
            }
        }
        if (afterEffects != null)
        {
            foreach (WorldState w in afterEffects)
            {
                effect.Add(w.key, w.value);
            }
        }

        inventory = GetComponent<GAgent>().inventory;
    }

    public bool IsAchievable()
    {
        return true;
    }

    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        foreach (KeyValuePair<string,int> pair in precondition)
        {
            if (!conditions.ContainsKey(pair.Key))
                return false;
        }
        return true;
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
