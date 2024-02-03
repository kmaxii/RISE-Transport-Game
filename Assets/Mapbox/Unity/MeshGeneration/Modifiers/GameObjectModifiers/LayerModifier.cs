using Mapbox.Unity.MeshGeneration.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mapbox.Unity.MeshGeneration.Modifiers.GameObjectModifiers
{
    [CreateAssetMenu(menuName = "Mapbox/Modifiers/Layer Modifier")]
    public class LayerModifier : GameObjectModifier
    {
        [FormerlySerializedAs("_layerId")] [SerializeField] private int layerId;

        [SerializeField] private Material material;


        public override void Run(VectorEntity ve, UnityTile tile)
        {
            ve.GameObject.layer = layerId;
            ve.GameObject.isStatic = true;

            ve.GameObject.AddComponent<MeshCollider>();

            // Get the current materials
            Material[] currentMaterials = ve.MeshRenderer.materials;

            // Create a new array for the new materials
            Material[] newMaterials = new Material[currentMaterials.Length];

            // Fill the new array with the new material
            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = material;
            }

            // Assign the new materials to the MeshRenderer
            ve.MeshRenderer.materials = newMaterials;
        }
    }
}