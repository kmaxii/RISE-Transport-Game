using System.Collections;
using System.Collections.Generic;
using Mapbox.Examples;
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

    [SerializeField]
    private PoiLabelTextSetter marker;

    private List<GameObject> _spawnedObjects;

    [SerializeField] private TileManagerUI tileManagerUI;
    
    void Start()
    {
        _locations = new Vector2d[BussStops.Instance.StopPoints.Count];
        _spawnedObjects = new List<GameObject>();

        StartCoroutine(LateStart());
    }


    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);
        int i = 0;
        foreach (var bussStopsStopPoint in BussStops.Instance.StopPoints)
        {
            var locationString = bussStopsStopPoint.GeoCoords;
            _locations[i] = Conversions.StringToLatLon(locationString);
            var instance = Instantiate(marker);
            instance.Set(bussStopsStopPoint.name);
            Vector3 pos = map.GeoToWorldPosition(_locations[i], true);
            bussStopsStopPoint.pos3d = pos;
            pos.y += 5;
            instance.transform.localPosition = pos;
            instance.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
            _spawnedObjects.Add(instance.gameObject);
            i++;
            tileManagerUI.AddPOI(
                CoordinateUtils.ToUiCoords(pos),
                PoiType.BussStation,
                bussStopsStopPoint.name);
        }
        
    }



    private void Update()
    {
        int count = _spawnedObjects.Count;
        for (int i = 0; i < count; i++)
        {
            var spawnedObject = _spawnedObjects[i];
            var location = _locations[i];
            
            Vector3 pos = map.GeoToWorldPosition(location, true);
            pos.y += 1.5f;
            spawnedObject.transform.localPosition = pos;
            spawnedObject.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
        }
    }
}
