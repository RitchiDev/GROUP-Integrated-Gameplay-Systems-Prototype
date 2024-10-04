using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneIndexes : int
{
    MENU = 0,
    GAME = 1,
}

public class Game
{
    private static List<IGameBehaviour> gameBehaviours;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitGame()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex != (int)SceneIndexes.MENU)
        {
            gameBehaviours = new List<IGameBehaviour>();

            IEnumerable<Type> behaviours = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(GameBehaviour)));

            foreach (Type behaviour in behaviours)
            {
                IGameBehaviour gB = (GameBehaviour)Activator.CreateInstance(behaviour);

                gameBehaviours.Add(gB);
                gB.OnDispose += BehaviourDisposed;
            }

            //Awake before start.
            gameBehaviours.ForEach(gB => gB.Awake());

            //Start after awake.
            gameBehaviours.ForEach(gB => gB.Start());

            UpdateLoop();
            FixedUpdateLoop();
        }
        else
        {
            new Menu().SceneChange += InitGame;
        }
    }

    private static async void UpdateLoop()
    {
        while (Application.isPlaying)
        {
            await Task.Delay(Mathf.RoundToInt(Time.deltaTime * 1000));

            //Makes a copy of the list to ensure no edits will ocure during the loop.
            List<IGameBehaviour> copyBehaviour = gameBehaviours.ToList();
            copyBehaviour.ForEach(gB => gB.Update());
            copyBehaviour.ForEach(gB => gB.LateUpdate());
        }
    }

    private static async void FixedUpdateLoop()
    {
        int fixedDelay = Mathf.RoundToInt(Time.fixedDeltaTime * 1000);

        while (Application.isPlaying)
        {
            await Task.Delay(fixedDelay);

            //Makes a copy of the list to ensure no edits will ocure during the loop.
            List<IGameBehaviour> copyBehaviour = gameBehaviours.ToList();
            copyBehaviour.ForEach(gB => gB.FixedUpdate());
        }
    }

    private static void BehaviourDisposed(IGameBehaviour _gameBehaviour)
    {
        gameBehaviours.Remove(_gameBehaviour);
        _gameBehaviour.OnDispose -= BehaviourDisposed;
    }

    /// <summary>
    /// Add a new IGameBehaviour to the update loops.
    /// </summary>
    /// <param name="_gameBehaviour">The IGameBehaviour to add.</param>
    /// <param name="_runStartingFunc">Run the start functions (default true)</param>
    public static void AddGameBehaviour(IGameBehaviour _gameBehaviour, bool _runStartingFunc = true)
    {
        if (_runStartingFunc)
        {
            _gameBehaviour.Awake();
            _gameBehaviour.Start();
        }

        gameBehaviours.Add(_gameBehaviour);
        _gameBehaviour.OnDispose += BehaviourDisposed;
    }
}
