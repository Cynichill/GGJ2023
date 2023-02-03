using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private int woodCount;
    private int stoneCount;

    private void Start()
    {
        
    }

    public void AddResource(string resourceType, int amount)
    {
        switch (resourceType)
        {
            case "wood":
                woodCount += amount;
                break;

            case "stone":
                stoneCount += amount;
                break;
        }

    }

    private void UpdateUI()
    {

    }

}
