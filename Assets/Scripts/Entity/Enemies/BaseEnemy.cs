using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseEnemy : EntityBehaviour
{
    public BaseEnemy()
    {
        EventSystem<Vector2>.AddListener(EventType.MOUSE_CLICKED_POS, Clicked);
        Game.AddGameBehaviour(this);
    }

    public abstract void Clicked(Vector2 clickPos);
}
