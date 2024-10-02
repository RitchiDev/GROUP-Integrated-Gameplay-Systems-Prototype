using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leveling : ILeveling
{
    private IStatHolder owner;

    private float currentExp;
    private int currentLevel = 0;

    private float expNeeded;    // the amount of exp needed to level up each level (Could make this get higher, when level gets higher)

    public Leveling(IStatHolder owner, float expNeeded = 50f)
    {
        this.owner = owner;

        this.currentExp = 0;
        this.currentLevel = 0;

        this.expNeeded = expNeeded;
    }

    public float GetCurrentExp()
    {
        return currentExp;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void AddExperience(float experience)
    {
        currentExp += experience * owner.GetStats().GetExperienceBoost();
        
        EventSystem<ExperienceData>.InvokeEvent(EventType.EXP_GAINED, new ExperienceData(owner, currentExp, expNeeded));

        int newLevel = (int)(currentExp / expNeeded);

        if (currentLevel < newLevel)
        {
            LevelUp(newLevel);
        }

        Debug.Log("[Leveling] Adding experience ---------------- ");
        Debug.Log("[Leveling] new Exp: " + currentExp);
        Debug.Log("[Leveling] added Exp: " + experience * owner.GetStats().GetExperienceBoost());
        Debug.Log("[Leveling] ----------------------------------");
    }

    // remove exp (Can not go down a level)
    public void RemoveExperience(float experience)
    {
        currentExp -= experience;

        if(currentExp < 0) { currentExp = 0; }
    }

    public void LevelUp(int newLevel)
    {
        EventSystem<LevelUpData>.InvokeEvent(EventType.EXP_LEVELUP , new LevelUpData(owner, newLevel, currentLevel));
        Debug.Log("[Leveling] Leveling up! lv: " + newLevel);

        currentLevel = newLevel;
    }
}
