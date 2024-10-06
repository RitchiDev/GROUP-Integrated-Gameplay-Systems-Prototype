using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownStatDecorator : StatDecorator
{
    private float change;

    public CooldownStatDecorator(IStatModifier _statModifier, float _change)
    {
        statModifier = _statModifier;
        change = _change;
    }

    public override float GetCooldownMod()
    {
        return base.GetCooldownMod() - change;
    }
}
