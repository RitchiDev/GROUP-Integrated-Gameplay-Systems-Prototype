using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class QuestList
{
    public QuestState QuestState { private set; get; }

    private List<IQuest> quests;
    private IQuest currentQuest;
    private int questIndex;

    private IQuestDisplay messenger;
    public QuestListData[] NextQuest { private set; get; }
    private string questName;
    private int xpOnComplete;

    public event Action<QuestList> QuestComplete;

    public QuestList(List<IQuest> _quest, IQuestDisplay _messenger, QuestListData[] _nextQuest, int _xpOnComplete, string _questName)
    {
        quests = _quest;
        messenger = _messenger;
        NextQuest = _nextQuest;
        xpOnComplete = _xpOnComplete;
        questName = _questName;
    }

    public void StartQuest()
    {
        QuestState = QuestState.STARTED;

        messenger?.Init(ShowQuestProgress());

        QuestCompleted();
    }

    private void QuestCompleted()
    {
        if (currentQuest != null)
        {
            messenger?.UpdateText(ShowQuestProgress());
            currentQuest.QuestCompleted -= QuestCompleted;
            currentQuest.QuestUpdate -= QuestUpdate;
            currentQuest.End();
            questIndex++;
        }

        if (questIndex >= quests.Count)
        {
            QuestState = QuestState.COMPLETED;
            messenger?.UpdateText(ShowQuestProgress());
            EventSystem<float>.InvokeEvent(EventType.EXP_GIVE, xpOnComplete);
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
            messenger?.UpdateText(ShowQuestProgress());
        }
    }

    private void QuestUpdate()
    {
        messenger?.UpdateText(ShowQuestProgress());
    }

    public void Update()
    {
        if (currentQuest != null) currentQuest.Update();
    }

    private string ShowQuestProgress()
    {
        string totalQuestDisplay = $"<b>{questName}:</b>\n";

        for (int i = 0; i < quests.Count; i++)
        {
            QuestState state = quests[i].States;

            if (state == QuestState.COMPLETED) totalQuestDisplay += "√ <s>";
            else if (state == QuestState.STARTED) totalQuestDisplay += "• ";
            else totalQuestDisplay += "□ ";

            totalQuestDisplay += quests[i].GetQuestString() + "\n";
            if (state == QuestState.COMPLETED) totalQuestDisplay += "</s>";
        }
        totalQuestDisplay += $"\nReward: {xpOnComplete} XP | {questIndex} / {quests.Count}";

        return totalQuestDisplay;
    }
}
