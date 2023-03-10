using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color offsetColour, BaseColour;
    [SerializeField] private SpriteRenderer _rend;
    [SerializeField] private BoxCollider2D tileBox;
    [SerializeField] private GameObject drill;
    [SerializeField] private GameObject resourceNode;
    [SerializeField] private GameObject rootOrigin;
    [SerializeField] private GameObject drillUIPrefab;
    private GameObject cam;
    public string tileType;

    /*
    z - Default
    y - Edge tile
    x - Goal tile
    w - Root spawn node
    v - player spawn
    a - Ruby Node
    b - Diamond Node
    c - Sapphire Node
    d - Amethyst Node
    e - Pearl Node
    f - Gold Node
    g - Errol Node
    */
    private bool occupied = false;

    public void GrabCam(GameObject camRef)
    {
        cam = camRef;
    }

    public void SetTileType(string changeToType, int goalCount)
    {
        tileType = changeToType;

        switch (tileType)
        {
            case "z":
                {
                    //Default tile
                    tileBox.enabled = false;
                    break;
                }

            case "y":
                {
                    //Edge Tile
                    _rend.color = Color.black;
                    tileBox.enabled = true;
                    break;
                }
            case "x":
                {
                    //Goal Tile
                    _rend.color = Color.yellow;
                    tileBox.enabled = false;
                    occupied = true;

                    var spawnDrill = Instantiate(drill, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    spawnDrill.transform.parent = this.transform;
                    spawnDrill.GetComponent<DrillHealth>().SpawnUI(goalCount, drillUIPrefab);

                    break;
                }
            case "w":
                {
                    //Spawn Root
                    _rend.color = Color.blue;
                    tileBox.enabled = false;
                    occupied = true;

                    break;
                }

            case "v":
                {
                    //Spawn Root
                    _rend.color = Color.red;
                    tileBox.enabled = false;
                    occupied = true;
                    var player = GameObject.FindGameObjectWithTag("Player");

                    player.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                    cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
                    break;
                }
            case "a":
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
            case "b":
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
            case "c":
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
            case "d":
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
            case "e":
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
            case "f":
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
            case "g":
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
        }
    }

    public void RespawnRoot(List<GameObject> drills)
    {
        var spawnRoot = Instantiate(rootOrigin, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
        spawnRoot.GetComponent<RootOrigin>().Initialize(drills);
    }

    public void Init(bool isOffset)
    {
        if (tileType == "z")
        {
            _rend.color = isOffset ? offsetColour : BaseColour;
        }
    }
}
