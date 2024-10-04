using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    KEY_PRESSED = 0,
    MOUSE_CLICKED = 1,

    // Leveling events
    EXP_GIVE = 2,
    EXP_GAINED = 3,
    EXP_LEVELUP = 4,

    // Upgrade events
    UPGRADE_AQCUIRED = 5,
}
public static class EventSystem
{

    public static Dictionary<EventType, Action> regEvents = new Dictionary<EventType, Action>();

    public static void AddListener(EventType _type, Action _action)
    {
        if (!regEvents.ContainsKey(_type))
        {
            regEvents.Add(_type, null);
        }
        regEvents[_type] += _action;
    }

    public static void RemoveListener(EventType _type, Action _action)
    {
        if (!regEvents.ContainsKey(_type)) return;
        regEvents[_type] -= _action;
    }

    public static void InvokeEvent(EventType _type)
    {
        if (regEvents.ContainsKey(_type))
        {
            regEvents[_type]?.Invoke();
        }
    }
}

public static class EventSystem<T>
{
    public static Dictionary<EventType, Action<T>> regEvents = new Dictionary<EventType, Action<T>>();

    public static void AddListener(EventType _type, Action<T> _action)
    {
        if (!regEvents.ContainsKey(_type))
        {
            regEvents.Add(_type, null);
        }
        regEvents[_type] += _action;
    }

    public static void RemoveListener(EventType _type, Action<T> _action)
    {
        if (!regEvents.ContainsKey(_type)) return;
        regEvents[_type] -= _action;
    }

    public static void InvokeEvent(EventType _type, T _value)
    {
        if (!regEvents.ContainsKey(_type)) return;

        regEvents[_type]?.Invoke(_value);
    }
}