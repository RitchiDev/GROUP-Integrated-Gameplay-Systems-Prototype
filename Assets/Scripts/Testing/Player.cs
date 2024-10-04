using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaceHolder
{
    public class Player : GameBehaviour, IStatHolder, ILevelHolder
    {
        private Stats stats;
        public ILeveling Leveling { get; }

        public Player()
        {
            stats = new Stats(10, 100, 6, 1, Elements.NONE);
            Leveling = new Leveling(this);
        }

        public override void Awake()
        {

        }

        public override void Start()
        {

        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.O)) { Leveling.AddExperience(50f); }
            if (Input.GetKeyDown(KeyCode.P)) { DebugModifiedStats(); }
        }

        public Stats GetStats()
        {
            return stats;
        }

        public ILeveling GetLeveling()
        {
            return Leveling;
        }

        private void DebugModifiedStats()
        {
            Debug.Log($"[Player stats] -------------------");
            Debug.Log($"[Player stats] Damage: {stats.GetDamage()}");
            Debug.Log($"[Player stats] Health: {stats.GetHealth()}");
            Debug.Log($"[Player stats] Speed: {stats.GetSpeed()}");
            Debug.Log($"[Player stats] Cooldown: {stats.GetCooldown()}");
            Debug.Log($"[Player stats] Experience Boost: {stats.GetExperienceBoost()}");
            Debug.Log($"[Player stats] Element: {stats.GetElement()}");
            Debug.Log($"[Player stats] -------------------");
        }
    }
}

