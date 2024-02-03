using Mapbox.Unity.MeshGeneration.Modifiers.GameObjectModifiers;

namespace Mapbox.Editor
{
	using UnityEngine;
	using UnityEditor;
	using Mapbox.Unity.MeshGeneration.Modifiers;

	[CustomEditor(typeof(LayerModifier))]
	public class LayerModifierEditor : Editor
	{
		public SerializedProperty layerId_Prop;
		public SerializedProperty material;
		private MonoScript script;

		void OnEnable()
		{
			layerId_Prop = serializedObject.FindProperty("layerId");
			material = serializedObject.FindProperty("material");

			script = MonoScript.FromScriptableObject((LayerModifier)target);
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			GUI.enabled = false;
			script = EditorGUILayout.ObjectField("Script", script, typeof(MonoScript), false) as MonoScript;
			GUI.enabled = true;

			layerId_Prop.intValue = EditorGUILayout.LayerField("Layer", layerId_Prop.intValue);
			
			material.objectReferenceValue = EditorGUILayout.ObjectField("Material", material.objectReferenceValue, typeof(Material), false);

			serializedObject.ApplyModifiedProperties();
		}
	}
}