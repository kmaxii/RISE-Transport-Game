using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New MapSettings", menuName = "Rise/Map/MapSettings")]
    public class MapSettingsSO : ScriptableObject
    {
        [SerializeField] private Vector2 mapOffset = new Vector2(0, 0);
        [SerializeField] private Vector2 mapScale = new Vector2(0, 0);
        [SerializeField] private Vector2 mapPosition = new Vector2(0, 0);
        [SerializeField] private float mapZoomSpeed = 1;
        [SerializeField] private float mapZoomMin = 1;
        [SerializeField] private float mapZoomMax = 1;
    }
}