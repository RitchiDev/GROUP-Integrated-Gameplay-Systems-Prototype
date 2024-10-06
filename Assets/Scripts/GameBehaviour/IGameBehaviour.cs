using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameBehaviour 
{
    public event Action<IGameBehaviour> OnDispose;

    public void Awake();
    public void Start();
    public void Update();
    public void FixedUpdate();
    public void LateUpdate();
    public void OnDestroy();
}
