using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int gridSizeX;
    [SerializeField] private int gridSizeY;
    [SerializeField] private Tile tilePrefab;

    string[] mapData = new string[]
{
            "1111111111", "1000000001", "1000002001","1000000001","1020000001","1000000001","1000000001","1000000001","1000000001","1000000001","1000000001","1000000001","1111111111"
};


    // Start is called before the first frame update
    private void Start()
    {
        gridSizeX = mapData[0].ToCharArray().Length;
        gridSizeY = mapData.Length;
        GenerateGrid();
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
        int passType = int.Parse(tileType);
        //Spawn tile, set this manager as parent
        var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
        spawnedTile.transform.parent = this.transform;
        spawnedTile.name = $"Tile {x} {y}";

        spawnedTile.SetTileType(passType);

        //Set alternating colours for tiles
        var isOdd = (x % 2 == 0 && y % 2 != 0 || y % 2 == 0 && x % 2 != 0);
        spawnedTile.Init(isOdd);


    }

}
