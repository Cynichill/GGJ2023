using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExternalMapManager : MonoBehaviour
{
    private GameStateManager gst;
    [SerializeField] private Transform player;
    [SerializeField] private CameraMove camMove;
    [SerializeField] private PlayerManager playerMan;
    [SerializeField] private GameObject butt;
    private Transform spawnTile;
    private List<Tile> rootTiles = new List<Tile>();

    private void Start()
    {
        gst = GameObject.FindGameObjectWithTag("GM").GetComponent<GameStateManager>();
    }

    public void GetSpawn(Transform location)
    {
        spawnTile = location;
    }

    public void InitiateWave()
    {
        gst.StartAttackPhase();
        butt.SetActive(false);
        playerMan.EnableControls();

        foreach (Tile tile in rootTiles)
        {
            tile.RespawnRoot();
        }
    }

    public void CloseWave()
    {
        FirstWave();

        //Teleport player to spawn
        player.position = spawnTile.position;

        //Tele cam too
        camMove.TeleToPlayer();

    }

    public void FirstWave()
    {
        //Destroy all roots
        GameObject[] roots = GameObject.FindGameObjectsWithTag("RootOrigin");

        foreach (GameObject root in roots)
        {
            Destroy(root);
        }

        //Enable UI for defence phase
        butt.SetActive(true);

        //Disable player control
        playerMan.DisableControls();
    }

    public void GetTiles()
    {
        GameObject[] rootOrigins = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in rootOrigins)
        {
            if (tile.GetComponent<Tile>().tileType == "w")
            {
                rootTiles.Add(tile.GetComponent<Tile>());
            }
        }
    }
}
