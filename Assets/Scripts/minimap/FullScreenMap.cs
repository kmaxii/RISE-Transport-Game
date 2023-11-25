using System;
using minimap;
using Unity.VisualScripting;
using UnityEngine;

public class FullScreenMap : MonoBehaviour
{
    private static FullScreenMap _instance;

    public static FullScreenMap Instance => _instance;

    private BussStop _interactingBussStop = null;

    [SerializeField] private GameObject map;
    [SerializeField] private GameObject closeButton;

    private void Awake()
    {
        _instance = this;
        Debug.Log("Spawned FullScreenMap");
    }
    
    public BussStop InteractingBussStop
    {
        get => _interactingBussStop;
        set
        {
            _interactingBussStop = value;
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
        Debug.Log("buss stop poi " + miniMapPoi.GetText());

        StopPoint stopPoint = BussStops.Instance.GetStop(miniMapPoi.GetText());
        
        if (!_interactingBussStop)
            return;
        
        Debug.Log("Interacting buss stop: " + _interactingBussStop.GetName());

        if (_interactingBussStop.GetName() == stopPoint.name)
        {
            Debug.Log("HANDLE CLICK ON SELF IN THE FUTURE");
            return;
        }
        
        Debug.Log("Clicked on " + stopPoint.name);
        
        
    }
}