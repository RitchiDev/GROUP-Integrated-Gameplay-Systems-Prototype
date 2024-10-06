using CameraSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : GameBehaviour
{
    public override void Start()
    {
        CameraArea[] areas = Resources.LoadAll<CameraArea>("Areas");

        foreach (CameraArea area in areas)
        {
            int random = Random.Range(2, 5);

            for (int i = 0; i < random; i++)
            {
                Vector2 randomPos = new Vector2(Random.Range(area.Minimum.x + 1, area.Maximum.x - 1), Random.Range(area.Minimum.y + 1, area.Maximum.y - 1));

                new DummyEnemy(randomPos);
            }
            
        }
    }
}
