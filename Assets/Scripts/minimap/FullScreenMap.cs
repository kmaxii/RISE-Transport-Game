using UnityEngine;

namespace minimap
{
    public class FullScreenMap : MonoBehaviour
    {
        private static FullScreenMap _instance;

        public static FullScreenMap Instance => _instance;


        [SerializeField] private GameObject map;
        [SerializeField] private GameObject closeButton;

        [SerializeField] private TripPlanner tripPlanner;

        private void Awake()
        {
            _instance = this;
        }

        public BussStop InteractingBussStop
        {
            set
            {
                tripPlanner.InteractingBussStop = value;
                ShowMap();
            }
        }

        public void ShowMap()
        {
            map.SetActive(true);
            closeButton.SetActive(true);
        }

        public void HideMap()
        {
            map.SetActive(false);
            closeButton.SetActive(false);
            tripPlanner.ClearCurrentData();
        }


        public void ClickedPoi(MiniMapPOI poiType)
        {
            switch (poiType)
            {
                case BussStopPoi poi:
                    tripPlanner.HandleBussStationClick(poi);
                    break;
            }
        }
    }
}