using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(Mission))]
    public class MissionEditor : UnityEditor.Editor
    {
        SerializedProperty missionName;
        SerializedProperty sprite;
        SerializedProperty canBeDoneAtAllLocation;
        SerializedProperty missionLocations;
        SerializedProperty isFixed;
        SerializedProperty timeItTakes;
        SerializedProperty isSetTime;
        SerializedProperty earliestTime;
        SerializedProperty latestTime;
        SerializedProperty hasChainedTask;
        SerializedProperty childMission;
        SerializedProperty isChainedTask;
        SerializedProperty parentMission;
        SerializedProperty moneyReward;
        SerializedProperty stressChange;
        SerializedProperty comfortChange;
        SerializedProperty moneyPunishment;
        SerializedProperty stressPunishment;
        SerializedProperty comfortPunishment;

        private void OnEnable()
        {
            missionName = serializedObject.FindProperty("missionName");
            sprite = serializedObject.FindProperty("sprite");
            canBeDoneAtAllLocation = serializedObject.FindProperty("canBeDoneAtAllLocation");
            missionLocations = serializedObject.FindProperty("missionLocations");
            isFixed = serializedObject.FindProperty("isFixed");
            timeItTakes = serializedObject.FindProperty("timeItTakes");
            isSetTime = serializedObject.FindProperty("isSetTime");
            earliestTime = serializedObject.FindProperty("earliestTime");
            latestTime = serializedObject.FindProperty("latestTime");
            hasChainedTask = serializedObject.FindProperty("hasChainedTask");
            childMission = serializedObject.FindProperty("childMission");
            isChainedTask = serializedObject.FindProperty("isChainedTask");
            parentMission = serializedObject.FindProperty("parentMission");
            moneyReward = serializedObject.FindProperty("moneyReward");
            stressChange = serializedObject.FindProperty("stressChange");
            comfortChange = serializedObject.FindProperty("comfortChange");
            moneyPunishment = serializedObject.FindProperty("moneyPunishment");
            stressPunishment = serializedObject.FindProperty("stressPunishment");
            comfortPunishment = serializedObject.FindProperty("comfortPunishment");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(missionName);
            EditorGUILayout.PropertyField(sprite);

 
            EditorGUILayout.PropertyField(missionLocations);

            if (missionLocations.arraySize > 1)
            {
                EditorGUILayout.PropertyField(canBeDoneAtAllLocation);
            }

            
            EditorGUILayout.PropertyField(isFixed);
            EditorGUILayout.PropertyField(timeItTakes);

            EditorGUILayout.PropertyField(isSetTime);
            if (isSetTime.boolValue)
            {
                EditorGUILayout.PropertyField(earliestTime);
                EditorGUILayout.PropertyField(latestTime);
            }

            EditorGUILayout.PropertyField(hasChainedTask);
            if (hasChainedTask.boolValue)
            {
                EditorGUILayout.PropertyField(childMission);
            }

            EditorGUILayout.PropertyField(isChainedTask);
            if (isChainedTask.boolValue)
            {
                EditorGUILayout.PropertyField(parentMission);
            }

            EditorGUILayout.PropertyField(moneyReward);
            EditorGUILayout.PropertyField(stressChange);
            EditorGUILayout.PropertyField(comfortChange);
            EditorGUILayout.PropertyField(moneyPunishment);
            EditorGUILayout.PropertyField(stressPunishment);
            EditorGUILayout.PropertyField(comfortPunishment);

            serializedObject.ApplyModifiedProperties();
        }
    }
}