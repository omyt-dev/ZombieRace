using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using ZombieRace;


public class Test: MonoBehaviour
{
    private GameSession session;

    [Inject]
    private void Initialize(GameStateMachine gameStateMachine, GameSession session)
    {
        this.session = session;
        gameStateMachine.StateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(EGameState state1, EGameState state2)
    {
        //if (state2 == GameState.Playing)
        //    Invoke("Finish", 1f);
    }

    private void Finish() => session.FinishLevel(true);

    private void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            FindAnyObjectByType<Health>().TakeDamage(10);
        }
    }

    private void Start()
    {
       
    }
}
