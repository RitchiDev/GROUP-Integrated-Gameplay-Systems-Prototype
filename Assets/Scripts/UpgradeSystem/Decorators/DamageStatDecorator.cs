using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStatDecorator : StatDecorator
{
    private float change;

    public DamageStatDecorator(IStatModifier statModifier, float change)
    {
        this.statModifier = statModifier;
        this.change = change;
    }

    public override float GetDamageMod()
    {
        return base.GetDamageMod() + change;
    }
}
