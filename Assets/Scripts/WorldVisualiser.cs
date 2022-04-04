using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldVisualiser : MonoBehaviour
{
    public Text states;

    // Update is called once per frame
    void LateUpdate()
    {
        Dictionary<string, int> WorldStates = GWorld.Instance.GetWorld().GetStates();

        states.text = "";

        foreach (KeyValuePair<string, int> pair in WorldStates)
        {
            print(pair.Key + " : " + pair.Value + "\n");
            states.text += pair.Key + " : " + pair.Value + "\n";
        }
    }
}
