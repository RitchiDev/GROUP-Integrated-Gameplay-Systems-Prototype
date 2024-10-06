using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DummyEnemy : BaseEnemy
{
    private GameObject enemyObj;

    private float health;

    public DummyEnemy(Vector2 _position, float _health = 3)
    {
        GameObject dummyObj = Resources.Load<GameObject>("Dummy");
        enemyObj = GameObject.Instantiate(dummyObj, _position, Quaternion.identity);
        health = _health;
        deathXP = 10;
    }

    public override void Clicked(MouseClickEvent _mouseData)
    {
        if (_mouseData.button == 0)
        {
            float enemySize = 1.5f;

            if (Vector2.Distance(_mouseData.worldMousePosition, enemyObj.transform.position) <= enemySize * .5f)
            {
                TakeDamage(1f);
            }
        }
    }
    public override void TakeDamage(float _damage)
    {
        base.TakeDamage(_damage);
        health -= _damage;
        if (health <= 0) Die();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        GameObject.Destroy(enemyObj);
    }
}
