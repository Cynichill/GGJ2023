using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int gridSizeX;
    [SerializeField] private int gridSizeY;
    [SerializeField] private Tile tilePrefab;
    private ExternalMapManager emm;
    private int goalCount;
    [SerializeField] private GameObject cam;

    string[] mapData = new string[]
{
            "yyyyyyyyyyyyyyyyyyyyyyyyyyyyyy", "yzzzzzzdzzzzzzzczzzzzzzzzzzzzy", "yzzzzzbzzzzzzzzzzzzzzzzzzzzzzy","yzzzzzzzzzzzzzzzzzzzzzzzzzzzzy","yzzzzzzzzzzwzzzzzzzzzazzzzzzzy","yzzzzzzzzzzzzzzazzzzxzzzzzzzzy","yzzzzzzzzzzzzzzzzzzzzzzzzzzzzy","yzzzzzzezzzzzzzzzzzzzzzzzzzzzy", "yzzzzzzzzzzzzzzwzzzzzzzzzzzzzy", "yzzzzzzzzzzzzzzzzzzzzzzzzzzzzy", "yzzzzzzzzzyyyyyyyyyyyyyyyyyyyy", "yzzzzzzbzzyyyyyyyyyyyyyyyyyyyy", "yzzzzzzzzzyyyyyyyyyyyyyyyyyyyy", "yzzzzzzzzzyyyyyyyyyyyyyyyyyyyy", "yzzzzzzwzzyyyyyyyyyyyyyyyyyyyy", "yzzzzvzzzzyyyyyyyyyyyyyyyyyyyy", "yzzzdzzzzzyyyyyyyyyyyyyyyyyyyy", "yzzzzazzzzyyyyyyyyyyyyyyyyyyyy", "yzzzzczzzzyyyyyyyyyyyyyyyyyyyy", "yzzzzzzzzzyyyyyyyyyyyyyyyyyyyy", "yzzzzzzzzzzzzzzzzzzzfzzzzzzzzy", "yzzzzwzzzzzzzzzzzzzezzzzzzzzzy", "yzzzzzzzzzzzbzzzzzzzzzzzzzzzzy", "yzzzzzzzzzzzzzzzzzzzzzzzzzzzzy", "yzzzzzzzazzzzzzzzzzzxzzzzzzzzy", "yzzzzzzczzzzzzzzzzzzzzzzzzzzzy", "yzzzzzzzzzzzzzzzzzzzzzzzzzzzzy", "yzzzzzzzbzzzzzzzzzzzfzzzzzzzzy", "yzzzzgzzzzzzzzzzazzzzzzzzwzzzy", "yyyyyyyyyyyyyyyyyyyyyyyyyyyyyy"
};


    // Start is called before the first frame update
    private void Awake()
    {
        emm = GameObject.FindGameObjectWithTag("emm").GetComponent<ExternalMapManager>();
        gridSizeX = mapData[0].ToCharArray().Length;
        gridSizeY = mapData.Length;
        GenerateGrid();
        emm.GetTiles();
    }

    private void GenerateGrid()
    {
        for (int y = 0; y < gridSizeY; y++)
        {
            char[] newRow = mapData[y].ToCharArray();

            for (int x = 0; x < gridSizeX; x++)
            {
                PlaceTile(newRow[x].ToString(), x, y);
            }
        }
    }

    private void PlaceTile(string tileType, int x, int y)
    {
        if(tileType == "x")
        {
            goalCount += 1;
        }
        //Spawn tile, set this manager as parent
        var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
        spawnedTile.transform.parent = this.transform;
        spawnedTile.name = $"Tile {x} {y}";
        if(tileType == "v")
        {
            spawnedTile.GrabCam(cam);
            emm.GetSpawn(spawnedTile.transform);
        }
        spawnedTile.SetTileType(tileType, goalCount);

        //Set alternating colours for tiles
        var isOdd = (x % 2 == 0 && y % 2 != 0 || y % 2 == 0 && x % 2 != 0);
        spawnedTile.Init(isOdd);
    }

}
