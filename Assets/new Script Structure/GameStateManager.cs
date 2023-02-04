using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public PlayerManager playerManager;

    private int wave = 0; //How many waves have passed


    private void Update()
    {
        playerManager.HandlePlayerActions();
    }
}
