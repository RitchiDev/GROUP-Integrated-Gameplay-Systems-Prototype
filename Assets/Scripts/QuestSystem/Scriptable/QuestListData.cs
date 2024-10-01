using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "QuestListData", menuName ="Custom/Quest/QuestListData")]
public class QuestListData : ScriptableObject
{
    public List<QuestData> questOrder;
    public QuestListData[] nextQuest;

    public QuestList CreateQuestList()
    {
        List<IQuest> quest = new List<IQuest>();

        for (int i = 0; i < questOrder.Count; i++)
        {
            quest.Add(questOrder[i].ConvertQuest());
        }

        return new QuestList(quest, new QuestDisplay(), nextQuest);
    }
}
