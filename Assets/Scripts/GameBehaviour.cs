using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If your class is a base class of GameBehaviour. The Game will automatically
/// </summary>
public class GameBehaviour : IGameBehaviour
{
    public event Action<IGameBehaviour> OnDispose;

    public virtual void Awake()
    {
    }

    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    protected void Dispose()
    {
        OnDispose?.Invoke(this);
        OnDispose = null;
    }
}
