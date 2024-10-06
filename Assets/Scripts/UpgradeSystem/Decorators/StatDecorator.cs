using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatDecorator : IStatModifier
{
    protected IStatModifier statModifier;

    public virtual float GetDamageMod() { return statModifier.GetDamageMod(); }
    public virtual float GetHealthMod() { return statModifier.GetHealthMod(); }
    public virtual float GetSpeedMod() { return statModifier.GetSpeedMod(); }
    public virtual float GetCooldownMod() { return statModifier.GetCooldownMod(); }
    public virtual float GetExperienceMod() { return statModifier.GetExperienceMod(); }
    public virtual Elements GetElementMod() { return statModifier.GetElementMod(); }
}
