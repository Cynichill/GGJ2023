using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveTimer : MonoBehaviour
{
    public float currentTime = 0f;
    private int textTime;
    private GameManager gm;

    private bool gameEnded = false;
    public TMPro.TMP_Text text;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("gm").GetComponent<GameManager>();
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            if (!gameEnded)
            {
                text.text = "Wave End!";
                gameEnded = true;
                //gm.EndGame();
            }
        }

        if (!gameEnded)
        {
            textTime = (int)currentTime;
            text.text = textTime.ToString();
        }

    }

}
