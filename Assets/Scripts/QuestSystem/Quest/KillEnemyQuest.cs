using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyQuest : AQuest
{
    private int enemyType;
    private int killAmount;
    private int killedAmount;
    private string name;

    public KillEnemyQuest(int _enemyType, int _killAmount)
    {
        enemyType = _enemyType;
        killAmount = _killAmount;
    }

    public override string GetQuestString()
    {
        string enemyTxt = "";
        if (enemyType == 1) enemyTxt = "Dummy";

        return $"Kill {enemyTxt} enemies {killedAmount}/{killAmount} times.";
    }

    public override void Start()
    {
        base.Start();
        EventSystem<EntityEvent>.AddListener(EventType.ENTITY_DIED, EnemyKilled);
    }

    private void EnemyKilled(EntityEvent _entityEvent)
    {
        if (_entityEvent.entityID == enemyType)
        {
            killedAmount++;

            if (killedAmount >= killAmount)
            {
                state = QuestState.COMPLETED;
                CompleteQuest();
            }
            else UpdateQuest();
        }
    }

    public override void End()
    {
        EventSystem<EntityEvent>.RemoveListener(EventType.ENTITY_DIED, EnemyKilled);
    }
}
