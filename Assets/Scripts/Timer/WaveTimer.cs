using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveTimer : MonoBehaviour
{
    private int textTime;
    private bool waveEnded = false;
    public TMPro.TMP_Text text;
    private float waveTime = 60;

    private void Update()
    {
        waveTime -= 1 * Time.deltaTime;

        if (waveTime <= 0)
        {
            waveTime = 0;
            if (!waveEnded)
            {
                text.text = "Wave End!";
                waveEnded = true;
            }
        }

        if (!waveEnded)
        {
            textTime = (int)waveTime;
            text.text = textTime.ToString();
        }

    }

    public void StartWave()
    {
        waveEnded = false;
        waveTime = 60;
    }

}
