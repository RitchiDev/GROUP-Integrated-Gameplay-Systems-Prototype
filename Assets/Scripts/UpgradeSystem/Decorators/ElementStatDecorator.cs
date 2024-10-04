using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementStatDecorator : StatDecorator
{
    private Elements newElement;

    public ElementStatDecorator(IStatModifier _statModifier, Elements _newElement)
    {
        statModifier = _statModifier;
        newElement = _newElement;
    }

    public override Elements GetElementMod()
    {
        return newElement;
    }
}
