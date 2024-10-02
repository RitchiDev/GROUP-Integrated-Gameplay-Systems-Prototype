using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newUpgrade", menuName = "Upgrades/emptyUpgrade")]
public class Upgrade : ScriptableObject
{
    [Header("identification")]
    public string UpgradeName;

    [Header("Upgrade values (in percentages)")]
    [Range(-100,500)]
    public float damageChange;
    [Range(-100, 500)]
    public float healthChange;
    [Range(-100,500)]
    public float speedChange;
    [Range(-100, 100)]
    public float cooldownChange;
    [Range(-100, 500)]
    public float experienceBoostChange;
    public Elements elementChange;

    // create and return a new statmodifier by wrapping decorators around the given statmodifier
    public IStatModifier CreateStatModifier(IStatModifier baseStatModifier)
    {
        //Debug.Log("[Upgrade][Stats] base Speed: " + baseStatModifier.GetSpeedMod() + ", base Stamina: " + baseStatModifier.GetStaminaMod());

        // TODO - optimize this to list of all decorator types, then use for loop

        // apply the specified changes to all used stat mods
        IStatModifier newModifier = baseStatModifier;
        newModifier = new DamageStatDecorator(newModifier, damageChange/100);
        newModifier = new HealthStatDecorator(newModifier, healthChange/100);
        //Debug.Log("[Upgrade][Stats] speed Change: " + speedChange);
        newModifier = new SpeedStatDecorator(newModifier, speedChange/100);
        newModifier = new CooldownStatDecorator(newModifier, cooldownChange/100);
        newModifier = new ExperienceStatDecorator(newModifier, experienceBoostChange/100);
        newModifier = new ElementStatDecorator(newModifier, elementChange);

        //Debug.Log("[Upgrade][Stats] newSpeed: " + newModifier.GetSpeedMod() + ", newStamina: " + newModifier.GetStaminaMod());
        return newModifier;
    }
}
