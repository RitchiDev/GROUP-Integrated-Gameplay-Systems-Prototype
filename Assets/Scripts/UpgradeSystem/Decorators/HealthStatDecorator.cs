using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStatDecorator : StatDecorator
{
    private float change;

    public HealthStatDecorator(IStatModifier statModifier, float change)
    {
        this.statModifier = statModifier;
        this.change = change;
    }

    public override float GetHealthMod()
    {
        return base.GetHealthMod() + change;
    }
}
