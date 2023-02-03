using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Vector2 maxPosition;
    public Vector2 minPosition;

    public void EndGame()
    {
        Restart();
    }

    private void Restart()
    {
        //Reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
