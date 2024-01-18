using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using MaxisGeneralPurpose.Scriptable_objects;
using Scriptable_objects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace minimap
{
    public class TileManagerUI : MonoBehaviour, IDragHandler, IScrollHandler, IEventListenerInterface
    {
        private float _maxZoom = 5f;

        public float MaxZoom
        {
            set => _maxZoom = value;
        }

        private float _minZoom = 0.35f;

        public float MinZoom
        {
            set => _minZoom = value;
        }

        private float _zoomSpeed = 0.5f;

        public float ZoomSpeed
        {
            set => _zoomSpeed = value;
        }

        [SerializeField] private int tileSize = 256;
        private int _currentTileSize = 256;
        [SerializeField] private int maxTiles = 16;
        private int _currentMaxTiles = 16;

        [SerializeField] private ImageTiler imageTiler;
        [SerializeField] private MapPools pools;
        [SerializeField] private Sprite playerSprite;
        [SerializeField] private Transform player;

        private RectTransform _mapRectTransform;
        private Image[,] _tiles;
        private bool[,] _boolTiles;
        private Vector3 _originalScale;
        private MiniMapPOI _playerPoi;
        public RectTransform canvasRectTransform;

        [SerializeField] private GameEvent onPlayerMove;

        private void OnEnable()
        {
            onPlayerMove.RegisterListener(this);
        }

        private void OnDisable()
        {
            onPlayerMove.UnregisterListener(this);
        }

        private float CurrentZoom => _mapRectTransform.localScale.x;


        private MmPoiHolder _poiHolder;
        private Vector2 LocalPos
        {
            get
            {
                var localPosition = _mapRectTransform.localPosition;

                return new Vector2(localPosition.x,
                    localPosition.y);
            }
        }


        private Vector2 _lastScreenSize;

        private Vector2 TopRightCoords => canvasRectTransform.rect.size / 2f - _mapRectTransform.anchoredPosition;
        private Vector2 BottomLeftCoords => -canvasRectTransform.rect.size / 2f - _mapRectTransform.anchoredPosition;

        private void Update()
        {
            Vector2 currentScreenSize = new Vector2(Screen.width, Screen.height);
            if (currentScreenSize == _lastScreenSize) return;
            UpdateMap();
            _lastScreenSize = currentScreenSize;
        }

        /// <summary>
        /// Gets the tile that is currently in the middle of the screen
        /// </summary>
        /// <param name="position"></param>
        /// <returns>X and y pos of the tile in a 16x16 coordinate system</returns>
        private Vector2Int GetTileAtPosition(Vector2 position)
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
            _poiHolder = new MmPoiHolder();

            if (_mapRectTransform == null)
            {
                _mapRectTransform = GetComponent<RectTransform>();
            }

            _originalScale = _mapRectTransform.localScale;

            _tiles = new Image[maxTiles, maxTiles];
            _boolTiles = new bool[maxTiles, maxTiles];

            _playerPoi = pools.GetPoi(player.position, playerSprite, "You", PoiType.Player);
            UpdateMap();
        }

        private Vector3 _playerLastPos = new Vector3(9999, 9999, 9999);
        private float _minimumMoved = 5;
        public void UpdateIfMovedEnough()
        {
            //Check if the quared distance of player.transform.position and _playerLastPos is bigger then _minimumMoved squared
            if (Vector3.SqrMagnitude(player.transform.position - _playerLastPos) > _minimumMoved * _minimumMoved)
            {
                _playerLastPos = player.transform.position;
                UpdateMap();
                
            }
        }
        
        
        public void UpdateMap()
        {
            UpdatePlayerPoiPosition();
            RenderTiles();
            LockMapInCanvasBorder();
            RenderPois();
        }


        private void RenderPois()
        {
            Vector2Int topRightTile16 = GetTileAtPosition(BottomLeftCoords);
            Vector2Int bottomLeftTile16 = GetTileAtPosition(TopRightCoords);

            HashSet<MmPoiData> shouldBeSpawned = new HashSet<MmPoiData>();

            //Why? I don't know! Please help!
            topRightTile16.x++;
            bottomLeftTile16.y++;

            for (int x = bottomLeftTile16.x; x < topRightTile16.x; x++)
            {
                for (int y = topRightTile16.y; y < bottomLeftTile16.y; y++)
                {
                    var pois = _poiHolder.Get(new Vector2Int(x, y));
                    if (pois == null) continue;

                    shouldBeSpawned.AddRange(pois);
                }
            }

            var toDespawn = LinqUtility.ToHashSet(_spawnedPois.Keys.Where(p => !shouldBeSpawned.Contains(p)));
            var toSpawn = LinqUtility.ToHashSet(shouldBeSpawned.Where(p => !_spawnedPois.Keys.Contains(p)));

            foreach (var poi in toDespawn)
            {
                DespawnPoi(poi);
                _spawnedPois.Remove(poi);
            }

            // Spawn logic
            foreach (var poi in toSpawn)
            {
                MiniMapPOI miniMapPoi = SpawnPoi(poi);
                _spawnedPois.Add(poi, miniMapPoi);
            }
        }

        private readonly Dictionary<MmPoiData, MiniMapPOI> _spawnedPois = new Dictionary<MmPoiData, MiniMapPOI>();


        private void DespawnPoi(MmPoiData poiData)
        {
            MiniMapPOI miniMapPoi = _spawnedPois[poiData];
            pools.Return(miniMapPoi);
            _spawnedPois.Remove(poiData);
        }

        private MiniMapPOI SpawnPoi(MmPoiData poiData)
        {
            MiniMapPOI poi = pools.GetPoi(poiData.PoiCoordinates, poiData.Sprite, poiData.Message, poiData.Type);
            // MiniMapPOI poi = Instantiate(poiPrefab, _mapRectTransform);
            poi.Position = poiData.PoiCoordinates;
            poi.Setup(poiData.Sprite, poiData.Message, poiData.Type);

            return poi;
        }

        void RenderTiles()
        {
            int resolution = imageTiler.GetResolution(CurrentZoom);

            _currentTileSize = (tileSize * resolution); // * (tileSize * resolution);
            _currentMaxTiles = (maxTiles / resolution);

            Vector2Int centerTile = GetTileAtPosition(LocalPos);

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
        }

        public MmPoiData AddPoi(Vector3 inWorldPos, PoiType poiType, String message, Sprite sprite)
        {
            return AddPoi(CoordinateUtils.ToUiCoords(inWorldPos), poiType, message, sprite);
        }

        public MmPoiData AddPoi(Vector2 poiCoordinates, PoiType poiType, String message, Sprite sprite)
        {
            // Convert POI coordinates to map's local coordinates
            Vector2 localPos = ConvertCoordinatesToLocalPosition(poiCoordinates);

            Vector2Int tilePos = GetTileAtPosition(localPos);

            MmPoiData mmPoiData = new MmPoiData(localPos, poiType, sprite, message);

            _poiHolder.Add(tilePos, mmPoiData);
            return mmPoiData;
        }


        public void RemovePoi(MmPoiData poiData)
        {
            _poiHolder.Remove(poiData);
        }

        public Vector2 ConvertCoordinatesToLocalPosition(Vector3 inWorldPos)
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
            float halfTileSize = (_currentTileSize * 0.5f * CurrentZoom);

            // Calculate the maximum allowed positions, including the adjusted offset
            float maxX = (canvasSize.x - mapSize.x) / 2f + halfTileSize;
            float maxY = (canvasSize.y - mapSize.y) / 2f + halfTileSize;

            // Ensure that the map is at least as big as the canvas
            maxX = Mathf.Min(maxX, halfTileSize);

            // Get the current position of the map
            Vector2 currentPosition = _mapRectTransform.anchoredPosition;

            // Clamp the map's position
            float clampedX = Mathf.Clamp(currentPosition.x, maxX, -maxX + _currentTileSize * CurrentZoom);
            float clampedY = Mathf.Clamp(currentPosition.y, maxY, -maxY + _currentTileSize * CurrentZoom);

            float offset = _currentMaxTiles switch
            {
                8 => -128 * CurrentZoom,
                4 => -384 * CurrentZoom,
                _ => 0
            };
            if (offset != 0)
            {
                clampedX = Mathf.Clamp(currentPosition.x, maxX + offset,
                    -maxX + offset + _currentTileSize * CurrentZoom);
                clampedY = Mathf.Clamp(currentPosition.y, maxY + offset,
                    -maxY + offset + _currentTileSize * CurrentZoom);
            }

            // Apply the clamped position with adjusted offset
            _mapRectTransform.anchoredPosition = new Vector2(clampedX, clampedY);
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

        public void OnDrag(PointerEventData eventData)
        {
            _mapRectTransform.anchoredPosition += eventData.delta;
            UpdateMap();
        }

        private int _lastRes = 9993;

        public void OnScroll(PointerEventData eventData)
        {
            Vector2 cursorPosition = eventData.position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_mapRectTransform, cursorPosition,
                eventData.pressEventCamera, out Vector2 localCursor);

            float scroll = eventData.scrollDelta.y;
            Vector3 oldScale = _mapRectTransform.localScale;
            Vector3 newScale = oldScale + Vector3.one * scroll * _zoomSpeed;
            newScale = new Vector3(
                Mathf.Clamp(newScale.x, _originalScale.x * _minZoom, _originalScale.x * _maxZoom),
                Mathf.Clamp(newScale.y, _originalScale.y * _minZoom, _originalScale.y * _maxZoom), 1
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

            UpdateMap();
        }
        
        private void SnapToPlayer()
        {
            var playerPoiPos = _playerPoi.rectTransform.localPosition;
            playerPoiPos.x = -playerPoiPos.x;
            playerPoiPos.y = -playerPoiPos.y;
            
            playerPoiPos.x *= CurrentZoom;
            playerPoiPos.y *= CurrentZoom;
            
            
            _mapRectTransform.localPosition = new Vector3(playerPoiPos.x, playerPoiPos.y, 0);
        }

        //Called on player move
        public void OnEventRaised()
        {
            UpdatePlayerPoiPosition();
            SnapToPlayer();
        }
    }
}