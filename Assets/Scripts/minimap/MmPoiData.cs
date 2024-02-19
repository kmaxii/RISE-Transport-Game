using UnityEngine;

namespace minimap
{
    public struct MmPoiData
    {
        public Vector2 PoiCoordinates;
        public PoiType Type;
        public Sprite Sprite;
        public string Message;
        public bool alwaysShow;
        
        public MmPoiData(Vector2 poiCoordinates, PoiType type, Sprite sprite, string message, bool alwaysShow = false)
        {
            PoiCoordinates = poiCoordinates;
            Type = type;
            Sprite = sprite;
            Message = message;
            this.alwaysShow = alwaysShow;
        }
    }
}