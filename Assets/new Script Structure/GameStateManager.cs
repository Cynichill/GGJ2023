using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    private enum GameState
    {
        Setup,
        Attack
    }

    private GameState gameState= GameState.Setup;

    public PlayerManager playerManager;

    public MapManager mapManager;

    private int wave = 0; //How many waves have passed
    private int maxWaves = 5; //Max number of waves
    private float timer = 0f;
    private float timePerWave = 60f; //in seconds


    private void Update()
    {
        playerManager.HandlePlayerActions();

        if (gameState == GameState.Attack)
        {
            UpdateAttackTimer();
        }
    }

    public void StartAttackPhase()
    {
        wave++;
        mapManager.InitiateWave(wave);
        gameState = GameState.Attack;
    }

    private void UpdateAttackTimer()
    {
        timer += Time.deltaTime;

        //Implement enemy tick here
        mapManager.WaveTick();

        if (timer > timePerWave)
        {
            mapManager.CloseWave(wave);


            if (wave == maxWaves)
            {
                DoVictoryCondition();
                return;
            }

            timer = 0f;
            gameState= GameState.Setup;

        }
    }

    public void DoVictoryCondition()
    {

    }
}
