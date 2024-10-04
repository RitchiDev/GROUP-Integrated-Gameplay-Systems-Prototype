using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStatDecorator : StatDecorator
{
    private float change;

    public HealthStatDecorator(IStatModifier _statModifier, float _change)
    {
        statModifier = _statModifier;
        change = _change;
    }

    public override float GetHealthMod()
    {
        return base.GetHealthMod() + change;
    }
}
