using System;
using System.Collections.Generic;
using System.IO;
using Mapbox.Examples;
using Newtonsoft.Json;
using UnityEngine;

public class SpawnBussStations : SpawnOnMap
{

    
    private void Awake()
    {
        foreach (var bussStopsStopPoint in BussStops.Instance.StopPoints)
        {
            SpawnBussStop(bussStopsStopPoint);
        }
    }
    
    private void SpawnBussStop(StopPoint stopPoint)
    {
        locationStrings.Add($"{stopPoint.geometry.northingCoordinate}, {stopPoint.geometry.eastingCoordinate}");
    }
    
    
        

}
