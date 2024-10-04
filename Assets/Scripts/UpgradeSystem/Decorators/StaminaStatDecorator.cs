using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IMPORTANT - THIS DECORATOR IS NOT USED, STAMINA STAT WAS USED IN TESTING
public class StaminaStatDecorator : StatDecorator
{
    private float change;

    public StaminaStatDecorator(IStatModifier _statModifier, float _change)
    {
        statModifier = _statModifier;
        change = _change;
    }
}
