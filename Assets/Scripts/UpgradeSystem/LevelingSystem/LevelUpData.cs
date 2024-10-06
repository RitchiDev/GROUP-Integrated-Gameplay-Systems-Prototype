using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpData
{
    public IStatHolder target;
    public int newLevel;
    public int currentLevel;

    public LevelUpData(IStatHolder _statHolder, int _newLevel, int _currentLevel)
    {
        target = _statHolder;
        newLevel = _newLevel;
        currentLevel = _currentLevel;
    }
}
