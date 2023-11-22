using Mapbox.Utils;
using UnityEngine;

public class CoordinateUtils
{
    private const float TopLeftLat = 57.769785f;
    private const float TopLeftLong = 11.855405f;
    private const float BottomRightLat = 57.645590f;
    private const float BottomRightLong = 12.083716f;
    private const int MapWidth = 10752;
    private const int MapHeight = 10752;
    

    public static Vector2 ConvertLatLongToGameCoords(double latitude, double longitude)
    {
        // Calculate the relative position of the latitude and longitude
        double xPercent = (longitude - TopLeftLong) / (BottomRightLong - TopLeftLong);
        double yPercent = (TopLeftLat - latitude) / (TopLeftLat - BottomRightLat);

        // Convert the percentage to game coordinates
        int x = (int)(xPercent * MapWidth);
        int y = (int)(yPercent * MapHeight);

        return new Vector2(x, y);
    }
}