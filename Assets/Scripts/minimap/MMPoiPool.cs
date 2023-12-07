using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace minimap
{
    [Serializable]
    public class MmPoiPool
    {
        [SerializeField] private int defaultCapacity = 16;
        [SerializeField] private int maxSize = 32;
        [SerializeField] private Transform parent;

        private ObjectPool<MiniMapPOI> _poiPool;

        [SerializeField] private MiniMapPOI poiPrefab;
    
        public void Initialize()
        {
            _poiPool = new ObjectPool<MiniMapPOI>(
                createFunc: CreatePoi,
                actionOnGet: OnGetPoi,
                actionOnRelease: OnRelease,
                actionOnDestroy: OnDestroy,
                collectionCheck: false,
                defaultCapacity: defaultCapacity,
                maxSize: maxSize);
        }

        private MiniMapPOI CreatePoi()
        {
            MiniMapPOI poi = Object.Instantiate(poiPrefab, parent);
            poi.transform.parent = parent;

            poi.gameObject.SetActive(false);
            return poi;
        }

        private void OnGetPoi(MiniMapPOI poi)
        {
            poi.gameObject.SetActive(true);
        }

        private void OnRelease(MiniMapPOI poi)
        {
            poi.gameObject.SetActive(false);
        }

        private void OnDestroy(MiniMapPOI poi)
        {
            parent.gameObject.Destroy(poi.gameObject);
        }

        public MiniMapPOI GetPoi(Sprite sprite, string text, PoiType poiType, Vector2 position)
        {
            MiniMapPOI poi = _poiPool.Get();
            
            poi.Setup(sprite, text, poiType);
            
            poi.rectTransform.position = position;
            return poi;
        }

        public void Return(MiniMapPOI poi)
        {
            _poiPool.Release(poi);
        
        }
    }
}