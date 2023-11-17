using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class BussStops
{
    private static String jsonFileName = "bussstations.json";

    private bool singleStopPerName = true;

    private HashSet<StopPoint> _uniqueStopPoints;

    public HashSet<StopPoint> StopPoints
    {
        get => _uniqueStopPoints;
    }

    //Singleton
    private static BussStops _instance;

    public static BussStops Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BussStops();
            }

            return _instance;
        }
    }

    private BussStops()
    {
        ProcessStopPointsFromFile();

    }

    private void ProcessStopPointsFromFile()
    {
        try
        {
            string jsonString = File.ReadAllText("Assets/" + jsonFileName);
            StopPointsData data = JsonConvert.DeserializeObject<StopPointsData>(jsonString);

            _uniqueStopPoints = singleStopPerName
                ? new HashSet<StopPoint>(new StopPointComparer())
                : new HashSet<StopPoint>();

            foreach (var stopPoint in data.stopPoints)
            {
                if (_uniqueStopPoints.Add(stopPoint))
                {
                    Debug.Log(
                        $"Added: {stopPoint.name}, Northing: {stopPoint.geometry.northingCoordinate}, Easting: {stopPoint.geometry.eastingCoordinate}");
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