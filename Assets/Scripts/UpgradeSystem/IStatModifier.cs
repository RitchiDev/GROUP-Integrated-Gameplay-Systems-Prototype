using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatModifier
{
    float GetDamageMod();
    float GetHealthMod();
    float GetSpeedMod();
    float GetCooldownMod();
    float GetExperienceMod();
    Elements GetElementMod();
}
