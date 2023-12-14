using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Missions;
using UnityEngine;

public class DayHandler : MonoBehaviour, IEventListenerInterface
{
    [SerializeField] private Day day;
    [SerializeField] private TimeVariable timeVariable;

    [SerializeField] private PoiSpawner poiSpawner;

    private List<DayMission> _notYetActiveMissions;

    private readonly List<DayMission> _activeMissions = new List<DayMission>();
    private readonly HashSet<DayMission> _completedMissions = new HashSet<DayMission>();

    
    void Start()
    {
        _notYetActiveMissions = new List<DayMission>(day.DayMissions);
        
        //Order _dayMissions. All elements where mission.HasShowUpTime = false should will be first in an order that does not matter, 
        //After which all missions come that have a mission.ShowUpTime where the first mission is the one with the smallest < 
        //And the last one is the one with the biggest showUp time
        _notYetActiveMissions = _notYetActiveMissions
            .Where(mission => !mission.HasShowUpTime) // Select missions where HasShowUpTime is false
            .Concat(_notYetActiveMissions
                .Where(mission => mission.HasShowUpTime) // Select missions where HasShowUpTime is true
                .OrderBy(mission => mission.ShowUpTime)) // Order them by ShowUpTime
            .ToList();
        
        for (int i = 0; i < _notYetActiveMissions.Count; i++)
        {
            var mission = _notYetActiveMissions[i];
            if (!mission.HasShowUpTime || mission.ShowUpTime < timeVariable.Time24H)
            {
                ActivateMission(mission);
                i--;
                continue;
            }
            
            //Because the list is ordered, once we encounter a mission that we should not yet spawn we can stop checking future missions
            break;
        }
    
        
    }

    private void ActivateMission(DayMission dayMission)
    {
        _notYetActiveMissions.Remove(dayMission);
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
        Debug.Log("Total missions: " + _notYetActiveMissions.Count);
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