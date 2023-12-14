using System.Collections.Generic;
using Mapbox.Examples.Scripts;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using minimap;
using Missions;
using UnityEngine;
using UnityEngine.Serialization;

public class PoiSpawner : MonoBehaviour
{
    [SerializeField] private AbstractMap map;
    
    [SerializeField] private float spawnScale = 1f;

    [FormerlySerializedAs("marker")] [SerializeField] private PoiLabelTextSetter bussStopMarker;
    [SerializeField] private PoiLabelTextSetter missionMarker;
    
    [SerializeField] private TileManagerUI tileManagerUI;

    private Dictionary<Mission, KeyValuePair<PoiLabelTextSetter, MmPoiData>[]> _spawnedMissions;

    [SerializeField] private Sprite bussStopSprite;

    /**
     * Used to keep track of missions that should be called but that could not be spawned yet because the map had not yet been initialized
     */
    private HashSet<Mission> _toSpawn;

    private bool _hasInitialized;

    private void Awake()
    {
        _spawnedMissions = new Dictionary<Mission, KeyValuePair<PoiLabelTextSetter, MmPoiData>[]>();
        _toSpawn = new HashSet<Mission>();
    }

    void Start()
    {
        map.OnInitialized += Initialize;
    }

    private void Initialize()
    {
        map.OnInitialized -= Initialize;
        _hasInitialized = true;
        SpawnBussStations();
        
        //For each mission that was attempted to spawn before the map had initialized
        foreach (var mission in _toSpawn)
        {
            SpawnMission(mission);
        }
        
    }

    public void DestroyMission(Mission mission)
    {
        if (_spawnedMissions.TryGetValue(mission, out var spawned))
        {
            foreach (var pair in spawned)
            {
                Destroy(pair.Key.gameObject);
                tileManagerUI.RemovePoi(pair.Value);
            }
            
            _spawnedMissions.Remove(mission);
        }
    }

    public void SpawnMission(Mission mission)
    {
        if (!_hasInitialized)
        {
            _toSpawn.Add(mission);
            return;
        }
        
        MissionLocation[] locations = mission.MissionLocations;

        List<KeyValuePair<PoiLabelTextSetter, MmPoiData>> spawned =
            new List<KeyValuePair<PoiLabelTextSetter, MmPoiData>>();
        
        foreach (var missionLocation in locations)
        {
            Vector2d loc = Conversions.StringToLatLon(missionLocation.LocationString);
            Debug.Log($"Spawning {mission.MissionName} at {loc}");
            var instance = Instantiate(missionMarker);
            instance.Set(mission.MissionName);
            instance.SetImage(mission.Sprite);
            instance.SetBackgroundColor(mission.Color);
            Vector3 pos = map.GeoToWorldPosition(loc);
            pos.y += 5;
            var transform1 = instance.transform;
            transform1.position = pos;
            transform1.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
            
            Debug.Log($"Spawned {mission.name} at {pos}");
            
            MmPoiData miniMapPoi = tileManagerUI.AddPoi(
                pos,
                PoiType.Mission,
                mission.MissionName,
                mission.Sprite);
            
            
            spawned.Add(new KeyValuePair<PoiLabelTextSetter, MmPoiData>(instance, miniMapPoi));
        }
        _spawnedMissions.Add(mission, spawned.ToArray());
    }

    private void SpawnBussStations()
    {
        List<Vector2d> longLats = new List<Vector2d>();
        
        BussStops bussStops = BussStops.Instance;

        foreach (var bussStopsStopPoint in bussStops.StopPoints)
        {
            var locationString = bussStopsStopPoint.GeoCoords;
            Vector2d loc = Conversions.StringToLatLon(locationString);
            
            longLats.Add(loc);

            Vector3 pos = map.GeoToWorldPosition(loc);
            pos.y = 5;
            bussStops.Set3dPos(pos, bussStopsStopPoint);
            
            tileManagerUI.AddPoi(
                pos,
                PoiType.BussStation,
                bussStopsStopPoint.name,
                bussStopSprite);
            
        }
        
        map._vectorData.SpawnPrefabAtGeoLocation(bussStopMarker.gameObject, longLats.ToArray(), list =>
        {
            foreach (var marker in list)
            {
                
                Vector3 pos = marker.transform.position;
                pos.y = 5;

                if (BussStops.Instance.TryGetStop(pos, out var stop))
                {
                    marker.GetComponent<PoiLabelTextSetter>().Set(stop.name);
                    marker.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
                    marker.transform.position = pos;
                }
                
            }
        });

    }
}