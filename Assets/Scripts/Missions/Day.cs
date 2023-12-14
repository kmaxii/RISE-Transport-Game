

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Missions
{
    [CreateAssetMenu(fileName = "New Day", menuName = "Rise/Day")]

    public class Day : ScriptableObject
    {
        [SerializeField] private List<DayMission> dayMissions;
        
        public List<DayMission> DayMissions => dayMissions;
        
    }
}