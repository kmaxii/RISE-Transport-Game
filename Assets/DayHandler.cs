using System;
using System.Collections.Generic;
using Interfaces;
using Missions;
using UnityEngine;

public class DayHandler : MonoBehaviour, IEventListenerInterface
{
    [SerializeField] private Day day;
    [SerializeField] private TimeVariable timeVariable;

    [SerializeField] private PoiSpawner poiSpawner;

    private List<DayMission> _dayMissions;

    private readonly List<DayMission> _activeMissions = new List<DayMission>();
    private HashSet<DayMission> _completedMissions = new HashSet<DayMission>();


    // Start is called before the first frame update
    void Start()
    {
        _dayMissions = new List<DayMission>(day.DayMissions);

        _dayMissions.ForEach(mission =>
        {
            if (!mission.HasShowUpTime || mission.ShowUpTime < timeVariable.Time24H)
            {
                ActivateMission(mission);
            }
        });
    }

    private void ActivateMission(DayMission dayMission)
    {
        poiSpawner.SpawnMission(dayMission.Mission);
        _activeMissions.Add(dayMission);
    }

    private void FinishMission(DayMission dayMission)
    {
        _activeMissions.Remove(dayMission);
        _completedMissions.Add(dayMission);
        poiSpawner.DestroyMission(dayMission.Mission);
        
        if (dayMission.HasChainedTask)
        {
            var nextMission = dayMission.ChildMission;
            
            _activeMissions.Add(nextMission);
            if (!nextMission.HasShowUpTime || nextMission.ShowUpTime < timeVariable.Time24H)
            {
                ActivateMission(nextMission);
            }
        }
        
        
        if (_activeMissions.Count == 0)
        {
            Debug.Log("No more missions");
        }

        Debug.Log("Mission completed");
        Debug.Log("Active missions: " + _activeMissions.Count);
        Debug.Log("Completed missions: " + _completedMissions.Count);
        Debug.Log("Total missions: " + _dayMissions.Count);
        Debug.Log("Time: " + timeVariable.Time24H);
        Debug.Log("Next mission: " + _activeMissions[0].Mission.name);
    }


    // Update is called once per frame
    void Update()
    {
    }


    private void OnEnable()
    {
        timeVariable.raiseOnValueChanged.RegisterListener(this);
    }

    private void OnDisable()
    {
        timeVariable.raiseOnValueChanged.UnregisterListener(this);
    }

    /**
     * On time change
     */
    public void OnEventRaised()
    {
        throw new NotImplementedException();
    }
}