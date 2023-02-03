using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int gridSizeX = 30;
    [SerializeField] private int gridSizeY = 30;
    [SerializeField] private Tile tilePrefab;


    // Start is called before the first frame update
    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {

                //Spawn tile, set this manager as parent
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                spawnedTile.transform.parent = this.transform;
                spawnedTile.name = $"Tile {x} {y}";

                //Set alternating colours for tiles
                var isOdd = (x % 2 == 0 && y % 2 != 0 || y % 2 == 0 && x % 2 != 0);
                spawnedTile.Init(isOdd);
            }
        }
    }

}
