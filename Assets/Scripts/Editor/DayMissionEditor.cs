using System;
using Missions;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(DayMission))]
    public class DayMissionEditor : UnityEditor.Editor
    {
        SerializedProperty mission;
        SerializedProperty hasShowUpTime;
        SerializedProperty showUpTime;
        SerializedProperty isSetTime;
        SerializedProperty earliestTime;
        SerializedProperty latestTime;
        SerializedProperty hasChainedTask;
        SerializedProperty childMission;
        SerializedProperty triggerChainedOnFail;
        SerializedProperty isChainedTask;
        SerializedProperty parentMission;

        private void OnEnable()
        {
            mission = serializedObject.FindProperty("mission");
            hasShowUpTime = serializedObject.FindProperty("hasShowUpTime");
            showUpTime = serializedObject.FindProperty("showUpTime");
            isSetTime = serializedObject.FindProperty("isSetTime");
            earliestTime = serializedObject.FindProperty("earliestTime");
            latestTime = serializedObject.FindProperty("latestTime");
            hasChainedTask = serializedObject.FindProperty("hasChainedTask");
            childMission = serializedObject.FindProperty("childMission");
            triggerChainedOnFail = serializedObject.FindProperty("triggerChainedOnFail");
            isChainedTask = serializedObject.FindProperty("isChainedTask");
            parentMission = serializedObject.FindProperty("parentMission");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(mission);
            EditorGUILayout.PropertyField(hasShowUpTime);

            if (hasShowUpTime.boolValue)
            {
                EditorGUILayout.PropertyField(showUpTime);
            }
            
            EditorGUILayout.PropertyField(isSetTime);
            if (isSetTime.boolValue)
            {
                EditorGUILayout.PropertyField(earliestTime);
                EditorGUILayout.PropertyField(latestTime);
            }

            EditorGUILayout.PropertyField(hasChainedTask);
            if (hasChainedTask.boolValue)
            {
                EditorGUILayout.PropertyField(triggerChainedOnFail);
                EditorGUILayout.PropertyField(childMission);
            }

            EditorGUILayout.PropertyField(isChainedTask);
            if (isChainedTask.boolValue)
            {
                EditorGUILayout.PropertyField(parentMission);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}