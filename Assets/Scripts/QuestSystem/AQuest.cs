using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AQuest : IQuest
{
    public QuestState States => state;
    protected QuestState state;

    public event Action QuestUpdate;
    public event Action QuestCompleted;

    protected void UpdateQuest() => QuestUpdate?.Invoke();
    protected void CompleteQuest() => QuestCompleted?.Invoke();


    public virtual void Start()
    {
        state = QuestState.STARTED;
    }

    public virtual void Update()
    {

    }

    public virtual void End()
    {
        
    }

    public abstract string GetQuestString();
}
