using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList
{
    public QuestState QuestState { private set; get; }

    private List<IQuest> quests;
    private IQuest currentQuest;
    private int questIndex;
    private int xpOnComplete;

    private IQuestDisplay messenger;
    public QuestListData[] NextQuest { private set; get; }

    public event Action<QuestList> QuestComplete;

    public QuestList(List<IQuest> _quest, IQuestDisplay _messenger, QuestListData[] _nextQuest, int _xpOnComplete)
    {
        quests = _quest;
        messenger = _messenger;
        NextQuest = _nextQuest;
        xpOnComplete = _xpOnComplete;
    }

    public void StartQuest()
    {
        QuestState = QuestState.STARTED;

        string totalQuestDisplay = "<b>Quest:</b>\n";
        for (int i = 0; i < quests.Count; i++)
        {
            totalQuestDisplay += quests[i].GetQuestString() + "\n";
        }
        totalQuestDisplay += $"\n{xpOnComplete}";
        messenger?.Init(totalQuestDisplay);
        //messenger?.UpdateText(totalQuestDisplay);

        QuestCompleted();
    }

    private void QuestCompleted()
    {
        if (currentQuest != null)
        {
            messenger?.UpdateText(currentQuest.GetQuestString() + " < COMPLETED " + questIndex + "/" + quests.Count);
            currentQuest.QuestCompleted -= QuestCompleted;
            currentQuest.QuestUpdate -= QuestUpdate;
            currentQuest.End();
            questIndex++;
        }

        if (questIndex >= quests.Count)
        {
            QuestState = QuestState.COMPLETED;
            messenger?.UpdateText("Quest Completed: " + questIndex + "/" + quests.Count);
            //Waiting for xp event.
            QuestComplete?.Invoke(this);
            QuestComplete = null;
            messenger?.End();
        }
        else
        {
            currentQuest = quests[questIndex];
            currentQuest.QuestCompleted += QuestCompleted;
            currentQuest.QuestUpdate += QuestUpdate;
            currentQuest.Start();
            messenger?.UpdateText("Started: " + currentQuest.GetQuestString() + " " + questIndex + "/" + quests.Count);
        }
    }

    private void QuestUpdate()
    {
        messenger.UpdateText(currentQuest.GetQuestString());
    }

    public void Update()
    {
        if (currentQuest != null) currentQuest.Update();
    }
}
