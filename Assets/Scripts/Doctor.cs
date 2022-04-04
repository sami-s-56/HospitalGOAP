using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor : GAgent
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        SubGoal s1 = new SubGoal("Research", 1, false);
        goals.Add(s1, 3);

        SubGoal s2 = new SubGoal("Rested", 1, false);
        goals.Add(s2, 5);

        SubGoal s3 = new SubGoal("Relieved", 1, false);
        goals.Add(s3, 1);

        Invoke("GetExhausted", Random.Range(10f, 20f));

        Invoke("GetNeedToRelieve", Random.Range(5f, 10f));
    }

    void GetExhausted()
    {
        agentBeliefs.ModifyState("isTired", 1);
        Invoke("GetExhausted", Random.Range(10f, 20f));
    }
    
    void GetNeedToRelieve()
    {
        agentBeliefs.ModifyState("isFull", 1);
        Invoke("GetNeedToRelieve", Random.Range(5f, 10f));
    }
}
