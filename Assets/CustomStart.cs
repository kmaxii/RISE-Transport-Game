using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;

public class CustomStart : MonoBehaviour
{
    [SerializeField] private AbstractMap map;
    [Tooltip("The location the player should start at representing real latitude and longitude")]
    [SerializeField] private string startLocation;

    [SerializeField] private Transform player;

    [Tooltip("We need the player to first spawn at the center pos for the world map to be correctly set up, then we can move them to the correct location")]
    [SerializeField] private float delayToMovePlayer = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(SetPlayerLocToStart), delayToMovePlayer);
    }

    private void SetPlayerLocToStart()
    {
        Vector2d loc = Conversions.StringToLatLon(startLocation);
        
        Vector3 pos = map.GeoToWorldPosition(loc);
        player.position = pos;
    }
  
}
