using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace minimap
{
    public class TileManagerUI : MonoBehaviour, IDragHandler, IScrollHandler
    {
        private RectTransform _mapRectTransform;
        [SerializeField] private float zoomSpeed = 0.5f;
        [SerializeField] private float maxZoom = 5f;
        [SerializeField] private float minZoom = 0.35f;
        [SerializeField] private int tileSize = 256;
        [SerializeField] private int maxTiles = 21;

        [SerializeField] private ImageTiler imageTiler;

        private Image[,] _tiles;
        private Vector3 _originalScale;

        [SerializeField] private Transform player;

        private MiniMapPOI _playerPoi;

        [SerializeField] MiniMapPOI poiPrefab;
        private float CurrentZoom => _mapRectTransform.localScale.x;

        [SerializeField] private Sprite playerSprite;

        [SerializeField] private MapPools pools;
        
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

            _playerPoi = AddPoi(player.position, PoiType.Player, "You", playerSprite);
        }

        private void Update()
        {
            UpdatePlayerPoiPosition();
            RenderTiles();
        }
        public RectTransform canvasRectTransform;

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

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    CreateTile(x, y,  0);
                }
            }
        }

        void CreateTile(int x, int y, int resolution)
        {
            if (_tiles[x, y] == null)
            {
                var tileImage = pools.GetTile(imageTiler.GetSprite(x, y, resolution));
                _tiles[x, y] = tileImage;
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
                Mathf.Clamp(newScale.y, _originalScale.y * minZoom, _originalScale.y * maxZoom),
                Mathf.Clamp(newScale.z, _originalScale.z * minZoom, _originalScale.z * maxZoom)
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