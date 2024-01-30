using System;
using System.Collections.Generic;
using MaxisGeneralPurpose.Scriptable_objects;
using UnityEngine;

public class EScooterSpawner : MonoBehaviour
{
    [SerializeField] private EScooterData eScooterData;
    [SerializeField] private GameEvent playerMovedEvent;
    [SerializeField] private GameObject eScooterPrefab;
    [SerializeField] private Transform player;

    private Dictionary<Vector2Int, List<GameObject>> _spawnedEScootersInGrids;

    [Header("Spawn Settings")]
    [Tooltip("The amount of 32x32 grids to spawn eScooters in. 0 = only spawn in the current grid.")]
    [SerializeField]
    private int spawnDistance = 0;

    [Tooltip("The maximum amount of eScooters to spawn in a 32x32 grid.")] [SerializeField]
    private int maxSpawnPerGrid = 30;

    private void OnEnable()
    {
        playerMovedEvent.RegisterListener(SpawnEScooters);
    }

    private void OnDisable()
    {
        playerMovedEvent.UnregisterListener(SpawnEScooters);
    }

    private void Awake()
    {
        _spawnedEScootersInGrids = new Dictionary<Vector2Int, List<GameObject>>();
    }

    private void SpawnEScooters()
    {
        var playerPos = player.position;
        int playerXGrid = (int) Math.Floor(playerPos.x / 32);
        int playerYGrid = (int) Math.Floor(playerPos.z / 32);

        // Track grids to remove
        List<Vector2Int> gridsToRemove = new List<Vector2Int>();

        foreach (var grid in _spawnedEScootersInGrids.Keys)
        {
            if (Mathf.Abs(grid.x - playerXGrid) > spawnDistance || Mathf.Abs(grid.y - playerYGrid) > spawnDistance)
            {
                gridsToRemove.Add(grid);
            }
        }

        // Remove scooters in grids outside of spawn distance
        foreach (var grid in gridsToRemove)
        {
            foreach (var scooter in _spawnedEScootersInGrids[grid])
            {
                Destroy(scooter);
                Debug.Log($"Destroyed eScooter in grid {grid.x}, {grid.y}");
            }

            _spawnedEScootersInGrids.Remove(grid);
        }

        // Spawn new scooters
        for (int x = -spawnDistance; x <= spawnDistance; x++)
        {
            for (int y = -spawnDistance; y <= spawnDistance; y++)
            {
                int xGrid = playerXGrid + x;
                int yGrid = playerYGrid + y;
                Vector2Int gridKey = new Vector2Int(xGrid, yGrid);

                if (!_spawnedEScootersInGrids.ContainsKey(gridKey))
                {
                    _spawnedEScootersInGrids[gridKey] = new List<GameObject>();

                    Debug.Log($"Spawning eScooters in grid {xGrid}, {yGrid}");

                    SpawnEScootersInGrid(xGrid, yGrid, _spawnedEScootersInGrids[gridKey]);
                }
            }
        }
    }

    private void SpawnEScootersInGrid(int xGrid, int yGrid, List<GameObject> scootersInGrid)
    {
        List<EScooterData.GridInfo> gridData = eScooterData.GetData(xGrid, yGrid);

        for (var i = 0; i < Math.Min(gridData.Count, maxSpawnPerGrid); i++)
        {
            var gridInfo = gridData[i];
            if (scootersInGrid.Find(scooter => scooter.GetInstanceID() == gridInfo.entryId) == null)
            {
                GameObject eScooter = Instantiate(eScooterPrefab);
                eScooter.transform.position = new Vector3((float) gridInfo.xPos, 0, (float) gridInfo.yPos);
                scootersInGrid.Add(eScooter);
            }
        }
    }
}