using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILeveling
{
    public float GetCurrentExp();
    public int GetCurrentLevel();
    public void AddExperience(float _experience);
    public void RemoveExperience(float _experience);
    public void LevelUp(int _level);
}
