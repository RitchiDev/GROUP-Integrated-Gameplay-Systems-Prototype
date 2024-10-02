using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILeveling
{
    public float GetCurrentExp();
    public int GetCurrentLevel();
    public void AddExperience(float experience);
    public void RemoveExperience(float experience);
    public void LevelUp(int level);
}
