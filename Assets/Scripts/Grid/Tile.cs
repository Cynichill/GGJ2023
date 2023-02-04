using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color offsetColour, BaseColour;
    [SerializeField] private SpriteRenderer _rend;
    [SerializeField] private BoxCollider2D tileBox;
    [SerializeField] private GameObject drill;
    [SerializeField] private GameObject resourceNode;
    public int tileType = 0;
    /*
    0 - Default
    1 - Edge tile
    2 - Goal tile
    3 - Ruby Node
    4 - Diamond Node
    5 - Sapphire Node
    6 - Amethyst Node
    7 - Pearl Node
    8- Gold Node
    9 - Errol Node
    10 - Root spawn node
    */
    private bool occupied = false;

    public void SetTileType(int changeToType)
    {
        tileType = changeToType;

        switch (tileType)
        {
            case 0:
                {
                    //Default tile
                    tileBox.enabled = false;
                    break;
                }

            case 1:
                {
                    //Edge Tile
                    _rend.color = Color.black;
                    tileBox.enabled = true;
                    break;
                }
            case 2:
                {
                    //Goal Tile
                    _rend.color = Color.yellow;
                    tileBox.enabled = false;
                    occupied = true;

                    var spawnDrill = Instantiate(drill, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    spawnDrill.transform.parent = this.transform;
                    break;
                }
            case 3:
                {
                    //Resource Node - Ruby
                    _rend.color = Color.grey;
                    tileBox.enabled = false;
                    occupied = true;

                    var spawnNode = Instantiate(resourceNode, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    spawnNode.GetComponent<ResourceNode>().ChangeType(0);
                    spawnNode.transform.parent = this.transform;
                    break;
                }
            case 4:
                {
                    //Resource Node - Diamond
                    _rend.color = Color.grey;
                    tileBox.enabled = false;
                    occupied = true;

                    var spawnNode = Instantiate(resourceNode, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    spawnNode.GetComponent<ResourceNode>().ChangeType(1);
                    spawnNode.transform.parent = this.transform;
                    break;
                }
            case 5:
                {
                    //Resource Node - Sapphire
                    _rend.color = Color.grey;
                    tileBox.enabled = false;
                    occupied = true;

                    var spawnNode = Instantiate(resourceNode, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    spawnNode.GetComponent<ResourceNode>().ChangeType(2);
                    spawnNode.transform.parent = this.transform;
                    break;
                }
            case 6:
                {
                    //Resource Node - Amethyst
                    _rend.color = Color.grey;
                    tileBox.enabled = false;
                    occupied = true;

                    var spawnNode = Instantiate(resourceNode, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    spawnNode.GetComponent<ResourceNode>().ChangeType(3);
                    spawnNode.transform.parent = this.transform;
                    break;
                }
            case 7:
                {
                    //Resource Node - Pearl
                    _rend.color = Color.grey;
                    tileBox.enabled = false;
                    occupied = true;

                    var spawnNode = Instantiate(resourceNode, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    spawnNode.GetComponent<ResourceNode>().ChangeType(4);
                    spawnNode.transform.parent = this.transform;
                    break;
                }
            case 8:
                {
                    //Resource Node - Gold
                    _rend.color = Color.grey;
                    tileBox.enabled = false;
                    occupied = true;

                    var spawnNode = Instantiate(resourceNode, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    spawnNode.GetComponent<ResourceNode>().ChangeType(5);
                    spawnNode.transform.parent = this.transform;
                    break;
                }
            case 9:
                {
                    //Resource Node - Errol
                    _rend.color = Color.grey;
                    tileBox.enabled = false;
                    occupied = true;

                    var spawnNode = Instantiate(resourceNode, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    spawnNode.GetComponent<ResourceNode>().ChangeType(6);
                    spawnNode.transform.parent = this.transform;
                    break;
                }
            case 10:
                {
                    //Spawn Root
                    _rend.color = Color.blue;
                    tileBox.enabled = false;
                    occupied = true;

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
