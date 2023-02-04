using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapMusic : MonoBehaviour
{

    public string prevMusic;
    public string nextMusic;
    public bool activated = false;
    public bool swap;

    //Swaps music to other track provided
    void OnTriggerEnter2D()
    {
        if (!activated)
        {
            FindObjectOfType<AudioManager>().Stop(prevMusic);

            if (swap)
            {
                FindObjectOfType<AudioManager>().Play(nextMusic);
            }

            activated = true;
        }
    }
}
