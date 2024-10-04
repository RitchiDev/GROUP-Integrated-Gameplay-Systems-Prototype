using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leveling : ILeveling
{
    private IStatHolder owner;

    private float currentExp;
    private int currentLevel = 0;

    private float expNeeded;    // the amount of exp needed to level up each level (Could make this get higher, when level gets higher with animation curve)

    public Leveling(IStatHolder _owner, float _expNeeded = 50f)
    {
        owner = _owner;

        this.currentExp = 0;
        this.currentLevel = 0;

        expNeeded = _expNeeded;
    }

    public float GetCurrentExp()
    {
        return currentExp;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void AddExperience(float _experience)
    {
        currentExp += _experience * owner.GetStats().GetExperienceBoost();
        
        EventSystem<ExperienceData>.InvokeEvent(EventType.EXP_GAINED, new ExperienceData(owner, currentExp, expNeeded));

        int newLevel = (int)(currentExp / expNeeded);

        if (currentLevel < newLevel)
        {
            LevelUp(newLevel);
        }

        Debug.Log("[Leveling] Adding experience ---------------- ");
        Debug.Log("[Leveling] new Exp: " + currentExp);
        Debug.Log("[Leveling] added Exp: " + _experience * owner.GetStats().GetExperienceBoost());
        Debug.Log("[Leveling] ----------------------------------");
    }

    // remove exp (Can not go down a level)
    public void RemoveExperience(float _experience)
    {
        currentExp -= _experience;

        if(currentExp < 0) { currentExp = 0; }
    }

    public void LevelUp(int _newLevel)
    {
        EventSystem<LevelUpData>.InvokeEvent(EventType.EXP_LEVELUP , new LevelUpData(owner, _newLevel, currentLevel));
        Debug.Log("[Leveling] Leveling up! lv: " + _newLevel);

        currentLevel = _newLevel;
    }
}
