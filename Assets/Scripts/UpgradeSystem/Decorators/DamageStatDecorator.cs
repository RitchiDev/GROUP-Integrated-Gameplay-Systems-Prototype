using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStatDecorator : StatDecorator
{
    private float change;

    public DamageStatDecorator(IStatModifier _statModifier, float _change)
    {
        statModifier = _statModifier;
        change = _change;
    }

    public override float GetDamageMod()
    {
        return base.GetDamageMod() + change;
    }
}
