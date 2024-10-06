using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseEnemy : EntityBehaviour
{
    protected int deathXP;
    protected int enemyType;

    public BaseEnemy()
    {
        EventSystem<MouseClickEvent>.AddListener(EventType.MOUSE_CLICKED, Clicked);
        Game.AddGameBehaviour(this);
    }

    public abstract void Clicked(MouseClickEvent _clickPos);
    public virtual void TakeDamage(float _damage)
    {
          EventSystem<EntityEvent>.InvokeEvent(EventType.ENTITY_DAMAGE, new EntityEvent(enemyType,  _damage));
    }

    public virtual void Die()
    {
        EventSystem<EntityEvent>.InvokeEvent(EventType.ENTITY_DIED, new EntityEvent(enemyType, 0));
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
    public int entityID;
    public float damage;

    public EntityEvent(int _entityID, float _damage)
    {
        entityID = _entityID;
        damage = _damage;
    }
}