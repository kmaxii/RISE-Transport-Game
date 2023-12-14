using System;
using minimap;
using UnityEngine;
using vasttrafik;

[Serializable]
public class TripPlanner
{
    [SerializeField] private BussTravelUI bussTravelUI;
    private StopPoint _interactingBussStop;
    private StopPoint _lastClicked;

    [SerializeField] private TimeVariable time;

    public void ClearCurrentData()
    {
        _interactingBussStop = null;
        _lastClicked = null;
    }
    
    public BussStop InteractingBussStop
    {
        set => _interactingBussStop = BussStops.Instance.GetStop(value.GetName());
    }
    public void HandleBussStationClick(MiniMapPOI miniMapPoi)
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

        JourneyResult result = await VasttrafikAPI.GetJourneyJson(_interactingBussStop.gid, stopPoint.gid, 1, time.Time24H.Rfc3339);

        if (result == null)
        {
            Debug.LogWarning("RESULT FROM VASTTRAFIK IS NULL!");
            return;
        }
        
        bussTravelUI.ShowTravelOption(_interactingBussStop, stopPoint, result.results[0]);
    }


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