using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : GameBehaviour
{
    private List<QuestList> questLists;

    public override void Start()
    {
        questLists = new List<QuestList>();

        QuestListData firstQuestList = Resources.Load<QuestListData>("Quest/FirstQuest");

        if (firstQuestList == null) GetNewQuest();
        else SetNewQuest(firstQuestList.CreateQuestList());
    }

    public override void Update()
    {
        foreach (QuestList quest in questLists.Where(q => q.QuestState == QuestState.STARTED))
        {
            quest.Update();
        }
    }

    private QuestList GetNewQuest()
    {
        int questLength = Mathf.Max(Random.Range(1, 6) - 1, 1);

        return QuestFactory.CreateQuest(questLength);
    }

    //Adds a quest to the quest list (and starts it). This could be from the factory or from some sort of quest giver.
    public void SetNewQuest(QuestList _questList)
    {
        questLists.Add(_questList);
        _questList.StartQuest();
        _questList.QuestComplete += QuestComplete;
    }

    private void QuestComplete(QuestList _questList)
    {
        _questList.QuestComplete -= QuestComplete;
        questLists.Remove(_questList);

        if(_questList.NextQuest != null && _questList.NextQuest.Length > 0)
        {
            for (int i = 0; i < _questList.NextQuest.Length; i++)
            {
                SetNewQuest(_questList.NextQuest[i].CreateQuestList());
            }
        }
        else SetNewQuest(GetNewQuest());
    }
}
