using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseEnemy : EntityBehaviour
{
    public BaseEnemy()
    {
        EventSystem<MouseClickEvent>.AddListener(EventType.MOUSE_CLICKED, Clicked);
        Game.AddGameBehaviour(this);
    }

    public abstract void Clicked(MouseClickEvent _clickPos);
    public abstract void TakeDamage(float _damage);
    public abstract void Die();

    public override void OnDestroy()
    {
        base.OnDestroy();
        EventSystem<MouseClickEvent>.RemoveListener(EventType.MOUSE_CLICKED, Clicked);
    }
}
