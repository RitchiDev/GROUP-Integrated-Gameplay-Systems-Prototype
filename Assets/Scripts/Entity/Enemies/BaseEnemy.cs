using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseEnemy : EntityBehaviour
{
    protected int deathXP;

    public BaseEnemy()
    {
        EventSystem<MouseClickEvent>.AddListener(EventType.MOUSE_CLICKED, Clicked);
        Game.AddGameBehaviour(this);
    }

    public abstract void Clicked(MouseClickEvent _clickPos);
    public virtual void TakeDamage(float _damage)
    {
          EventSystem<EntityEvent>.InvokeEvent(EventType.ENTITY_DAMAGE, new EntityEvent(this,  _damage));
    }

    public virtual void Die()
    {
        EventSystem<EntityEvent>.InvokeEvent(EventType.ENTITY_DIED, new EntityEvent(this, 0));
        EventSystem<float>.InvokeEvent(EventType.EXP_GIVE, deathXP);
        Dispose();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        EventSystem<MouseClickEvent>.RemoveListener(EventType.MOUSE_CLICKED, Clicked);
    }
}

public struct EntityEvent
{
    public BaseEnemy entity;
    public float damage;

    public EntityEvent(BaseEnemy _entity, float _damage)
    {
        entity = _entity;
        damage = _damage;
    }
}