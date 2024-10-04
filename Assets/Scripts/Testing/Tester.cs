using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : GameBehaviour
{
    private Camera cam;

    public override void Start()
    {
        cam = Camera.main;
    }

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
            // EventSystem<Vector2>.InvokeEvent(EventType.MOUSE_CLICKED_POS, Input.mousePosition);
            EventSystem<MouseClickEvent>.InvokeEvent(EventType.MOUSE_CLICKED, new MouseClickEvent(0, Input.mousePosition, cam.ScreenToWorldPoint(Input.mousePosition)));
        }
        if (Input.GetMouseButtonDown(1)) EventSystem<MouseClickEvent>.InvokeEvent(EventType.MOUSE_CLICKED, new MouseClickEvent(1, Input.mousePosition, cam.ScreenToWorldPoint(Input.mousePosition)));
        if (Input.GetMouseButtonDown(2)) EventSystem<MouseClickEvent>.InvokeEvent(EventType.MOUSE_CLICKED, new MouseClickEvent(2, Input.mousePosition, cam.ScreenToWorldPoint(Input.mousePosition)));

        if (Input.GetKeyDown(KeyCode.L)) new DummyEnemy(new Vector2(Random.Range(-9f, 9f), Random.Range(-4f, 4f)));
    }
}

public struct MouseClickEvent
{
    public int button;
    public Vector2 mousePosition;
    public Vector2 worldMousePosition;

    public MouseClickEvent(int _button, Vector2 _mousePosition, Vector2 _worldMousePosition)
    {
        button = _button;
        mousePosition = _mousePosition;
        worldMousePosition = _worldMousePosition;
    }
}