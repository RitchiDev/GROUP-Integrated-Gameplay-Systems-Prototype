using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedStatDecorator : StatDecorator
{
    private float change; // in factor (i.e. 20% -> 0.2)

    public SpeedStatDecorator(IStatModifier statModifier, float change)
    {
        this.statModifier = statModifier;
        this.change = change;
        Debug.Log("[Upgrade][Stats] Change: " + change);
    }

    public override float GetSpeedMod()
    {
        return base.GetSpeedMod() + change;
    }
}
