using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nurse : GAgent
{
    public GameObject AssignedPatient = null;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("PatientTreated", 1, false);
        goals.Add(s1, 3);

        SubGoal s2 = new SubGoal("Rested", 1, false);
        goals.Add(s2, 3);

        SubGoal s3 = new SubGoal("Relieved", 1, false);
        goals.Add(s3, 3);

        Invoke("GetExhausted", Random.Range(5f, 10f));
        Invoke("GetNeedToRelieve", Random.Range(30f, 35f));
    }
    
    void GetExhausted()
    {
        agentBeliefs.ModifyState("isTired", 1);
        float nextTime = Random.Range(5f, 10f) / agentBeliefs.GetStateValue("isTired");
        Invoke("GetExhausted", nextTime);
    }

    void GetNeedToRelieve()
    {
        agentBeliefs.ModifyState("isFull", 1);
        float nextTime = Random.Range(30f, 35f) / agentBeliefs.GetStateValue("isFull");
        Invoke("GetNeedToRelieve", nextTime);
    }
}

