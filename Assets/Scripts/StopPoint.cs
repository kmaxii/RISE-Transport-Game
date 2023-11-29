using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StopPoint
{
    public string name;
    public Geometry geometry;
    public string gid;
    
    public Vector3 pos3d { get; set; }

    public String GeoCoords => $"{geometry.northingCoordinate}, {geometry.eastingCoordinate}";

    public float NorthingCoord => geometry.northingCoordinate;
    public float EasternCoord => geometry.eastingCoordinate;
}