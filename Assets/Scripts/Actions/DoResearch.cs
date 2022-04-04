using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoResearch : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.RemoveOffice();
        if (target == null)
            return false;

        GetComponent<GAgent>().inventory.AddItem(target);

        return true;
    }
    
    public override bool PostPerform()
    {
        GWorld.AddOffice(target);
        GetComponent<GAgent>().inventory.RemoveItem(target);
        return true;
    }
}
