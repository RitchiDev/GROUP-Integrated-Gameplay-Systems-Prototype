using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : BaseEnemy
{
    public DummyEnemy(Vector2 position)
    {
        GameObject dummyObj = Resources.Load<GameObject>("Dummy");
        GameObject.Instantiate(dummyObj, position, Quaternion.identity);
    }

    public override void Clicked(Vector2 clickPos)
    {
        Debug.Log("Hi " + clickPos);
    }
}
