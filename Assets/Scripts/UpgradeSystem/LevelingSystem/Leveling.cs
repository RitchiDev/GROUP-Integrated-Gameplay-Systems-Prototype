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

        EventSystem<float>.AddListener(EventType.EXP_GIVE, AddExperience);
    }

    public void RemoveListeners()
    {
        EventSystem<float>.RemoveListener(EventType.EXP_GIVE, AddExperience);
    }

    public float GetCurrentExp()
    {
        return currentExp;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    private int GetTotalExperienceNeeded(int _n)
    {
        int value = 0;
        for (int i = 0; i <= _n - 1; i++)
        {
            value = value + 50 + 25 * i;
        }
        return value;
    }

    public void AddExperience(float _experience)
    {
        currentExp += _experience * owner.GetStats().GetExperienceBoost();  // het totale hoeveelheid exp

        // get newlevel by looping
        //int newLevel = currentLevel;
        //while(GetExperienceNeeded(newLevel) < currentExp)
        //{
        //    newLevel++;
        //}

        //if(GetExperienceNeeded(newLevel) > currentExp)
        //{
        //    newLevel--;
        //}

        // or single function - Credits to my brother
        int newLevel = Mathf.FloorToInt((-37.5f + Mathf.Sqrt(1406.25f + (50f * currentExp))) / 25);

        EventSystem<ExperienceData>.InvokeEvent(EventType.EXP_GAINED, new ExperienceData(owner, currentExp, GetTotalExperienceNeeded(newLevel + 1), GetTotalExperienceNeeded(newLevel)));

        if (currentLevel < newLevel)
        {
            LevelUp(newLevel);
        }
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

        currentLevel = _newLevel;
    }
}
