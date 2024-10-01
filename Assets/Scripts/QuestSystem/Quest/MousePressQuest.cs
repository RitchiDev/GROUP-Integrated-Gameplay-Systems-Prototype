using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePressQuest : AQuest
{
    private int mouseButton;
    private int clickCount;
    private int clickedCount;

    public MousePressQuest(int _mouseBtn, int _clickCount)
    {
        mouseButton = _mouseBtn;
        clickCount = _clickCount;
    }

    public override string GetQuestString()
    {
        string mouseBtnText = "";
        if (mouseButton == 0) mouseBtnText = "left";
        else if (mouseButton == 1) mouseBtnText = "right";
        else if (mouseButton == 2) mouseBtnText = "middle";

        return $"Click {mouseBtnText} mouse button {clickedCount}/{clickCount} times.";
    }

    public override void Start()
    {
        EventSystem<int>.AddListener(EventType.MOUSE_CLICKED, MouseClicked);
    }

    private void MouseClicked(int _mouseBtn)
    {
        if (_mouseBtn == mouseButton)
        {
            clickedCount++;

            if (clickedCount >= clickCount)
            {
                state = QuestState.COMPLETED;
                CompleteQuest();
            }
            else UpdateQuest();
        }
    }

    public override void End()
    {
        EventSystem<int>.RemoveListener(EventType.MOUSE_CLICKED, MouseClicked);
    }
}
