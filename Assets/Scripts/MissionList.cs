using System.Collections.Generic;
using MaxisGeneralPurpose.Event;
using MaxisGeneralPurpose.Scriptable_objects;
using Missions;
using UnityEngine;

public class MissionList : MonoBehaviour
{
    [SerializeField] private GameEventWithData missionGottenEvent;
    [SerializeField] private GameEventWithData missionFinishedEvent;
    [SerializeField] private GameEventWithData missionFailedEvent;

    [SerializeField] private UiMissionShowcase prefabMissionShowcase;

    private void OnEnable()
    {
        missionGottenEvent.RegisterListener(AddMission);
        missionFinishedEvent.RegisterListener(RemoveMission);
        missionFailedEvent.RegisterListener(RemoveMission);
    }

    private void OnDisable()
    {
        missionGottenEvent.UnregisterListener(AddMission);
        missionFinishedEvent.UnregisterListener(RemoveMission);
        missionFailedEvent.UnregisterListener(RemoveMission);
    }

    private readonly Dictionary<DayMission, UiMissionShowcase> spawnedElements = new();


    private void AddMission(DataCarrier dataCarrier)
    {
        //Cast dataCarrier to a Mission if it is fo that type
        if (!(dataCarrier is DayMission dayMission))
        {
            Debug.LogError("Invalid type of data sent with Add mission event. Was not a day mission");
            return;
        }

        UiMissionShowcase spawned = Instantiate(prefabMissionShowcase, transform);
        spawned.Show(dayMission);
        spawnedElements.Add(dayMission, spawned);
        
    }

    private void RemoveMission(DataCarrier dataCarrier)
    {
        //Cast dataCarrier to a Mission if it is fo that type
        if (!(dataCarrier is DayMission dayMission))
        {
            Debug.LogError("Invalid type of data sent with Add mission event. Was not a day mission");
            return;
        }
        spawnedElements[dayMission].gameObject.Destroy();
        spawnedElements.Remove(dayMission);
    }
}