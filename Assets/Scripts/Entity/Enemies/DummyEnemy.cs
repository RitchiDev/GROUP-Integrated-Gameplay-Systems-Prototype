using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : BaseEnemy
{
    private GameObject enemyObj;

    private float health;

    public DummyEnemy(Vector2 _position, float _health = 5)
    {
        GameObject dummyObj = Resources.Load<GameObject>("Dummy");
        enemyObj = GameObject.Instantiate(dummyObj, _position, Quaternion.identity);
        health = _health;
    }

    public override void Clicked(MouseClickEvent _mouseData)
    {
        if (_mouseData.button == 0)
        {
            float enemySize = enemyObj.transform.lossyScale.x;

            if (Vector2.Distance(_mouseData.worldMousePosition, enemyObj.transform.position) <= enemySize * .5f)
            {
                TakeDamage(1f);
            }
        }
    }
    public override void TakeDamage(float _damage)
    {
        health -= _damage;
        if (health <= 0) Die();
    }

    public override void Die()
    {
        //$$ TODO: Give player XP
        Dispose();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        GameObject.Destroy(enemyObj);
    }

}
