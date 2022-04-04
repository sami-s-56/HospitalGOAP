using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationSpeed : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] float speedTime;

    public float SpeedTime
    {
        get { return speedTime; }
        set 
        { 
            if(value != speedTime) 
            { 
                speedTime = value;
                Time.timeScale = speedTime;
            } 
        }
    }
    

}
