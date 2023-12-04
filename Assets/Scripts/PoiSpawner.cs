using System.Collections;
using System.Collections.Generic;
using Mapbox.Examples.Scripts;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using minimap;
using UnityEngine;
using UnityEngine.Serialization;

public class PoiSpawner : MonoBehaviour
{
    [SerializeField] private AbstractMap map;
    
    [SerializeField] private float spawnScale = 1f;

    [FormerlySerializedAs("marker")] [SerializeField] private PoiLabelTextSetter bussStopMarker;
    [SerializeField] private PoiLabelTextSetter missionMarker;
    
    [SerializeField] private TileManagerUI tileManagerUI;

    private Dictionary<Mission, KeyValuePair<PoiLabelTextSetter, MiniMapPOI>[]> _spawnedMissions;


    private void Awake()
    {
        _spawnedMissions = new Dictionary<Mission, KeyValuePair<PoiLabelTextSetter, MiniMapPOI>[]>();
    }

    void Start()
    {
        StartCoroutine(LateStart());
    }


    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);
        SpawnBussStations();
    }

    public void DestroyMission(Mission mission)
    {
        if (_spawnedMissions.TryGetValue(mission, out var spawned))
        {
            foreach (var pair in spawned)
            {
                Destroy(pair.Key.gameObject);
                Destroy(pair.Value.gameObject);
            }
            
            _spawnedMissions.Remove(mission);
        }
    }

    public void SpawnMission(Mission mission)
    {
        MissionLocation[] locations = mission.MissionLocations;

        List<KeyValuePair<PoiLabelTextSetter, MiniMapPOI>> spawned =
            new List<KeyValuePair<PoiLabelTextSetter, MiniMapPOI>>();
        
        foreach (var missionLocation in locations)
        {
            Vector2d loc = Conversions.StringToLatLon(missionLocation.LocationString);
            var instance = Instantiate(missionMarker);
            instance.Set(mission.name);
            instance.SetImage(mission.Sprite);
            instance.SetBackgroundColor(mission.Color);
            Vector3 pos = map.GeoToWorldPosition(loc);
            pos.y += 5;
            var transform1 = instance.transform;
            transform1.localPosition = pos;
            transform1.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
            MiniMapPOI miniMapPoi = tileManagerUI.AddPoi(
                CoordinateUtils.ToUiCoords(pos),
                PoiType.Mission,
                mission.name);
            
            miniMapPoi.SetImage(mission.Sprite);
            
            spawned.Add(new KeyValuePair<PoiLabelTextSetter, MiniMapPOI>(instance, miniMapPoi));
        }
        _spawnedMissions.Add(mission, spawned.ToArray());
    }

    private void SpawnBussStations()
    {
        foreach (var bussStopsStopPoint in BussStops.Instance.StopPoints)
        {
            var locationString = bussStopsStopPoint.GeoCoords;
            Vector2d loc = Conversions.StringToLatLon(locationString);
            var instance = Instantiate(bussStopMarker);
            instance.Set(bussStopsStopPoint.name);
            Vector3 pos = map.GeoToWorldPosition(loc);
            bussStopsStopPoint.pos3d = pos;
            pos.y += 5;
            var transform1 = instance.transform;
            transform1.localPosition = pos;
            transform1.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
            tileManagerUI.AddPoi(
                CoordinateUtils.ToUiCoords(pos),
                PoiType.BussStation,
                bussStopsStopPoint.name);
        }
    }
}