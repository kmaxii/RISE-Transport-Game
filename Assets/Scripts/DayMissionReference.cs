using MaxisGeneralPurpose.Event;
using Missions;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/data/dayMission")]
public class DayMissionReference : DataCarrier
{
    [SerializeField] private DayMission dayMission;

    public DayMission Value
    {
        get => dayMission;
        set
        {
            dayMission = value;
            if (raiseOnValueChanged)
                raiseOnValueChanged.Raise();
        }
    }

    public override string ToString()
    {
        return dayMission.ToString();
    }
}