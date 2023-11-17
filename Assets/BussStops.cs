using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
[CreateAssetMenu(menuName = "custom/BussStopsList")]
public class BussStops : ScriptableObject
{
    [SerializeField] private String jsonFileName;

    [SerializeField] private bool singleStopPerName = true;

    private HashSet<StopPoint> uniqueStopPoints;

    private void Awake()
    {
        ProcessStopPointsFromFile(jsonFileName);
    }

    private void ProcessStopPointsFromFile(string jsonFileName)
    {
        try
        {
            string jsonString = File.ReadAllText("Assets/" + jsonFileName);
            StopPointsData data = JsonConvert.DeserializeObject<StopPointsData>(jsonString);

            uniqueStopPoints = singleStopPerName ? new HashSet<StopPoint>(new StopPointComparer()) : new HashSet<StopPoint>();

            foreach (var stopPoint in data.stopPoints)
            {
                if (uniqueStopPoints.Add(stopPoint))
                {
                    Debug.Log($"Added: {stopPoint.name}, Northing: {stopPoint.geometry.northingCoordinate}, Easting: {stopPoint.geometry.eastingCoordinate}");
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
