using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class QuestFactory
{
    public static KeyCode[] Keys =
    {
        KeyCode.A,
        KeyCode.B,
        KeyCode.C,
        KeyCode.D,
        KeyCode.E,
        KeyCode.F,
        KeyCode.G,
        KeyCode.H,
        KeyCode.I,
        KeyCode.J,
        KeyCode.K,
        KeyCode.L,
        KeyCode.M,
        KeyCode.N,
        KeyCode.O,
        KeyCode.P,
        KeyCode.Q,
        KeyCode.R,
        KeyCode.S,
        KeyCode.T,
        KeyCode.U,
        KeyCode.V,
        KeyCode.W,
        KeyCode.X,
        KeyCode.Y,
        KeyCode.Z,
        KeyCode.Space,
        KeyCode.Return,
        KeyCode.Backspace,
        KeyCode.Escape,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow
    };

    public static QuestList CreateQuest(int _questLength)
    {
        List<IQuest> quests = new List<IQuest>();
        int xpReward = 0;

        QuestType[] questTypes = (QuestType[])System.Enum.GetValues(typeof(QuestType));
        for (int i = 0; i < _questLength; i++)
        {
            quests.Add(CreateQuest(questTypes[Random.Range(0, questTypes.Length)]));
            xpReward += Random.Range(5, 14);
        }

        return new QuestList(quests, new QuestDisplay(), null, xpReward, "Quest");
    }

    private static IQuest CreateQuest(QuestType _rndQuest)
    {
        switch (_rndQuest)
        {
            case QuestType.RANDOM_KEYCODE:
                return new KeyPressQuest(Keys[Random.Range(0, Keys.Length)], Random.Range(2, 12));
            case QuestType.RANDOM_MOUSE_BUTTON:
                return new MousePressQuest(Random.Range(0, 3), Random.Range(2, 12));
            case QuestType.RANDOM_ENEMY:
                return new KillEnemyQuest(1, Random.Range(0, 2));
            default:
                Debug.LogError("Quest Type not found.");
                return null;
        }
    }
}
