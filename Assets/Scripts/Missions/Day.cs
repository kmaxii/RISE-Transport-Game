

using System.Collections.Generic;
using UnityEngine;

namespace Missions
{
    [CreateAssetMenu(fileName = "New Day", menuName = "Rise/Day")]

    public class Day : ScriptableObject
    {
        [SerializeField] private List<DayMission> dayMissions;

    }
}