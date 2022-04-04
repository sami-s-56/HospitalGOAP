using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPatient : GAction
{
    GameObject cubicle;

    public override bool PrePerform()
    {
        cubicle = GWorld.RemoveCubicle();

        if (cubicle != null)
        {
            GameObject _p = GWorld.Instance.WaitingPatients.Dequeue();
            GWorld.Instance.GetWorld().ModifyState("PatientWaiting", -1);
            GetComponent<Nurse>().AssignedPatient = _p;
            if (_p != null)
            {
                return true;
            }
        }
        else
        {
            return false;
        }

        
        return true;
    }

    public override bool PostPerform()
    {
        GetComponent<Nurse>().AssignedPatient.GetComponent<GAgent>().inventory.AddItem(cubicle);
        GetComponent<GAgent>().inventory.AddItem(cubicle);
        print("GetPatient_Postperform");
        return true;
    }
}
