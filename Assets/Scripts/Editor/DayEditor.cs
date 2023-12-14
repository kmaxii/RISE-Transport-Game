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
        
        DayMission newMissionToAdd; // Field to hold the new mission to add



        private void OnEnable()
        {
            dayMissions = serializedObject.FindProperty("dayMissions");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            SortMissions();

            // Display each mission with a removal button
            for (int i = 0; i < dayMissions.arraySize; i++)
            {
                
                SerializedProperty element = dayMissions.GetArrayElementAtIndex(i);
    
                // Check if the element is null and remove it
                if (element.objectReferenceValue == null)
                {
                    dayMissions.DeleteArrayElementAtIndex(i);
                    continue; 
                }
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(dayMissions.GetArrayElementAtIndex(i), GUIContent.none);
                if (GUILayout.Button("-", GUILayout.Width(20)))
                {
                    dayMissions.DeleteArrayElementAtIndex(i);
                }
                EditorGUILayout.EndHorizontal();
            }

            
            // Field to select or drop a new DayMission
            newMissionToAdd = (DayMission)EditorGUILayout.ObjectField("Add New Mission", newMissionToAdd, typeof(DayMission), false);

            // Check if a DayMission was assigned to the field
            if (newMissionToAdd != null)
            {
                dayMissions.InsertArrayElementAtIndex(dayMissions.arraySize);
                dayMissions.GetArrayElementAtIndex(dayMissions.arraySize - 1).objectReferenceValue = newMissionToAdd;
                newMissionToAdd = null; // Clear the field after adding
            }
            
            
            serializedObject.ApplyModifiedProperties();
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