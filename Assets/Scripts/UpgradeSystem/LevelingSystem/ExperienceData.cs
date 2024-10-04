using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceData
{
    public IStatHolder target;
    public float currentExp;
    public float nextNeededExp;
    public float neededExp;

    public ExperienceData(IStatHolder _statholder, float _currentExp,float _nextNeededExp, float _neededExp)
    {
        target = _statholder;
        currentExp = _currentExp;
        nextNeededExp = _nextNeededExp;
        neededExp = _neededExp;
    }
}
