using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedStatDecorator : StatDecorator
{
    private float change; // in factor (i.e. 20% -> 0.2)

    public SpeedStatDecorator(IStatModifier _statModifier, float _change)
    {
        statModifier = _statModifier;
        change = _change;
    }

    public override float GetSpeedMod()
    {
        return base.GetSpeedMod() + change;
    }
}
