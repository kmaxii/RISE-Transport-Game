using System;
using System.Collections.Generic;

[Serializable]
public class StopPoint
{
    public string name;
    public Geometry geometry;
    
    public String GeoCoords => $"{geometry.northingCoordinate}, {geometry.eastingCoordinate}";

    public float NorthingCoord => geometry.northingCoordinate;
    public float EasternCoord => geometry.eastingCoordinate;
}