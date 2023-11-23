using System;
using Unity.VisualScripting;
using UnityEngine;

public class FullScreenMap : MonoBehaviour
{
    private static FullScreenMap _instance;

    public static FullScreenMap Instance => _instance;

    private BussStop _interactingBussStop = null;

    [SerializeField] private GameObject map;
    [SerializeField] private GameObject closeButton;

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


    public void ResetInteractingBussStop()
    {
        InteractingBussStop = null;
    }


    private void Awake()
    {
        _instance = this;
    }

    public void ClickedPoi(string name, PoiType poiType)
    {
        switch (poiType)
        {
            case PoiType.BussStation:
                HandleBussStationClick(name);
                break;
        }
    }

    private void HandleBussStationClick(String name)
    {
        if (!_interactingBussStop)
            return;
        
        if (_interactingBussStop.name == name)
        {
            Debug.Log("HANDLE CLICK ON SELF IN THE FUTURE");
            return;
        }
        
        
        
    }
}