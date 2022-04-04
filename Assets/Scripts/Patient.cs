using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : GAgent
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
       
        SubGoal s1 = new SubGoal("IsWaiting", 1, true);
        goals.Add(s1, 3);

        SubGoal s2 = new SubGoal("IsTreated", 1, true);
        goals.Add(s2, 6);

        SubGoal s3 = new SubGoal("IsHome", 1, true);
        goals.Add(s3, 7);

        SubGoal s4 = new SubGoal("Relieved", 1, false);
        goals.Add(s4, 7);

        //Invoke("GetNeedToRelieve", Random.Range(30f, 40f));
    }

    void GetNeedToRelieve()
    {
        agentBeliefs.ModifyState("isFull", 1);
        Invoke("GetNeedToRelieve", Random.Range(30f, 40f));
    }
}
