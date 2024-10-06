using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestState
{
    NOT_STARTED,
    STARTED,
    COMPLETED
}

public interface IQuest
{
    public QuestState States { get; }

    public event Action QuestUpdate;
    public event Action QuestCompleted;

    public void Start();
    public void Update();

    public string GetQuestString();
    public void End();
}
