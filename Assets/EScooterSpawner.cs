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

    private Dictionary<int, GameObject> _spawnedEScooters;

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
        _spawnedEScooters = new();
    }

    private void SpawnEScooters()
    {
        var playerPos = player.position;
        int xGrid = (int) Math.Floor(playerPos.x / 32);
        int yGrid = (int) Math.Floor(playerPos.z / 32);

        List<EScooterData.GridInfo> gridData = eScooterData.GetData(xGrid, yGrid);
        
        foreach (var gridInfo in gridData)
        {
            if (!_spawnedEScooters.ContainsKey(gridInfo.entryId))
            {
                GameObject eScooter = Instantiate(eScooterPrefab);
                eScooter.transform.position = new Vector3((float) gridInfo.xPos, 0, (float) gridInfo.yPos);
                _spawnedEScooters.Add(gridInfo.entryId, eScooter);
            }
        }
    }
}