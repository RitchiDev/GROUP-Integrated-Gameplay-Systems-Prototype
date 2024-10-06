using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : IGameBehaviour
{
    public event Action<IGameBehaviour> OnDispose;

    public virtual void Awake() { }

    public virtual void Start() { }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }
    public void LateUpdate() { }
    protected void Dispose()
    {
        OnDispose?.Invoke(this);
        OnDispose = null;
        OnDestroy();
    }

    public virtual void OnDestroy() { }

}
