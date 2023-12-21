using Missions;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(Mission))]
    public class MissionEditor : UnityEditor.Editor
    {
        SerializedProperty missionName;
        SerializedProperty sprite;
        SerializedProperty color;
        SerializedProperty canBeDoneAtAllLocation;
        SerializedProperty missionLocations;
        SerializedProperty timeItTakes;


        
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
            color = serializedObject.FindProperty("color");
            canBeDoneAtAllLocation = serializedObject.FindProperty("canBeDoneAtAllLocation");
            missionLocations = serializedObject.FindProperty("missionLocations");
            timeItTakes = serializedObject.FindProperty("timeItTakes");
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
            EditorGUILayout.PropertyField(color);

 
            EditorGUILayout.PropertyField(missionLocations);

            if (missionLocations.arraySize > 1)
            {
                EditorGUILayout.PropertyField(canBeDoneAtAllLocation);
            }

            
            EditorGUILayout.PropertyField(timeItTakes);
            
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