using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownStatDecorator : StatDecorator
{
    private float change;

    public CooldownStatDecorator(IStatModifier statModifier, float change)
    {
        this.statModifier = statModifier;
        this.change = change;
    }

    public override float GetCooldownMod()
    {
        return base.GetCooldownMod() - change;
    }
}
