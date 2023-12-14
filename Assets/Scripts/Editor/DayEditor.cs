using System.Collections.Generic;
using System.Linq;
using Missions;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Day))]
    public class DayEditor : UnityEditor.Editor
    {
        SerializedProperty dayMissions;
        
        DayMission _newMissionToAdd; // Field to hold the new mission to be added

        private void OnEnable()
        {
            dayMissions = serializedObject.FindProperty("dayMissions");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            RemoveNullMissions();
            SortMissions();

            DisplayMissions(false, "Missions with no set show up Time");
            
            DisplayMissions(true, "Missions with set show up Time");

            RenderNewObjectField();
  

            serializedObject.ApplyModifiedProperties();
        }

        private void RenderNewObjectField()
        {
            // Field to select or drop a new DayMission
            _newMissionToAdd = (DayMission)EditorGUILayout.ObjectField("Add New Mission", _newMissionToAdd, typeof(DayMission), false);

            // Check if a DayMission was assigned to the field
            if (_newMissionToAdd != null)
            {
                dayMissions.InsertArrayElementAtIndex(dayMissions.arraySize);
                dayMissions.GetArrayElementAtIndex(dayMissions.arraySize - 1).objectReferenceValue = _newMissionToAdd;
                _newMissionToAdd = null; // Clear the field after adding
            }
        }

        private void RemoveNullMissions()
        {
            for (int i = 0; i < dayMissions.arraySize; i++)
            {
                SerializedProperty element = dayMissions.GetArrayElementAtIndex(i);
    
                // Check if the element is null and remove it
                if (element.objectReferenceValue == null)
                {
                    dayMissions.DeleteArrayElementAtIndex(i);
                }
            }
        }
        
        private void DisplayMissions(bool showUpTimeValue, string header)
        {
            bool hasShownHeader = false;
            
            for (int i = 0; i < dayMissions.arraySize; i++)
            {
                SerializedProperty element = dayMissions.GetArrayElementAtIndex(i);
                DayMission mission = (DayMission)element.objectReferenceValue;

                // Skip if the element is null or if its isSetTime value doesn't match the current section
                if (mission == null || mission.HasShowUpTime != showUpTimeValue)
                {
                    continue;
                }

                if (!hasShownHeader)
                {
                    EditorGUILayout.LabelField(header, EditorStyles.boldLabel);
                    hasShownHeader = true;
                }
                

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(element, GUIContent.none);

                if (mission.HasShowUpTime)
                {
                    EditorGUILayout.LabelField($"Time: {mission.ShowUpTime.hour:00}:{mission.ShowUpTime.minute:00}", GUILayout.Width(100));

                }
                
                
                
                if (GUILayout.Button("-", GUILayout.Width(20)))
                {
                    dayMissions.DeleteArrayElementAtIndex(i);
                    i--; // Adjust the index after removal
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        private void SortMissions()
        {
            // Convert SerializedProperty to a regular list
            List<DayMission> missions = new List<DayMission>();
            List<DayMission> nullMissions = new List<DayMission>(); // List to store null elements

            for (int i = 0; i < dayMissions.arraySize; i++)
            {
                SerializedProperty element = dayMissions.GetArrayElementAtIndex(i);
                DayMission mission = (DayMission)element.objectReferenceValue;
                if (mission != null)
                {
                    missions.Add(mission);
                }
                else
                {
                    nullMissions.Add(mission); // Add null elements to the separate list
                }
            }

            //Order non-null missions. All elements where mission.HasShowUpTime = false should will be first in an order that does not matter, 
            //After which all missions come that have a mission.ShowUpTime where the first mission is the one with the smallest < 
            //And the last one is the one with the biggest showUp time
            missions = missions
                .Where(mission => !mission.HasShowUpTime) // Select missions where HasShowUpTime is false
                .Concat(missions
                    .Where(mission => mission.HasShowUpTime) // Select missions where HasShowUpTime is true
                    .OrderBy(mission => mission.ShowUpTime)) // Order them by ShowUpTime
                .ToList();

            // Append null elements to the end
            missions.AddRange(nullMissions);

            // Clear the original SerializedProperty list and reassign the sorted elements
            dayMissions.ClearArray();
            foreach (DayMission mission in missions)
            {
                int index = dayMissions.arraySize;
                dayMissions.InsertArrayElementAtIndex(index);
                dayMissions.GetArrayElementAtIndex(index).objectReferenceValue = mission;
            }
        }

    }
}