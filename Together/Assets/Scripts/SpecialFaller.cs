using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFaller : Faller
{

    public enum SpecialFallerType { shield = 1, extraBullet = 2, clearDanger = 3 }

    public SpecialFallerType type;


    public override void DoSpecialEffect(Heart h)
    {
        switch (type)
        {
            case SpecialFallerType.shield:
                h.OpenShield();
                break;
            case SpecialFallerType.extraBullet:
                h.AddBullets();
                break;
            case SpecialFallerType.clearDanger:
                h.ClearDanger();
                break;
            default:
                break;
        }
        Break();
    }

    public override void Break()
    {
        base.Break();
    }


}
