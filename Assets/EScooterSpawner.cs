using System;
using System.Collections.Generic;
using MaxisGeneralPurpose.Scriptable_objects;
using UnityEngine;
using UnityEngine.Pool;

public class EScooterSpawner : MonoBehaviour
{
    [SerializeField] private EScooterData eScooterData;
    [SerializeField] private GameEvent playerMovedEvent;
    [SerializeField] private GameObject eScooterPrefab;
    [SerializeField] private Transform player;

    private Dictionary<Vector2Int, List<GameObject>> _spawnedEScootersInGrids;

    private ObjectPool<GameObject> _eScooterPool;


    [Header("Spawn Settings")]
    [Tooltip("The amount of 32x32 grids to spawn eScooters in. 0 = only spawn in the current grid.")]
    [SerializeField]
    private int spawnDistance = 0;

    [Tooltip("The maximum amount of eScooters to spawn in a 32x32 grid.")] [SerializeField]
    private int maxSpawnPerGrid = 30;

    //SpawnChance
    [Tooltip("The chance for each scooter that it wants to spawn to actually spawn.")] [SerializeField] [Range(0, 1)]
    private float spawnChance = 1f;

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

        // Initialize the object pool
        _eScooterPool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                return Instantiate(eScooterPrefab); // Create new eScooter
            },
            actionOnGet: (eScooter) =>
            {
                eScooter.SetActive(true); // Activate eScooter when taken from the pool
            },
            actionOnRelease: (eScooter) =>
            {
                eScooter.SetActive(false); // Deactivate eScooter when returned to the pool
            },
            actionOnDestroy: (eScooter) =>
            {
                Destroy(eScooter); // Destroy eScooter when the pool is cleared
            },
            collectionCheck: false, // Set this to true only if you need to check for double-release etc.
            defaultCapacity: 20, // Initial pool size
            maxSize: 50 // Maximum number of objects that can be in the pool
        );
    }

    /**
     * Cached list so that not a new one needs to be created every time
     */
    private readonly List<Vector2Int> _gridsToRemove = new List<Vector2Int>();

    private void SpawnEScooters()
    {
        var playerPos = player.position;
        int playerXGrid = (int) Math.Floor(playerPos.x / 32);
        int playerYGrid = (int) Math.Floor(playerPos.z / 32);

        _gridsToRemove.Clear();

        foreach (var grid in _spawnedEScootersInGrids.Keys)
        {
            if (Mathf.Abs(grid.x - playerXGrid) > spawnDistance || Mathf.Abs(grid.y - playerYGrid) > spawnDistance)
            {
                _gridsToRemove.Add(grid);
            }
        }

        // Remove scooters in grids outside of spawn distance
        foreach (var grid in _gridsToRemove)
        {
            foreach (var scooter in _spawnedEScootersInGrids[grid])
            {
                _eScooterPool.Release(scooter);
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
                    SpawnEScootersInGrid(xGrid, yGrid, _spawnedEScootersInGrids[gridKey]);
                }
            }
        }
    }


    private void SpawnEScootersInGrid(int xGrid, int yGrid, List<GameObject> scootersInGrid)
    {
        List<EScooterData.GridInfo> gridData = eScooterData.GetData(xGrid, yGrid);

        scootersInGrid.Shuffle();

        for (var i = 0; i < Math.Min(gridData.Count, maxSpawnPerGrid); i++)
        {
            if (UnityEngine.Random.value > spawnChance) continue;
            var gridInfo = gridData[i];

            GameObject eScooter = _eScooterPool.Get();
            eScooter.transform.position = new Vector3((float) gridInfo.xPos, 0, (float) gridInfo.yPos);
            scootersInGrid.Add(eScooter);
        }
    }
}