using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Elements { NONE, FIRE, WATER, GRASS, LIGHTNING }

public class Stats
{
    private IStatModifier statModifier;

    private float damage;
    private float health;
    private float speed;
    private float cooldown;
    private Elements element;

    public Stats(float damage, float health, float speed, float cooldown , Elements element)
    {
        EventSystem<Upgrade>.AddListener(EventType.UPGRADE_AQCUIRED, ApplyModifier);

        // create stat modifiers without any decorators (with default values)
        statModifier = new StatModifier();

        // set base values which can differ between stat bearers
        this.damage = damage;
        this.health = health;
        this.speed = speed;
        this.cooldown = cooldown;
        this.element = element;
    }

    // call this when player is disabled/destroyed
    public void OnDisable()
    {
        EventSystem<Upgrade>.RemoveListener(EventType.UPGRADE_AQCUIRED, ApplyModifier);
    }

    public float GetDamage() { return damage * statModifier.GetDamageMod(); }

    public float GetHealth() { return health * statModifier.GetHealthMod(); }

    public float GetSpeed() { return speed * statModifier.GetSpeedMod(); }

    public float GetCooldown() { return cooldown * statModifier.GetCooldownMod(); }

    public float GetExperienceBoost() { return statModifier.GetExperienceMod(); }

    public Elements GetElement() { return statModifier.GetElementMod(); }

    private void ApplyModifier(Upgrade upgrade)
    {
        statModifier = upgrade.CreateStatModifier(statModifier);
        
        Debug.Log("[Stats] upgrade applied: " + upgrade.name);
        //Debug.Log("[Stats] " + GetSpeed());
    }
}
