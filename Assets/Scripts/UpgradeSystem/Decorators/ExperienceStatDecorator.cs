using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceStatDecorator : StatDecorator
{
    private float change;

    public ExperienceStatDecorator(IStatModifier _statModifier, float _change)
    {
        statModifier = _statModifier;
        change = _change;
    }

    public override float GetExperienceMod()
    {
        return base.GetExperienceMod() + change;
    }
}
