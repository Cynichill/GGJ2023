using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color offsetColour, BaseColour;
    [SerializeField] private SpriteRenderer _rend;

    public void Init(bool isOffset)
    {
        _rend.color = isOffset ? offsetColour : BaseColour;
    }
}
