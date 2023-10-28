using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSpawner : MonoBehaviour
{
    public GameObject housePrefab;
    private Dictionary<GameObject, bool> spawnStatus = new Dictionary<GameObject, bool>();
    public ScoreManager scoreManager;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject tile = hit.transform.gameObject;
                if (tile.CompareTag("Dirt") || tile.CompareTag("Desert"))
                {
                    if (!IsTreeInTile(tile) && !IsHouseAlreadySpawned(tile))
                    {
                        SpawnHouse(tile);
                    }
                }
            }
        }
    }

    bool IsTreeInTile(GameObject tile)
    {
        for (int i = 0; i < tile.transform.childCount; i++)
        {
            if (tile.transform.GetChild(i).CompareTag("Tree"))
            {
                return true;
            }
        }
        return false;
    }

    bool IsHouseAlreadySpawned(GameObject tile)
    {
        if (spawnStatus.ContainsKey(tile))
        {
            return spawnStatus[tile];
        }
        return false;
    }

    void SpawnHouse(GameObject tile)
    {
        Instantiate(housePrefab, tile.transform.position, Quaternion.identity);
        spawnStatus[tile] = true;
        if (tile.CompareTag("Dirt"))
        {
            scoreManager.AddScore(10);
        }
        else if (tile.CompareTag("Desert"))
        {
            scoreManager.AddScore(2);
        }
    }
}
