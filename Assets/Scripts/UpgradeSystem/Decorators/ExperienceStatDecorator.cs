using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceStatDecorator : StatDecorator
{
    private float change;

    public ExperienceStatDecorator(IStatModifier statModifier, float change)
    {
        this.statModifier = statModifier;
        this.change = change;
    }

    public override float GetExperienceMod()
    {
        return base.GetExperienceMod() + change;
    }
}
