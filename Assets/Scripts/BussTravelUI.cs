using System.Collections.Generic;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using MaxisGeneralPurpose.Scriptable_objects;
using minimap;
using TMPro;
using UnityEngine;
using Utils;
using vasttrafik;

public class BussTravelUI : MonoBehaviour
{
    [Tooltip("%FN: From Name " +
             "\n%FT: From Time" +
             "\n%TN: To Name" +
             "\n%TT: To time " +
             "\n%BA: Byten amount" +
             "\n%BI: Byten info")]
    [SerializeField] [TextArea] private string travelInfoTemplate;
    [SerializeField] private TMP_Text travelInfoText;
    
    private StopPoint _showingInfoFrom;
    private StopPoint _showingInfoTo;
    private Result _showingResult;
    private Time24H _leaveTime;

    private GameObject[] _children;

    [SerializeField] private AbstractMap map;
    [SerializeField] private TileManagerUI tileManagerUI;
    [SerializeField] private LineRendererHandler lineRenderer;
    
    [SerializeField] private GameEvent timePassedEvent;
    [SerializeField] private GameEvent traveledByBussEvent;
    [SerializeField] private GameEvent showBussTravelCostUI;
    private bool _isEventSubscriber;
    [SerializeField] private TimeVariable currentTime;

    [SerializeField] private IntVariable lastBusSwitches;
    [SerializeField] private TimeVariable lastBusArrive;
    [SerializeField] private IntVariable timeSpentOnPublicTransport;
    [SerializeField] private StringVariable lastBusInfo;
    
    private StopPoint _lastFrom;
    private StopPoint _lastTo;
    void Start()
    {
        //Put all of this children into the children array
        _children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _children[i] = transform.GetChild(i).gameObject;
        }
    }

    private void SubscribeToEventsWhileShowing()
    {
        if (_isEventSubscriber)
        {
            return;
        }
        timePassedEvent.RegisterListener(OnEventRaised);
        traveledByBussEvent.RegisterListener(Travel);
        _isEventSubscriber = true;
    }
    
    private void UnsubscribeToEventsWhileShowing()
    {
        if (!_isEventSubscriber)
        {
            return;
        }
        timePassedEvent.UnregisterListener(OnEventRaised);
        traveledByBussEvent.UnregisterListener(Travel);
        _isEventSubscriber = false;
    }
    
    //Called when clicked on accept in buss ui
    public void AcceptTravel()
    {
        showBussTravelCostUI.Raise();
    }

    //Called from the accept button in the price ui
    private void Travel()
    {
        //The stop is set to be at a higher y so the buss stops are seen well, but we want to ignore that
        Vector3 pos = _showingInfoTo.pos3d;
        pos.y = 0;
        
        GameObject.FindWithTag("Player").transform.position = pos;
        HideTravelOption();
        
        Time24H timeTripTakes = new Time24H(_showingResult.DestinationTime) - _leaveTime;
        timeSpentOnPublicTransport.Value += timeTripTakes.TotalMinutes;

        lastBusArrive.Time24H = new Time24H(_showingResult.DestinationTime);
        
        lastBusInfo.Value = _lastFrom.name + " to " + _lastTo.name;
        
        traveledByBussEvent.Raise();
    }

    public void HideTravelOption()
    {
        UnsubscribeToEventsWhileShowing();
        ShowChildren(false);
    }



    public async void HandleGoing(StopPoint from, StopPoint to)
    {


        StartCoroutine(VasttrafikAPI.GetJourneyJson(from.gid, to.gid, 1, currentTime.Time24H.Rfc3339, result =>
        {
            if (result == null)
            {
                Debug.LogWarning("RESULT FROM VASTTRAFIK IS NULL!");
                return;
            }
        
            _lastFrom = from;
            _lastTo = to;

            int resultNum = result.results[0].SwitchesAmount == -1 && result.results.Count > 1 ? 1 : 0;
        
            SubscribeToEventsWhileShowing();
        
            ShowTravelOption(from, to, result.results[resultNum]);
        }));
    }

    private async void ShowTravelOption(StopPoint from, StopPoint to, Result result)
    {
        
        var leaveTimeTimeFormatter = new DateTimeFormatter(result.LeaveTime);
        _leaveTime = new Time24H(leaveTimeTimeFormatter.Hour, leaveTimeTimeFormatter.Minute);

        _showingResult = result;
        ShowChildren(true);
        travelInfoText.text = travelInfoTemplate
            .Replace("%FN", from.name)
            .Replace("%FT", leaveTimeTimeFormatter.HourMinute)
            .Replace("%TN", to.name)
            .Replace("%TT", new DateTimeFormatter(result.DestinationTime).HourMinute)
            .Replace("%BA", result.SwitchesAmount + "")
            .Replace("%BI", "NOT IMPLEMENTED");
        _showingInfoFrom = from;
        _showingInfoTo = to;

        lastBusSwitches.Value = result.SwitchesAmount;

        lineRenderer.ClearLines();
        
        StartCoroutine(VasttrafikAPI.GetJourneyDetailsJson(result.detailsReference, journeyDetails =>
            {
                List<Vector2> onTripCoords = new List<Vector2> {tileManagerUI.ConvertCoordinatesToLocalPosition(from.pos3d)};

                var firstCoord = journeyDetails.tripLegs[0].tripLegCoordinates[0];
                onTripCoords.Add(tileManagerUI.ConvertCoordinatesToLocalPosition(
                    map.GeoToWorldPosition(new Vector2d(firstCoord.latitude, firstCoord.longitude))));
                lineRenderer.AddLines(onTripCoords, "walk");

                Vector2 lastPos = Vector2.zero;
        
                foreach (var tripLeg in journeyDetails.tripLegs)
                {

                    onTripCoords.Clear();
                    if (tripLeg.serviceJourneys.Count > 1)
                    {
                        for (int i = 0; i < 500; i++)
                        {
                            Debug.LogError("PLEASE REPORT WHAT TRIP YOU DID TO MAXI!");
                        }
                    }
            
                    foreach (var coord in tripLeg.tripLegCoordinates)
                    {
                        lastPos =
                            tileManagerUI.ConvertCoordinatesToLocalPosition(
                                map.GeoToWorldPosition(new Vector2d(coord.latitude, coord.longitude)));
                        onTripCoords.Add(lastPos);

                    }

                    var bussLine = tripLeg.serviceJourneys[0].line;

                    string transportMode = bussLine.transportMode;

            
                    lineRenderer.AddLines(onTripCoords, transportMode);
                }
        
                onTripCoords.Clear();
                onTripCoords.Add(lastPos);
                onTripCoords.Add(tileManagerUI.ConvertCoordinatesToLocalPosition(to.pos3d));
                lineRenderer.AddLines(onTripCoords, "walk");
            }))
        ;
        
      

    }

    private void ShowChildren(bool show)
    {
        foreach (var child in _children)
        {
            child.SetActive(show);
        }
    }

    //On Time Passed
    public void OnEventRaised()
    {
        if (currentTime.Time24H < _leaveTime)
        {
            return;
        }
        
        HandleGoing(_showingInfoFrom, _showingInfoTo);
    }
}