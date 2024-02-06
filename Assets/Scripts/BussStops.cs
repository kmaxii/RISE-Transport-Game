using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mapbox.Json;
using UnityEngine;

public class BussStops
{
    private static String jsonFileName = "bussstations";

    private bool singleStopPerName = true;

    private HashSet<StopPoint> _uniqueStopPoints;
    private Dictionary<String, StopPoint> _stopPoints;
    
    /**
     * We use a Vector3Int because Vector3s with floats can't be used correctly as keys because of floating point rounding faults
     * If multiple buss stops where to get really close to each other this will lead to errors, but should not be a problem for this project
     */
    private Dictionary<Vector3Int, StopPoint> _stopPointsInWorldPos;

    
    private BussStops()
    {
        _stopPoints = new Dictionary<string, StopPoint>();
        _stopPointsInWorldPos = new Dictionary<Vector3Int, StopPoint>();
        ProcessStopPointsFromFile();
    }
    
    public StopPoint GetStop(string name)
    {
        return _stopPoints[name];
    }
    
    public bool TryGetStop(Vector3 position, out StopPoint stopPoint)
    {
        Vector3Int pos = new Vector3Int((int)position.x, (int)position.y, (int)position.z);
        
        return _stopPointsInWorldPos.TryGetValue(pos, out stopPoint);
    }
    
    public void Set3dPos(Vector3 pos, StopPoint stopPoint)
    {

        Vector3Int posInt = new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z);
        
        _stopPointsInWorldPos.Add(posInt, stopPoint);
        stopPoint.pos3d = pos;
    }


    
    public HashSet<StopPoint> StopPoints => _uniqueStopPoints;

    
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



    private void ProcessStopPointsFromFile()
    {
        try
        {
            
            var textAsset = Resources.Load<TextAsset>(jsonFileName);
            string jsonString = textAsset.text;
            
            StopPointsData data = JsonConvert.DeserializeObject<StopPointsData>(jsonString);

            _uniqueStopPoints = singleStopPerName
                ? new HashSet<StopPoint>(new StopPointComparer())
                : new HashSet<StopPoint>();

            foreach (var stopPoint in data.stopPoints.Where(stopPoint => _uniqueStopPoints.Add(stopPoint)))
            {
                _stopPoints.Add(stopPoint.name, stopPoint);
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
            
            if (x == null || y == null)
            {
                return false;
            }
            
            return x.name == y.name;

        }
        
        public int GetHashCode(StopPoint obj)
        {
            return obj.name.GetHashCode();
        }
    }
}