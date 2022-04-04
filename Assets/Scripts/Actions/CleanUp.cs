using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUp : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.RemovePuddle(transform.position);

        if (target == null)
            return false;

        return true;
    }

    public override bool PostPerform()
    {
        Destroy(target);
        return true;
    }
}
