using System.Collections.Generic;
using Editor;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using minimap;
using TMPro;
using UnityEngine;
using Utils;
using vasttrafik;

public class BussTravelUI : MonoBehaviour
{
    /*[TextArea] [ReadOnly] private string textReplacements = "%FN: From Name " +
                                                                             "\n%FT: From Time" +
                                                                             "\n%TN: To Name" +
                                                                             "\n%TT: To time " +
                                                                             "\n%BA: Byten amount" +
                                                                             "\n%BI: Byten info";*/
    [SerializeField] [TextArea] private string travelInfoTemplate;
    [SerializeField] private TMP_Text travelInfoText;

    [SerializeField] private TimeVariable timeVariable;

    private StopPoint _showingInfoFrom;
    private StopPoint _showingInfoTo;
    private Result _showingResult;

    private GameObject[] _children;

    [SerializeField] private AbstractMap map;
    [SerializeField] private TileManagerUI tileManagerUI;
    [SerializeField] private LineRendererHandler lineRenderer;

    void Start()
    {
        //Put all of this children into the children array
        _children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _children[i] = transform.GetChild(i).gameObject;
        }
    }

    public void AcceptTravel()
    {
        GameObject.FindWithTag("Player").transform.position = _showingInfoTo.pos3d;
        HideTravelOption();
        timeVariable.Time24H = new Time24H(_showingResult.DestinationTime);
    }

    public void HideTravelOption()
    {
        ShowChildren(false);
    }


    public async void ShowTravelOption(StopPoint from, StopPoint to, Result result)
    {
        _showingResult = result;
        Debug.Log("Trip leg count: " + result.tripLegs.Count);
        ShowChildren(true);
        travelInfoText.text = travelInfoTemplate
            .Replace("%FN", from.name)
            .Replace("%FT", new DateTimeFormatter(result.LeaveTime).HourMinute)
            .Replace("%TN", to.name)
            .Replace("%TT", new DateTimeFormatter(result.DestinationTime).HourMinute)
            .Replace("%BA", result.SwitchesAmount + "")
            .Replace("%BI", "NOT IMPLEMENTED");
        _showingInfoFrom = from;
        _showingInfoTo = to;


        lineRenderer.ClearLines();
        JourneyDetails journeyDetails = await result.GetJourneyDetails();
        foreach (var tripLeg in journeyDetails.tripLegs)
        {
            List<Vector2> canvasCoords = new List<Vector2>();
            foreach (var coord in tripLeg.tripLegCoordinates)
            {
                Vector3 posInWorld = map.GeoToWorldPosition(new Vector2d(coord.latitude, coord.longitude));
                canvasCoords.Add(tileManagerUI.ConvertCoordinatesToLocalPosition(posInWorld));
            }

            var line = tripLeg.serviceJourneys[0].line;
            string hexColor = line.foregroundColor;

            
            
            if (ColorUtility.TryParseHtmlString(hexColor, out var color))
            {
                lineRenderer.AddLines(canvasCoords, color);
            }
            else
            {
                Debug.LogError("WRONG COLOR: " + hexColor);
            }

        }
    }

    private void ShowChildren(bool show)
    {
        foreach (var child in _children)
        {
            child.SetActive(show);
        }
    }
}