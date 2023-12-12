using System.Collections.Generic;
using JetBrains.Annotations;
using minimap;
using UnityEngine;

public class MmPoiHolder
{
    private Dictionary<Vector2Int, HashSet<MmPoiData>> _pois;
    private Dictionary<MmPoiData, Vector2Int> _poiLocations;

    

    public MmPoiHolder()
    {
        _pois = new Dictionary<Vector2Int, HashSet<MmPoiData>>();
        _poiLocations = new Dictionary<MmPoiData, Vector2Int>();
    }


    public void Add(Vector2Int pos, MmPoiData poi)
    {
        if (!_pois.ContainsKey(pos))
        {
            _pois[pos] = new HashSet<MmPoiData>();
        }

        _pois[pos].Add(poi);
        
        _poiLocations.Add(poi, pos);
    }

    public void Remove(MmPoiData poi)
    {
        if (!_poiLocations.TryGetValue(poi, out Vector2Int pos))
        {
            return;
        }
        Remove(pos, poi);
        _poiLocations.Remove(poi);
    }

    public void Remove(Vector2Int pos, MmPoiData poi)
    {
        if (!_pois.ContainsKey(pos))
        {
            return;
        }

        _pois[pos].Remove(poi);
    }
    
    [CanBeNull]
    public HashSet<MmPoiData> Get(Vector2Int pos)
    {
        if (!_pois.ContainsKey(pos))
        {
            return null;
        }

        return _pois[pos];
    }
}