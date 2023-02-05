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

    private ExternalMapManager emm;
    private GameState gameState = GameState.Setup;

    public PlayerManager playerManager;

    public MapManager mapManager;

    private int wave = 1; //How many waves have passed
    private int maxWaves = 5; //Max number of waves
    private float timer = 5f;
    private float timePerWave = 5f; //in seconds

    //Timer variables

    private int textTime;
    [SerializeField] private TMPro.TMP_Text timerText;
    [SerializeField] private TMPro.TMP_Text maxWaveText;
    [SerializeField] private TMPro.TMP_Text curWaveText;


    private void Start()
    {
        emm = GameObject.FindGameObjectWithTag("emm").GetComponent<ExternalMapManager>();
        curWaveText.text = wave.ToString();
        maxWaveText.text = maxWaves.ToString();
        emm.FirstWave();
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
        timer = timePerWave;
        //mapManager.InitiateWave(wave);
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
            else
            {
                wave++;
                curWaveText.text = wave.ToString();
                emm.CloseWave();
            }

            gameState = GameState.Setup;
        }

    }

    public void DoVictoryCondition()
    {

    }
}
