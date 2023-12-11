using System;
using Mapbox.Platform.Cache;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace minimap
{
    public class TileManagerUI : MonoBehaviour, IDragHandler, IScrollHandler
    {
        [SerializeField] private float maxZoom = 5f;
        [SerializeField] private float minZoom = 0.35f;
        [SerializeField] private float zoomSpeed = 0.5f;
        [SerializeField] private int tileSize = 256;
        [SerializeField] private int maxTiles = 16;

        [SerializeField] private ImageTiler imageTiler;
        [SerializeField] private MapPools pools;
        [SerializeField] private Sprite playerSprite;
        [SerializeField] private Transform player;
        [SerializeField] MiniMapPOI poiPrefab;

        private RectTransform _mapRectTransform;
        private Image[,] _tiles;
        private bool[,] _boolTiles;
        private Vector3 _originalScale;
        private MiniMapPOI _playerPoi;
        public RectTransform canvasRectTransform;
        
        private float CurrentZoom => _mapRectTransform.localScale.x;


        
        private Vector2 LocalPos
        {
            get
            {
                var localPosition = _mapRectTransform.localPosition;

                return new Vector2(localPosition.x,
                    localPosition.y);
            }
        }

        /// <summary>
        /// Gets the tile that is currently in the middle of the screen
        /// </summary>
        /// <param name="position">X and y pos of the tile in a 16x16 coordinate system</param>
        /// <returns></returns>
        private Vector2Int GetCenterTile(Vector2 position)
        {
            position /= CurrentZoom;

            position.x += 1792;
            position.y += 2048;

            position.x /= 256;
            position.y /= 256;

            position.x++;
            position.y--;

            position.x = 16 - position.x;
            
            return new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
        }

            

        private void Awake()
        {
            imageTiler.Initialize();
            pools.Initialize();

            if (_mapRectTransform == null)
            {
                _mapRectTransform = GetComponent<RectTransform>();
            }

            _originalScale = _mapRectTransform.localScale;

            _tiles = new Image[maxTiles, maxTiles];
            _boolTiles = new bool[maxTiles, maxTiles];

            _playerPoi = AddPoi(player.position, PoiType.Player, "You", playerSprite);
        }

        private void Update()
        {
            UpdatePlayerPoiPosition();
            RenderTiles();
            LockMapInCanvasBorder();
        }

        

        

        void RenderTiles()
        {
            Vector2Int centerTile = GetCenterTile(LocalPos);

            var rect = canvasRectTransform.rect;
            float canvasWidth = rect.width;
            float canvasHeight = rect.height;

            float adjustedTileSize = tileSize * CurrentZoom;
            
            // Calculate how many tiles are needed horizontally and vertically
            int tilesHorizontal = Mathf.CeilToInt(canvasWidth / adjustedTileSize) + 1;
            int tilesVertical = Mathf.CeilToInt(canvasHeight / adjustedTileSize) + 1;
            
            int minX = Mathf.Max(0, centerTile.x - tilesHorizontal / 2);
            int maxX = Mathf.Min(15, centerTile.x + tilesHorizontal / 2);
            int minY = Mathf.Max(0, centerTile.y - tilesVertical / 2);
            int maxY = Mathf.Min(15, centerTile.y + tilesVertical / 2);

            Debug.Log($"minX: {minX}, maxX: {maxX}, minY: {minY}, maxY: {maxY}");
            
            //  Send every tile that isnt visible to the pool
            /*for (int x = 0; x < maxTiles; x++)
                {
                for (int y = 0; y < maxTiles; y++)
                {
                    if (_tiles[x, y] != null)
                    {
                        if (x < minX || x > maxX || y < minY || y > maxY)
                        {
                            pools.ReturnTile(_tiles[x, y]);
                            _tiles[x, y] = null;
                        }
                    }
                }
            }*/

            for (int x = 0; x < maxTiles; x++) {
                for (int y = 0; y < maxTiles; y++) {
                    if (x < minX || x > maxX || y < minY || y > maxY) {
                        if (_boolTiles[x, y]) {
                            pools.Return(_tiles[x, y]);
                            _boolTiles[x, y] = false;
                        }
                    }
                    else {
                        CreateTile(x, y,  0);
                    }
                }
            }
        }

        void CreateTile(int x, int y, int resolution)
        {
            if (!_boolTiles[x, y])
            {
                var tileImage = pools.GetTile(imageTiler.GetSprite(x, y, resolution));
                _tiles[x, y] = tileImage;
                _boolTiles[x, y] = true;
                float centerX = (maxTiles * tileSize) / 2f;
                float centerY = (maxTiles * tileSize) / 2f;

                tileImage.rectTransform.anchoredPosition =
                    new Vector2((x * tileSize) - centerX, ((maxTiles - 1 - y) * tileSize) - centerY);
                tileImage.rectTransform.sizeDelta = new Vector2(256,256);
                tileImage.transform.localScale = new Vector3(1, 1, 1);

            }
        }

        public MiniMapPOI AddPoi(Vector3 inWorldPos, PoiType poiType, String message, Sprite sprite)
        {
            return AddPoi(CoordinateUtils.ToUiCoords(inWorldPos), poiType, message, sprite);
        }

        public MiniMapPOI AddPoi(Vector2 poiCoordinates, PoiType poiType, String message, Sprite sprite)
        {
            // Convert POI coordinates to map's local coordinates
            Vector2 localPos = ConvertCoordinatesToLocalPosition(poiCoordinates);

            MiniMapPOI poi = Instantiate(poiPrefab, _mapRectTransform);
            poi.Position = localPos;
            poi.Setup(sprite, message, poiType);

            return poi;
        }

        private Vector2 ConvertCoordinatesToLocalPosition(Vector3 inWorldPos)
        {
            Vector2 vector2 = CoordinateUtils.ToUiCoords(inWorldPos);
            return ConvertCoordinatesToLocalPosition(vector2);
        }

        private Vector2 ConvertCoordinatesToLocalPosition(Vector2 poiCoordinates)
        {
            float mapSize = CoordinateUtils.MapSize;

            float localX = (poiCoordinates.x - mapSize / 2f) / mapSize * (tileSize * maxTiles);
            float localY = (poiCoordinates.y - mapSize / 2f) / mapSize * (tileSize * maxTiles);

            return new Vector2(localX - 141.5f, localY - 101);
        }


        private void UpdatePlayerPoiPosition()
        {
            _playerPoi.Position = ConvertCoordinatesToLocalPosition(player.position);
        }
        
        private void LockMapInCanvasBorder() {
        
            // Get the size of the canvas
            Vector2 canvasSize = canvasRectTransform.rect.size;

            // Get the size of the map at the current zoom level
            Vector2 mapSize = new Vector2(tileSize * maxTiles, tileSize * maxTiles) * CurrentZoom;

            // Adjust the offset to correctly position the map
            // We subtract half a tile size to bring the extra tile into the view
            float halfTileSize = tileSize * 0.5f * CurrentZoom;
            float offset = halfTileSize;
            float offsetY = halfTileSize;

            // Calculate the maximum allowed positions, including the adjusted offset
            float maxX = (canvasSize.x - mapSize.x) / 2f + offset;
            float maxY = (canvasSize.y - mapSize.y) / 2f + offsetY;

            // Ensure that the map is at least as big as the canvas
            maxX = Mathf.Min(maxX, offset);

            // Get the current position of the map
            Vector2 currentPosition = _mapRectTransform.anchoredPosition;

            // Clamp the map's position
            float clampedX = Mathf.Clamp(currentPosition.x, maxX, -maxX + tileSize * CurrentZoom);
            float clampedY = Mathf.Clamp(currentPosition.y, maxY, -maxY + tileSize * CurrentZoom);

            // Apply the clamped position with adjusted offset
            _mapRectTransform.anchoredPosition = new Vector2(clampedX, clampedY);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _mapRectTransform.anchoredPosition += eventData.delta;
        }

        public void OnScroll(PointerEventData eventData)
        {
            Vector2 cursorPosition = eventData.position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_mapRectTransform, cursorPosition,
                eventData.pressEventCamera, out Vector2 localCursor);

            float scroll = eventData.scrollDelta.y;
            Vector3 oldScale = _mapRectTransform.localScale;
            Vector3 newScale = oldScale + Vector3.one * scroll * zoomSpeed;
            newScale = new Vector3(
                Mathf.Clamp(newScale.x, _originalScale.x * minZoom, _originalScale.x * maxZoom),
                Mathf.Clamp(newScale.y, _originalScale.y * minZoom, _originalScale.y * maxZoom), 1
            );

            // Calculate the ratio of change in scale
            Vector3 scaleRatioChange = newScale - oldScale;
            Vector2 adjustedCursorPos =
                new Vector2(localCursor.x * scaleRatioChange.x, localCursor.y * scaleRatioChange.y);

            // Adjust the position to keep the cursor point stationary
            Vector2 newPosition = _mapRectTransform.anchoredPosition - adjustedCursorPos;

            // Apply the new scale and adjusted position
            _mapRectTransform.localScale = newScale;
            _mapRectTransform.anchoredPosition = newPosition;
        }
    
    }
}