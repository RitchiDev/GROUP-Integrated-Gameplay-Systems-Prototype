using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier : IStatModifier
{
    // return default modifier values on creation
    public float GetDamageMod() { return 1; }
    public float GetHealthMod() { return 1; }
    public float GetSpeedMod() { return 1; }
    public float GetCooldownMod() { return 1; }
    public float GetExperienceMod() { return 1; }
    public Elements GetElementMod() { return Elements.NONE; }
}
