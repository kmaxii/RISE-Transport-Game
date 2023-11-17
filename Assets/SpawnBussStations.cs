using System;
using System.Collections.Generic;
using System.IO;
using Mapbox.Examples;
using Newtonsoft.Json;
using UnityEngine;

public class SpawnBussStations : SpawnOnMap
{


    [SerializeField] private String jsoFileName;

    private void Awake()
    {
        ProcessStopPointsFromFile(jsoFileName);
    }

    private void ProcessStopPointsFromFile(string jsonFileName)
    {
        try
        {
            string jsonString = File.ReadAllText("Assets/" + jsonFileName);
            StopPointsData data = JsonConvert.DeserializeObject<StopPointsData>(jsonString);

            HashSet<StopPoint> uniqueStopPoints = new HashSet<StopPoint>(new StopPointComparer());

            foreach (var stopPoint in data.stopPoints)
            {
                if (uniqueStopPoints.Add(stopPoint))
                {
                    Debug.Log($"Added: {stopPoint.name}, Northing: {stopPoint.geometry.northingCoordinate}, Easting: {stopPoint.geometry.eastingCoordinate}");
                    SpawnBussStop(stopPoint);
                }
                else
                {
                    Debug.Log($"Duplicate found, skipped: {stopPoint.name}");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error reading or processing file: {ex.Message}");
        }
    }

    private void SpawnBussStop(StopPoint stopPoint)
    {
        locationStrings.Add($"{stopPoint.geometry.northingCoordinate}, {stopPoint.geometry.eastingCoordinate}");
    }
    
    
        
    private class StopPointComparer : IEqualityComparer<StopPoint>
    {
        public bool Equals(StopPoint x, StopPoint y)
        {
            return x.name == y.name;
        }

        public int GetHashCode(StopPoint obj)
        {
            return obj.name.GetHashCode();
        }
    }
 
}
