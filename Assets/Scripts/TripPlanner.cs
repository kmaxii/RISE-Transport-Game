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
    
    public Interactable3dPoi InteractingInteractable3dPoi
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

       
        bussTravelUI.HandleGoing(_interactingBussStop, stopPoint);

        
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