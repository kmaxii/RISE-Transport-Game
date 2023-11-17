using System.Collections.Generic;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnBussStations : MonoBehaviour
{
    [SerializeField]
    private AbstractMap map;
    
    private Vector2d[] _locations;

    [FormerlySerializedAs("_spawnScale")] [SerializeField]
    private float spawnScale = 100f;

    [FormerlySerializedAs("_markerPrefab")] [SerializeField]
    private GameObject markerPrefab;

    private List<GameObject> _spawnedObjects;
    
    
    void Start()
    {
        _locations = new Vector2d[BussStops.Instance.StopPoints.Count];
        _spawnedObjects = new List<GameObject>();

        int i = 0;
        foreach (var bussStopsStopPoint in BussStops.Instance.StopPoints)
        {
            var locationString = bussStopsStopPoint.GeoCoords;
            _locations[i] = Conversions.StringToLatLon(locationString);
            var instance = Instantiate(markerPrefab);
            instance.transform.localPosition = map.GeoToWorldPosition(_locations[i], true);
            instance.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
            _spawnedObjects.Add(instance);
            i++;
        }
        
    }

    private void Update()
    {
        int count = _spawnedObjects.Count;
        for (int i = 0; i < count; i++)
        {
            var spawnedObject = _spawnedObjects[i];
            var location = _locations[i];
            spawnedObject.transform.localPosition = map.GeoToWorldPosition(location, true);
            spawnedObject.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
        }
    }
}
