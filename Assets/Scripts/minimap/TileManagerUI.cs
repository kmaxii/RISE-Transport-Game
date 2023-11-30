using System;
using Mapbox.Unity.Map;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace minimap
{
    public class TileManagerUI : MonoBehaviour, IDragHandler, IScrollHandler
    {
        public RectTransform mapRectTransform;
        public float zoomSpeed = 0.5f;
        public float maxZoom = 5f;
        public float minZoom = 1f;
        public int tileSize = 256;
        public int maxTiles = 21;

        private RawImage[,] _tiles;
        private Vector3 _originalScale;

        [FormerlySerializedAs("_map")] [SerializeField]
        private AbstractMap map;

        [SerializeField] private Transform player;

        [SerializeField] private Sprite bussStationSprite;
        [SerializeField] private Sprite eScooterSprite;
        private MiniMapPOI _playerPoi;

        private void Awake()
        {
            if (mapRectTransform == null)
            {
                mapRectTransform = GetComponent<RectTransform>();
            }

            _originalScale = mapRectTransform.localScale;

            _tiles = new RawImage[maxTiles, maxTiles];
            UpdateVisibleTiles();

            _playerPoi = AddPoi(player.position, PoiType.Player, "You");
        }


        [SerializeField] MiniMapPOI poiPrefab; // Assign this in the Unity Editor
        [SerializeField] MiniMapPOI bussPoiPrefab; // Assign this in the Unity Editor
        [SerializeField] MiniMapPOI playerPoiPrefab; // Assign this in the Unity Editor


        public MiniMapPOI AddPoi(Vector3 inWorldPos, PoiType poiType, String message)
        {
            return AddPoi(CoordinateUtils.ToUiCoords(inWorldPos), poiType, message);
        }

        public MiniMapPOI AddPoi(Vector2 poiCoordinates, PoiType poiType, String message)
        {
            // Convert POI coordinates to map's local coordinates
            Vector2 localPos = ConvertCoordinatesToLocalPosition(poiCoordinates);

            MiniMapPOI poi;
            switch (poiType)
            {
                case PoiType.BussStation:
                    poi = Instantiate(bussPoiPrefab, mapRectTransform);
                    break;
                case PoiType.Player:
                    poi = Instantiate(playerPoiPrefab, mapRectTransform);
                    break;
                case PoiType.Mission:
                    poi = Instantiate(poiPrefab, mapRectTransform);
                    break;
                default:
                    poi = Instantiate(poiPrefab, mapRectTransform);
                    break;
            }

            poi.Position = localPos;
            poi.Setup(message);

            return poi;
        }

        private Vector2 ConvertCoordinatesToLocalPosition(Vector3 inWorldPos)
        {
            Vector2 vector2 = CoordinateUtils.ToUiCoords(inWorldPos);
            return ConvertCoordinatesToLocalPosition(vector2);
        }

        private Vector2 ConvertCoordinatesToLocalPosition(Vector2 poiCoordinates)
        {
            // Assuming the total size of your map is 10752x10752 pixels
            float mapSize = CoordinateUtils.MapSize;

            float localX = (poiCoordinates.x - mapSize / 2f) / mapSize * (tileSize * maxTiles);
            float localY = (poiCoordinates.y - mapSize / 2f) / mapSize * (tileSize * maxTiles);

            return new Vector2(localX - 141.5f, localY - 101);
        }


        private void Update()
        {
            UpdateVisibleTiles();
            UpdatePlayerPoiPosition();
        }


        private void UpdatePlayerPoiPosition()
        {
            _playerPoi.Position = ConvertCoordinatesToLocalPosition(player.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            mapRectTransform.anchoredPosition += eventData.delta;
        }

        public void OnScroll(PointerEventData eventData)
        {
            Vector2 cursorPosition = eventData.position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(mapRectTransform, cursorPosition,
                eventData.pressEventCamera, out Vector2 localCursor);

            float scroll = eventData.scrollDelta.y;
            Vector3 oldScale = mapRectTransform.localScale;
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
            Vector2 newPosition = mapRectTransform.anchoredPosition - adjustedCursorPos;

            // Apply the new scale and adjusted position
            mapRectTransform.localScale = newScale;
            mapRectTransform.anchoredPosition = newPosition;
        }


        void UpdateVisibleTiles()
        {
            for (int x = 0; x < maxTiles; x++)
            {
                for (int y = 0; y < maxTiles; y++)
                {
                    if (TileShouldBeVisible(x, y))
                    {
                        if (_tiles[x, y] == null)
                        {
                            CreateTile(x, y);
                        }
                    }
                    else
                    {
                        if (_tiles[x, y] != null)
                        {
                            DestroyTile(x, y);
                        }
                    }
                }
            }
        }

        bool TileShouldBeVisible(int x, int y)
        {
            //TODO Logic to determine if a tile should be visible

            return true; // Placeholder
        }

        void CreateTile(int x, int y)
        {
            Texture2D tileTexture = ImageTiler.GetTileTexture(x, y);
            RawImage tileImage = new GameObject("Tile_" + x + "_" + y).AddComponent<RawImage>();


            tileImage.texture = tileTexture;
            tileImage.rectTransform.SetParent(mapRectTransform, false);

            // Calculate the offset to center the map
            float centerX = (maxTiles * tileSize) / 2f;
            float centerY = (maxTiles * tileSize) / 2f;

            tileImage.rectTransform.anchoredPosition =
                new Vector2((x * tileSize) - centerX, ((maxTiles - 1 - y) * tileSize) - centerY);
            tileImage.rectTransform.sizeDelta = new Vector2(tileSize, tileSize);

            _tiles[x, y] = tileImage;
        }

        void DestroyTile(int x, int y)
        {
            Destroy(_tiles[x, y].gameObject);
            _tiles[x, y] = null;
        }
    }
}