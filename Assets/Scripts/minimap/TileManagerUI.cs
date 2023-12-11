using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace minimap
{
    public class TileManagerUI : MonoBehaviour, IDragHandler, IScrollHandler
    {
        [SerializeField] private float maxZoom = 5f;
        [SerializeField] private float minZoom = 0.35f;
        [SerializeField] private float zoomSpeed = 0.5f;
        [SerializeField] private int tileSize = 256;
        private int _currentTileSize = 256;
        [SerializeField] private int maxTiles = 16;
        private int _currentMaxTiles = 16;

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

            _tiles = new Image[_currentMaxTiles, _currentMaxTiles];
            _boolTiles = new bool[_currentMaxTiles, _currentMaxTiles];

            _playerPoi = AddPoi(player.position, PoiType.Player, "You", playerSprite);
        }

        private void Update()
        {
            UpdatePlayerPoiPosition();
            RenderTiles();
           // LockMapInCanvasBorder();
        }


        void RenderTiles()
        {
            int resolution = imageTiler.GetResolution(CurrentZoom);

            _currentTileSize = (tileSize * resolution); // * (tileSize * resolution);
            _currentMaxTiles = (maxTiles / resolution);

            Vector2Int centerTile = GetCenterTile(LocalPos);

            centerTile.x /= resolution;
            centerTile.y /= resolution;

            var rect = canvasRectTransform.rect;
            float canvasWidth = rect.width;
            float canvasHeight = rect.height;

            float adjustedTileSize = _currentTileSize * CurrentZoom;

            // Calculate how many tiles are needed horizontally and vertically
            int tilesHorizontal = Mathf.CeilToInt(canvasWidth / adjustedTileSize) + 1;
            int tilesVertical = Mathf.CeilToInt(canvasHeight / adjustedTileSize) + 1;

            int minX = Mathf.Max(0, centerTile.x - tilesHorizontal / 2);
            int maxX = Mathf.Min(_currentMaxTiles - 1, centerTile.x + tilesHorizontal / 2);
            int minY = Mathf.Max(0, centerTile.y - tilesVertical / 2);
            int maxY = Mathf.Min(_currentMaxTiles - 1, centerTile.y + tilesVertical / 2);
            

            for (int x = 0; x < _currentMaxTiles; x++)
            {
                for (int y = 0; y < _currentMaxTiles; y++)
                {
                    if (x < minX || x > maxX || y < minY || y > maxY)
                    {
                        if (_boolTiles[x, y])
                        {
                            pools.Return(_tiles[x, y]);
                            _boolTiles[x, y] = false;
                        }
                    }
                    else
                    {
                        CreateTile(x, y);
                    }
                }
            }
        }

        void CreateTile(int x, int y)
        {
            //   Debug.Log($"x: {x} y: {y}");
            if (!_boolTiles[x, y])
            {
                // Get the resolution (1, 2, or 4) and calculate the tile size based on it
                int resolution = imageTiler.GetResolution(CurrentZoom);

                Image tileImage = pools.GetTile(imageTiler.GetSprite(x, y, resolution));

                // Keep track of the spawned image
                _tiles[x, y] = tileImage;
                _boolTiles[x, y] = true;

                // Calculate the position of the tile
                // Adjust the calculation to account for varying tile sizes
                float offsetX = (_currentTileSize * (_currentMaxTiles / 2f)) - (_currentTileSize / 2f) + tileSize / 2f;
                float offsetY = (_currentTileSize * (_currentMaxTiles / 2f)) - (_currentTileSize / 2f) + tileSize / 2f;

                tileImage.rectTransform.anchoredPosition = new Vector2(
                    (x * _currentTileSize) - offsetX,
                    ((_currentMaxTiles - 1 - y) * _currentTileSize) - offsetY
                );

                // Set the size of the tile
                tileImage.rectTransform.sizeDelta = new Vector2(_currentTileSize, _currentTileSize);
                tileImage.transform.localScale = new Vector3(1, 1, 1);
            }
            
            /*
   
                            
                //Calculate the center of the coordinate system
                float centerX = (_currentMaxTiles * _currentTileSize) / 2f;
                float centerY = (_currentMaxTiles * _currentTileSize) / 2f;
                
            }
             */
        }

        public MiniMapPOI AddPoi(Vector3 inWorldPos, PoiType poiType, String message, Sprite sprite)
        {
            return AddPoi(CoordinateUtils.ToUiCoords(inWorldPos), poiType, message, sprite);
        }

        public MiniMapPOI AddPoi(Vector2 poiCoordinates, PoiType poiType, String message, Sprite sprite)
        {
            // Convert POI coordinates to map's local coordinates
            Vector2 localPos = ConvertCoordinatesToLocalPosition(poiCoordinates);

            MiniMapPOI poi = pools.GetPoi(localPos, sprite, message, poiType);
            // MiniMapPOI poi = Instantiate(poiPrefab, _mapRectTransform);
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

            float localX = (poiCoordinates.x - mapSize / 2f) / mapSize * (_currentTileSize * _currentMaxTiles);
            float localY = (poiCoordinates.y - mapSize / 2f) / mapSize * (_currentTileSize * _currentMaxTiles);

            Vector2 offset = new Vector2(-141.5f, -101);
            //   offset *= imageTiler.GetResolution(CurrentZoom);

            return new Vector2(localX + offset.x, localY + offset.y);
        }


        private void UpdatePlayerPoiPosition()
        {
            _playerPoi.Position = ConvertCoordinatesToLocalPosition(player.position);
        }

        private void LockMapInCanvasBorder()
        {
            // Get the size of the canvas
            Vector2 canvasSize = canvasRectTransform.rect.size;

            // Get the size of the map at the current zoom level
            Vector2 mapSize = new Vector2(_currentTileSize * _currentMaxTiles, _currentTileSize * _currentMaxTiles) *
                              CurrentZoom;

            // Adjust the offset to correctly position the map
            // We subtract half a tile size to bring the extra tile into the view
            float halfTileSize = _currentTileSize * 0.5f * CurrentZoom;
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
            float clampedX = Mathf.Clamp(currentPosition.x, maxX, -maxX + _currentTileSize * CurrentZoom);
            float clampedY = Mathf.Clamp(currentPosition.y, maxY, -maxY + _currentTileSize * CurrentZoom);

            // Apply the clamped position with adjusted offset
            _mapRectTransform.anchoredPosition = new Vector2(clampedX, clampedY);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _mapRectTransform.anchoredPosition += eventData.delta;
        }

        private int _lastRes = 9993;

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

            int res = imageTiler.GetResolution(CurrentZoom);
            if (res != _lastRes)
            {
                RemoveAllTiles();
                _lastRes = res;
            }
        }

        private void RemoveAllTiles()
        {
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    if (_boolTiles[x, y])
                    {
                        _boolTiles[x, y] = false;
                        pools.Return(_tiles[x, y]);
                    }
                }
            }
        }
    }
}