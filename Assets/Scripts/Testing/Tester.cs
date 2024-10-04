using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : GameBehaviour
{ 
    //Testing script for simulating the game.
    public override void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    EventSystem<KeyCode>.InvokeEvent(EventType.KEY_PRESSED, key);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            EventSystem<Vector2>.InvokeEvent(EventType.MOUSE_CLICKED_POS, Input.mousePosition);
            EventSystem<int>.InvokeEvent(EventType.MOUSE_CLICKED, 0);
        }
        if (Input.GetMouseButtonDown(1)) EventSystem<int>.InvokeEvent(EventType.MOUSE_CLICKED, 1);
        if (Input.GetMouseButtonDown(2)) EventSystem<int>.InvokeEvent(EventType.MOUSE_CLICKED, 2);

        if(Input.GetKeyDown(KeyCode.L)) new DummyEnemy(new Vector2(8, 2));
    }
}
