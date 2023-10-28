using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tileTypes;
    public int gridWidth = 8;
    public int gridHeight = 8;
    public GameObject[,] tiles;
    public GameObject treePrefab;
    public GameObject housePrefab;
    private IEnumerator PlantTrees()
    {
        float plantingInterval = 1.0f;
        bool noEmptyDirtTiles = true;
        while (noEmptyDirtTiles == true)
        {
            noEmptyDirtTiles = true; // Assume there are no empty "Dirt" tiles by default.
            
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    GameObject tile = tiles[x, y];
                    // Wait for the specified planting interval before the next planting.
                    yield return new WaitForSeconds(plantingInterval);

                    if (tile != null && tile.CompareTag("Dirt") && tile.transform.childCount == 0)
                    {
                        Instantiate(treePrefab, tile.transform.position, Quaternion.identity);
                        noEmptyDirtTiles = false; // There was an empty "Dirt" tile, so set this to false.
                    }
                }
            }
            // If there are no more empty "Dirt" tiles, break out of the loop.
            if (noEmptyDirtTiles == false)
            {
                Debug.Log("Tree is finished planting");
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                // Randomly select a tile type from tileTypes array
                int randomTileIndex = Random.Range(0, tileTypes.Length);
                GameObject tilePrefab = tileTypes[randomTileIndex];

                // Instantiate the selected tile at the grid position
                GameObject tile = Instantiate(tilePrefab, new Vector3(x, 0, z), Quaternion.identity);

                // Set the tile in the 2D array
                tiles[x, z] = tile;
            }
        }
        StartCoroutine(PlantTrees());
    }
}
