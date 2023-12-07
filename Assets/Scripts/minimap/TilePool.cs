using System;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace minimap
{
    [Serializable]
    public class TilePool
    {
        [SerializeField] private int defaultCapacity = 16;
        [SerializeField] private int maxSize = 32;
        [SerializeField] private Transform parent;

        private ObjectPool<Image> _tilePool;
    
        public void Initialize()
        {
            _tilePool = new ObjectPool<Image>(
                createFunc: CreateTile,
                actionOnGet: OnGetTile,
                actionOnRelease: OnReleaseTile,
                actionOnDestroy: OnDestroyTile,
                collectionCheck: false,
                defaultCapacity: defaultCapacity,
                maxSize: maxSize);
        }

        private Image CreateTile()
        {
            Image tileImage = new GameObject("Tile_").AddComponent<Image>();
            tileImage.transform.parent = parent;

            tileImage.gameObject.SetActive(false);
            return tileImage;
        }

        private void OnGetTile(Image tile)
        {
            tile.gameObject.SetActive(true);
        }

        private void OnReleaseTile(Image tile)
        {
            tile.gameObject.SetActive(false);
        }

        private void OnDestroyTile(Image tile)
        {
            parent.gameObject.Destroy(tile.gameObject);
        }

        public Image GetTile(Vector2 position, Sprite sprite)
        {
            Image tile = _tilePool.Get();
            tile.sprite = sprite;
            tile.rectTransform.position = position;
            return tile;
        }

        public void Return(Image tile)
        {

            _tilePool.Release(tile);
        
        }
    }
}