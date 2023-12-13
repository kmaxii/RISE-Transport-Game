﻿using Editor;
using UnityEngine;

namespace Missions
{
    [CreateAssetMenu(fileName = "New_DayMission", menuName = "Rise/DayMission")]
    public class DayMission : ScriptableObject
    {
        [SerializeField] private Mission mission;

        [Tooltip("If false, will show up at the begging of the day, if true, will show up from the set time")]
        [SerializeField] private bool hasShowUpTime;
        [SerializeField] private Time24H showUpTime; // Visibility managed in custom editor

        [Tooltip("If true, needs to be performed between a set time. If false, can be performed any time")]
        [SerializeField]
        private bool isSetTime;
        [Tooltip("At what time the task starts being available")] [SerializeField]
        private Time24H earliestTime; // Visibility managed in custom editor
        [Tooltip("When is the task considered failed?")] [SerializeField]
        private Time24H latestTime = new Time24H(23, 59); // Visibility managed in custom editor
        
        
        
        [Header("Chained Task")] [Tooltip("Does doing this task create other tasks")] [SerializeField]
        private bool hasChainedTask;
        [SerializeField] private Mission childMission; // Visibility managed in custom editor

        [Tooltip("If the task is dependent on another task")] [SerializeField]
        private bool isChainedTask;
        [SerializeField] private Mission parentMission; // Visibility managed in custom editor

        public override string ToString()
        {
            //Return a string with mission name and time it has to be done if there is one
            if (isSetTime)
            {
                return $"{mission} at {earliestTime} - {latestTime}";
            }

            return mission.ToString();
        }
    }
}