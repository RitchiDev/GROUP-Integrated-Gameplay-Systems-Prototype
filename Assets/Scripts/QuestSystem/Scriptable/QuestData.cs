using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    RANDOM_KEYCODE = 0,
    RANDOM_MOUSE_BUTTON = 1,
    RANDOM_ENEMY = 2
}

[CreateAssetMenu(fileName = "QuestData", menuName = "Custom/Quest/QuestData")]
public class QuestData : ScriptableObject
{
    public QuestType questType;
    public int questValue;

    public int actionCount;

    public AQuest ConvertQuest()
    {
        switch (questType)
        {
            case QuestType.RANDOM_KEYCODE:
                return new KeyPressQuest(QuestFactory.Keys[questValue], actionCount);
            case QuestType.RANDOM_MOUSE_BUTTON:
                return new MousePressQuest(Random.Range(0, 3), actionCount);
            case QuestType.RANDOM_ENEMY:
                return new KillEnemyQuest(questValue, actionCount);
            default:
                Debug.LogError("Quest Type not found.");
                return null;
        }
    }
}
