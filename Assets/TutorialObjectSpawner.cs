using System.Collections.Generic;
using MaxisGeneralPurpose.Event;
using MaxisGeneralPurpose.Scriptable_objects;
using minimap;
using UnityEngine;

public class TutorialObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameEventWithData missionFinishedEvent;
    

    [Header("First Mission Spawn")]
    [SerializeField] private GameObject spawnFirstMission;
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private GameObject spawnFirstMission2;

    [Header("Second Mission")]
    [SerializeField] private TileManagerUI tileManagerUI;

    [SerializeField] private PoiSpawner poiSpawner;

    private int _finishedMissions = 0;
    private void OnEnable()
    {
        missionFinishedEvent.RegisterListener(OnMissionFinished);
    }

    private void OnDisable()
    {
        missionFinishedEvent.UnregisterListener(OnMissionFinished);
    }

    private void OnMissionFinished(DataCarrier _)
    {
        
        if (_finishedMissions == 0)
        {
            Instantiate(spawnFirstMission, position, Quaternion.Euler(rotation));
            Instantiate(spawnFirstMission2);
        }

        if (_finishedMissions == 1)
        {
            tileManagerUI.showBussStations = true;
            tileManagerUI.UpdateMap();
            poiSpawner.SpawnBussStopsOnMap();
        }
        _finishedMissions++;
    }


}
