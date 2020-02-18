using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerFaller : Faller
{
    

    public override void DoSpecialEffect(Heart h)
    {
        if (h.isShieldOpen)
        {
            Break();
        }
        else
        {
           h.Break();
        }
        
    }

    public override void Break()
    {
        base.Break();
    }


}
