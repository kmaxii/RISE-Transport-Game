using UnityEngine;
using vasttrafik;

namespace minimap
{
    public class FullScreenMap : MonoBehaviour
    {
        private static FullScreenMap _instance;

        public static FullScreenMap Instance => _instance;

        private StopPoint _interactingBussStop;

        [SerializeField] private GameObject map;
        [SerializeField] private GameObject closeButton;

        [SerializeField] private BussTravelUI bussTravelUI;

        private void Awake()
        {
            _instance = this;
            Debug.Log("Spawned FullScreenMap");
        }

        public BussStop InteractingBussStop
        {
            set
            {
                _interactingBussStop = BussStops.Instance.GetStop(value.GetName());
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
            _interactingBussStop = null;
            _lastClicked = null;
        }


        public void ClickedPoi(MiniMapPOI poiType)
        {
            switch (poiType)
            {
                case BussStopPoi poi:
                    HandleBussStationClick(poi);
                    break;
            }
        }

        private void HandleBussStationClick(MiniMapPOI miniMapPoi)
        {
            StopPoint stopPoint = BussStops.Instance.GetStop(miniMapPoi.GetText());

            if (_interactingBussStop == null)
            {
                HandlePlanningClick(stopPoint);
                return;
            }

            HandleGoing(stopPoint);
        }

        private async void HandleGoing(StopPoint stopPoint)
        {
            Debug.Log("Interacting buss stop: " + _interactingBussStop.name);

            if (_interactingBussStop.name == stopPoint.name)
            {
                Debug.Log("HANDLE CLICK ON SELF IN THE FUTURE");
                return;
            }

            Debug.Log("Clicked on " + stopPoint.name);
        
            JourneyResult result = await VasttrafikAPI.GetJourneyJson(_interactingBussStop.gid, stopPoint.gid);

            if (result == null)
            {
                Debug.LogWarning("RESULT FROM VASTTRAFIK IS NULL!");
                return;
            }
        
            Debug.Log($"Result amount: " + result.results.Count);
        
            bussTravelUI.ShowTravelOption(_interactingBussStop, stopPoint, result.results[0]);
        }

        private StopPoint _lastClicked;

        private async void HandlePlanningClick(StopPoint stopPoint)
        {
            if (_lastClicked == null || _lastClicked == stopPoint)
            {
                _lastClicked = stopPoint;
                return;
            }

            JourneyResult journey = await VasttrafikAPI.GetJourneyJson(_lastClicked.gid, stopPoint.gid, 7);
            foreach (var trip in journey.results)
            {
                Debug.Log($"Leave time: {trip.LeaveTime}, arrive time: {trip.DestinationTime}");
            }
        }
    }
}