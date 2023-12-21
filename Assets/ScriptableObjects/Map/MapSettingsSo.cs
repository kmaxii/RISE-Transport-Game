using UnityEngine;
using Debug = UnityEngine.Debug;

namespace ScriptableObjects.Map
{
    [CreateAssetMenu(fileName = "New MapSettings", menuName = "Rise/Map/MapSettings")]
    public class MapSettingsSo : ScriptableObject
    {
        [SerializeField] private float mapZoomSpeed = 0.5f;
        public float MapZoomSpeed => mapZoomSpeed;
        [SerializeField] private float mapZoomMin = 0.35f;
        public float MapZoomMin => mapZoomMin;
        [SerializeField] private float mapZoomMax = 5;
        public float MapZoomMax => mapZoomMax;


        private void OnValidate()
        {
            if (mapZoomMax < mapZoomMin)
            {
                mapZoomMax = mapZoomMin;
                Debug.LogError("MapZoomMax can't be smaller than MapZoomMin");
            }

            if (mapZoomMin > mapZoomMax)
            {
                mapZoomMin = mapZoomMax;
                Debug.LogError("MapZoomMin can't be bigger than MapZoomMax");
            }

            if (mapZoomMin < 0)
            {
                mapZoomMin = 0;
                Debug.LogError("mapZoomMin can't be negative");
            }
        }
    }
}