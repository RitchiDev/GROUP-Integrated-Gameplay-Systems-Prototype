using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newUpgrade", menuName = "Upgrades/emptyUpgrade")]
public class Upgrade : ScriptableObject
{
    [Header("identification")]
    public string upgradeName;

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

    // create and return a new statmodifier by wrapping decorators around the given statmodifier - Factory pattern(?)
    public IStatModifier CreateStatModifier(IStatModifier _baseStatModifier)
    {
        // apply the specified changes to all used stat mods
        IStatModifier newModifier = _baseStatModifier;
        newModifier = new DamageStatDecorator(newModifier, damageChange/100);
        newModifier = new HealthStatDecorator(newModifier, healthChange/100);
        newModifier = new SpeedStatDecorator(newModifier, speedChange/100);
        newModifier = new CooldownStatDecorator(newModifier, cooldownChange/100);
        newModifier = new ExperienceStatDecorator(newModifier, experienceBoostChange/100);
        if(elementChange != Elements.NONE) { newModifier = new ElementStatDecorator(newModifier, elementChange); }

        return newModifier;
    }
}
