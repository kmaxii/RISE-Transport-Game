using System.Collections.Generic;
using UnityEngine;

public class StatsChanger : MonoBehaviour
{
    [NonReorderable] [SerializeField] List<StatsChange> statsChanges;


    /*[SerializeField] private GameEventWithData missionCompleted;
    [SerializeField] private GameEventWithData missionFailed;
    [SerializeField] private FloatVariable comfort;
    [SerializeField] private FloatVariable stress;
    [SerializeField] private FloatVariable money;*/
    private void OnEnable()
    {
        foreach (var statsChange in statsChanges)
        {
            statsChange.RegisterListener();
        }
        //    missionCompleted.RegisterListener(MissionCompleted);
        //   missionFailed.RegisterListener(MissionFailed);
    }

    private void OnDisable()
    {
        foreach (var statsChange in statsChanges)
        {
            statsChange.UnregisterListener();
        }
        //    missionCompleted.UnregisterListener(MissionCompleted);
        //   missionFailed.UnregisterListener(MissionFailed);
    }

    /*private void MissionCompleted(DataCarrier dataCarrier)
    {
        if (dataCarrier is DayMission dayMission)
        {
            Mission mission = dayMission.Mission;
            comfort.Value += mission.ComfortChange;
            money.Value += mission.MoneyReward;
            stress.Value += mission.StressChange;
        }
    }

    private void MissionFailed(DataCarrier dataCarrier)
    {
        if (dataCarrier is DayMission dayMission)
        {
            Mission mission = dayMission.Mission;
            comfort.Value += mission.ComfortPunishment;
            money.Value += mission.MoneyPunishment;
            stress.Value += mission.StressPunishment;
        }

    }*/
}