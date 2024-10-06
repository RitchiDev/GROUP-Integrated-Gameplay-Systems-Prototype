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

    public Stats(float _damage, float _health, float _speed, float _cooldown , Elements _element)
    {
        EventSystem<Upgrade>.AddListener(EventType.UPGRADE_AQCUIRED, ApplyModifier);

        // create stat modifiers without any decorators (with default values)
        statModifier = new StatModifier();

        // set base values which can differ between stat holders
        damage = _damage;
        health = _health;
        speed = _speed;
        cooldown = _cooldown;
        element = _element;
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

    private void ApplyModifier(Upgrade _upgrade)
    {
        statModifier = _upgrade.CreateStatModifier(statModifier);
    }
}
