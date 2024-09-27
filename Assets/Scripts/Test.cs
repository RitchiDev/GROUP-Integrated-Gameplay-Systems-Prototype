using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : GameBehaviour
{
    float timer;

    public Test()
    {
        Debug.Log("Hi");
    }

    public override void Start()
    {
        base.Start();
        Debug.Log("Dit is de Start");
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("UPDATE UPDATE");

        timer += Time.deltaTime;
        if (timer >= 5)
        {
            Debug.Log("Dispose");
            Game.AddGameBehaviour(new Test2());
            Dispose();
        }
    }
}
