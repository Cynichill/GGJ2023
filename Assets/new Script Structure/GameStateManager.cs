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

    private GameState gameState = GameState.Attack;

    public PlayerManager playerManager;

    public MapManager mapManager;

    private int wave = 0; //How many waves have passed
    private int maxWaves = 5; //Max number of waves
    private float timer = 60f;
    private float timePerWave = 60f; //in seconds

    //Timer variables

    private int textTime;
    [SerializeField] private TMPro.TMP_Text timerText;
    [SerializeField] private TMPro.TMP_Text maxWaveText;
    [SerializeField] private TMPro.TMP_Text curWaveText;
    

    private void Start()
    {
        maxWaveText.text = maxWaves.ToString();
        //SetUp();
    }

    private void Update()
    {
        playerManager.HandlePlayerActions();

        if (gameState == GameState.Attack)
        {
            UpdateAttackTimer();
        }
    }

    public void SetUp()
    {
        mapManager.GetWaveUtils();
    }

    public void StartAttackPhase()
    {
        wave++;
        curWaveText.text = wave.ToString();
        mapManager.InitiateWave(wave);
        gameState = GameState.Attack;
    }

    private void UpdateAttackTimer()
    {
        timer -= 1 * Time.deltaTime;

        //Implement enemy tick here
        //mapManager.WaveTick();

        textTime = (int)timer;
        timerText.text = textTime.ToString();

        if (timer <= 0)
        {
            timer = 0;
            //mapManager.CloseWave(wave);


            if (wave == maxWaves)
            {
                DoVictoryCondition();
                return;
            }

            timer = 0f;
            gameState = GameState.Setup;
        }

    }

    public void DoVictoryCondition()
    {

    }
}
