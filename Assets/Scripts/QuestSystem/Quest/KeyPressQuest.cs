using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPressQuest : AQuest
{
    private KeyCode keyToPress;
    private int keyPressCount;
    private int pressedCount;

    public KeyPressQuest(KeyCode _keyToPress, int _keyPressCount)
    {
        keyToPress = _keyToPress;
        keyPressCount = _keyPressCount;
    }

    public override string GetQuestString()
    {
        return $"Press {keyToPress} {pressedCount}/{keyPressCount}."; 
    }

    public override void Start()
    {
        base.Start();
        EventSystem<KeyCode>.AddListener(EventType.KEY_PRESSED, KeyPressed);
    }

    private void KeyPressed(KeyCode _keyCode)
    {
        if(keyToPress == _keyCode)
        {
            pressedCount++;

            if (pressedCount >= keyPressCount)
            {
                state = QuestState.COMPLETED;
                CompleteQuest();
            }
            else UpdateQuest();
        }
    }

    public override void End()
    {
        EventSystem<KeyCode>.RemoveListener(EventType.KEY_PRESSED, KeyPressed);
    }
}
