using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : IGameBehaviour
{
    public event Action<IGameBehaviour> OnDispose;

    public void Awake()
    {

    }

    public void FixedUpdate()
    {

    }

    public void Start()
    {
        Debug.Log("Ik hou van kaas.");
    }

    public void Update()
    {
        Debug.Log("Hi Update");
    }
}
