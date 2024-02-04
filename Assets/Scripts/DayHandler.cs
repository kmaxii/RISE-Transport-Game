using System.Collections.Generic;
using System.Linq;
using Interfaces;
using MaxisGeneralPurpose.Scriptable_objects;
using Missions;
using Scriptable_objects;
using UnityEngine;

public class DayHandler : MonoBehaviour
{
    [SerializeField] private Day day;


    [SerializeField] private PoiSpawner poiSpawner;

    [Header("Stats")] private TimeVariable _timeVariable;

    [SerializeField] private CurrentStats currentStats;

    private List<DayMission> _notYetActiveMissions;

    private readonly List<DayMission> _activeMissions = new();
    private readonly HashSet<DayMission> _completedMissions = new();
    private readonly HashSet<DayMission> _failedMissions = new();

    [SerializeField] private GameEventWithData missionGotten;
    [SerializeField] private GameEventWithData missionFailed;
    [SerializeField] private GameEventWithData missionCompleted;
    
    [SerializeField] private Persona persona;

    
    private static DayHandler _instance;
    public static DayHandler Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(_instance.gameObject);
        }
        else
        {
            _instance = this;
        }

        _timeVariable = currentStats.TimeVariable;
    }

    void Start()
    {
        _notYetActiveMissions = new List<DayMission>(day.DayMissions);


        CheckToStartMissions();
    }

    private void CheckToStartMissions()
    {
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
            //Activate Mission will remove the element, that is why we can use 0 here
            var mission = _notYetActiveMissions[0];

            if (!mission.HasShowUpTime || mission.ShowUpTime < _timeVariable.Time24H)
            {
                ActivateMission(mission);
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
        missionGotten.Raise(dayMission);
    }

    public void FinishMission(string missionName)
    {
        DayMission mission = _activeMissions.FirstOrDefault(mission => mission.Mission.MissionName == missionName);

        if (mission == null)
        {
            Debug.LogError($"Mission {missionName} not found");
            return;
        }

        FinishMission(mission);
    }
    public void FinishMission(DayMission dayMission)
    {
        dayMission.Mission.FinishMission(currentStats, persona);
        
        _activeMissions.Remove(dayMission);
        _completedMissions.Add(dayMission);
        poiSpawner.DestroyMission(dayMission.Mission);

        missionCompleted.Raise(dayMission);
        
        if (dayMission.HasChainedTask)
        {
            var nextMission = dayMission.ChildMission;

            _notYetActiveMissions.Add(nextMission);
            if (!nextMission.HasShowUpTime || nextMission.ShowUpTime < _timeVariable.Time24H)
            {
                ActivateMission(nextMission);
            }
        }
    }
    
    
    
    public void FailedMission(string missionName)
    {
        DayMission mission = _activeMissions.FirstOrDefault(mission => mission.Mission.MissionName == missionName);

        if (mission == null)
        {
            Debug.LogError($"Mission {missionName} not found");
            return;
        }

        FailedMission(mission);
    }
    public void FailedMission(DayMission dayMission)
    {
        dayMission.Mission.FailMission(currentStats, persona);
        
        _activeMissions.Remove(dayMission);
        _failedMissions.Add(dayMission);
        poiSpawner.DestroyMission(dayMission.Mission);

        missionFailed.Raise(dayMission);
        
        if (dayMission.HasChainedTask && dayMission.TriggerChainedOnFail)
        {
            var nextMission = dayMission.ChildMission;

            _notYetActiveMissions.Add(nextMission);
            if (!nextMission.HasShowUpTime || nextMission.ShowUpTime < _timeVariable.Time24H)
            {
                ActivateMission(nextMission);
            }
        }
    }
    


    private void OnEnable()
    {
        _timeVariable.raiseOnValueChanged.RegisterListener(OnEventRaised);
    }

    private void OnDisable()
    {
        _timeVariable.raiseOnValueChanged.UnregisterListener(OnEventRaised);
    }

    /**
     * On time change
     */
    public void OnEventRaised()
    {
        CheckToStartMissions();
    }
}