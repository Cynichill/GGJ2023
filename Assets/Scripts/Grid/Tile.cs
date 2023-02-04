using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color offsetColour, BaseColour;
    [SerializeField] private SpriteRenderer _rend;
    [SerializeField] private BoxCollider2D tileBox;
    [SerializeField] private GameObject drill;
    public int tileType = 0; //0 - Default, 1 - Edge tile, 2 - Goal tile
    private bool occupied = false;

    public void SetTileType(int changeToType)
    {
        tileType = changeToType;

        switch (tileType)
        {
            case 0:
                {
                    tileBox.enabled = false;
                    break;
                }

            case 1:
                {
                    _rend.color = Color.black;
                    tileBox.enabled = true;
                    break;
                }
            case 2:
                {
                    _rend.color = Color.yellow;
                    tileBox.enabled = false;
                    occupied = true;
                    
                    var spawnDrill = Instantiate(drill, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    break;
                }
        }
    }

    public void Init(bool isOffset)
    {
        if (tileType == 0)
        {
            _rend.color = isOffset ? offsetColour : BaseColour;
        }
    }
}
