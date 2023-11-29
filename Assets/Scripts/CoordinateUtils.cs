using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using UnityEngine;

public class CoordinateUtils
{
    public const int MapSize = 10752;

    // private const float middleOffsetX = -17.27467f;
   // private const float middleOffsetZ = -1.724591f;

    // Define the ranges of the first coordinate system
    private const float minX1 = -2146.21f;// + middleOffsetX;
    private const float maxX1 = 2010.06f;//  + middleOffsetX;
    private const float minZ1 = -2145.28f;//  + middleOffsetZ;
    private const float maxZ1 = 2086.72f;//  + middleOffsetZ;


        
    public static Vector2 ToUiCoords(Vector3 worldCoords)
    {
        // Normalize the input coordinates to a 0-1 range
        double normalizedX = (worldCoords.x - minX1) / (maxX1 - minX1);
        double normalizedZ = (worldCoords.z - minZ1) / (maxZ1 - minZ1);

        // Log the normalized x and z
        Debug.Log($"{normalizedX}, {normalizedZ}");

        // Scale the normalized coordinates to the second coordinate system
        double convertedX = normalizedX * MapSize;
        double convertedZ = normalizedZ * MapSize;

        return new Vector2((float) convertedX, (float) convertedZ);
    }


}