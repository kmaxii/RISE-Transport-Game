using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using UnityEngine;

public class CoordinateUtils
{
    private const float TopLeftLat = 57.769785f;
    private const float TopLeftLong = 11.855405f;
    private const float BottomRightLat = 57.645590f;
    private const float BottomRightLong = 12.083716f;
    public const int MapSize = 10752;

    private static Vector2 worldTopLeft = new Vector2(-1067.259f, 1048.266f);
    private static Vector2 worldBottomRight = new Vector2(1032.728f, -1051.723f);

    //  private static Vector2d centerMerc = new Vector2d(7906351.00352, 1332861.65912);
    // private static float worldRelativeScale = 0.1635333f;

   // private const float middleOffsetX = -17.27467f;
   // private const float middleOffsetZ = -1.724591f;

    // Define the ranges of the first coordinate system
    private const float minX1 = -2146.21f;// + middleOffsetX;
    private const float maxX1 = 2010.06f;//  + middleOffsetX;
    private const float minZ1 = -2145.28f;//  + middleOffsetZ;
    private const float maxZ1 = 2086.72f;//  + middleOffsetZ;


        
    public static Vector2 ToUiCoords(Vector3 worldCoords)
    {
       // worldCoords.x -= middleOffsetX;
      //  worldCoords.z -= middleOffsetZ;
       // 
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

    public static Vector2 ConvertLatLongToGameCoords(double latitude, double longitude, AbstractMap map)
    {
        /*  var convertedCoords = Conversions.GeoToWorldPosition(latitude, longitude, map.CenterMercator,
              map.WorldRelativeScale);
          
          // Normalize the input coordinates to a 0-1 range
          double normalizedX = (convertedCoords.x - minX1) / (maxX1 - minX1);
          double normalizedZ = (convertedCoords.y - minZ1) / (maxZ1 - minZ1);
  
          //Log the normalizedx and z
          Debug.Log($"{normalizedZ}, {normalizedZ}");
          
          // Scale the normalized coordinates to the second coordinate system
          double convertedX = normalizedX * sizeX2;
          double convertedZ = normalizedZ * sizeZ2;
  
          return new Vector2((float) convertedX, (float) convertedZ);
          /

        Vector2 asVec2 = new Vector2((float) convertedCoords.x, (float) convertedCoords.y);
        Debug.Log(
            $"lat {latitude} long: {longitude}, mapmeractor: {map.CenterMercator} marp world relative scale: {map.WorldRelativeScale} Converted coords: {convertedCoords} asVec2 {asVec2}");


// Calculate the relative position of the latitude and longitude
        double xPer = (convertedCoords.x - worldTopLeft.x) / (worldBottomRight.x - worldTopLeft.x);
        double yPer = (convertedCoords.y - worldTopLeft.y) / (worldBottomRight.y - worldTopLeft.y);


        return new Vector2((int) (xPer * MapWidth), (int) (yPer * MapHeight));


        return asVec2;
        */


        // Calculate the relative position of the latitude and longitude
        double xPercent = (longitude - TopLeftLong) / (BottomRightLong - TopLeftLong);
        double yPercent = (TopLeftLat - latitude) / (TopLeftLat - BottomRightLat);

        // Convert the percentage to game coordinates
        int x = (int) (xPercent * MapSize);
        int y = (int) (yPercent * MapSize);

        return new Vector2(x, y);
    }
}