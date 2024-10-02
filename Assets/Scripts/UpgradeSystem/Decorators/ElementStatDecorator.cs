using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementStatDecorator : StatDecorator
{
    private Elements newElement;

    public ElementStatDecorator(IStatModifier statModifier, Elements newElement)
    {
        this.statModifier = statModifier;
        this.newElement = newElement;
    }

    public override Elements GetElementMod()
    {
        return newElement;
    }
}
