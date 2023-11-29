using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using Utils;
using vasttrafik;

public class BussTravelUI : MonoBehaviour
{

    [TextArea] [ReadOnly] private string textReplacements = "%FN: From Name " +
                                                                             "\n%FT: From Time" +
                                                                             "\n%TN: To Name" +
                                                                             "\n%TT: To time " +
                                                                             "\n%BA: Byten amount" +
                                                                             "\n%BI: Byten info";
    [SerializeField] [TextArea] private string travelInfoTemplate;
    [SerializeField] private TMP_Text travelInfoText;

    private StopPoint _showingInfoFrom;
    private StopPoint _showingInfoTo;

    private GameObject[] _children;
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
    }

    public void HideTravelOption()
    {
        ShowChildren(false);
    }


    public void ShowTravelOption(StopPoint from, StopPoint to, Result result)
    {
        
        Debug.Log("Trip leg count: " + result.tripLegs.Count);
        ShowChildren(true);
        travelInfoText.text = travelInfoTemplate
            .Replace("%FN", from.name)
            .Replace("%FT", new DateTimeFormatter(result.LeaveTime).HourMinute)
            .Replace("%TN", to.name)
            .Replace("%TT", new DateTimeFormatter(result.DestinationTime).HourMinute)
            .Replace("%BA", result.tripLegs[0].journeyLegIndex + "")
            .Replace("%BI", "NOT IMPLEMENTED");
        _showingInfoFrom = from;
        _showingInfoTo = to;
    }

    private void ShowChildren(bool show)
    {
        foreach (var child in _children)
        {
            child.SetActive(show);
        }
    }
}
