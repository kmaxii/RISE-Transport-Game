﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace minimap
{
    [Serializable]
    public class MapPools
    {
        [SerializeField] private TilePool tilePool;
        [SerializeField] private MmPoiPool poiPool;


        public void Initialize()
        {
            tilePool.Initialize();
            poiPool.Initialize();
        }
        
        public void Return(Image image)
        {
            tilePool.Return(image);
        }

        public void Return(MiniMapPOI mapPoi)
        {
            poiPool.Return(mapPoi);
        }

        public Image GetTile(Sprite sprite)
        {
            return tilePool.GetTile(sprite);
        }

        public MiniMapPOI GetPoi(Vector2 position, Sprite sprite, string text, PoiType poiType)
        {
            return poiPool.GetPoi(sprite, text, poiType, position);
        }      
        
        public MiniMapPOI GetPoi(Vector3 inWorldPos, Sprite sprite, string text, PoiType poiType)
        {
            return GetPoi(CoordinateUtils.ToUiCoords(inWorldPos), sprite, text, poiType);
        }
        
    }
}