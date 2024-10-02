using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeList : GameBehaviour
{
    public static UpgradeList Instance { get; private set; }

    private Upgrade[] upgrades;

    public override void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Dispose();
            return;
        }

        upgrades = Resources.LoadAll<Upgrade>("Upgrades");
        Debug.Log($"upgrades list: {upgrades.Length}");
    }

    public Upgrade GetUpgrade(string _name)
    {
        foreach (Upgrade upgrade in upgrades)
        {
            if(upgrade.name == _name) {  return upgrade; }
        }

        Debug.LogWarning($"Could not find upgrade with name: {_name} ");
        return null;
    }
}
