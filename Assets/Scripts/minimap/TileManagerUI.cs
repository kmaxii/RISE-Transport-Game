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


        /*
        private void FixedUpdate()
        {
            RaycastUI();
            var localPosition = _mapRectTransform.localPosition;
            Debug.Log("Math mid: " + CalculateTileCoordinates(new Vector2(localPosition.x,
                localPosition.y)));
        }
        */

        private Vector2 LocalPos
        {
            get
            {
                var localPosition = _mapRectTransform.localPosition;

                return new Vector2(localPosition.x,
                    localPosition.y);
            }
        }

        public Vector2Int CalculateTileCoordinates(Vector2 position)
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
            UpdateVisibleTiles();

            _playerPoi = AddPoi(player.position, PoiType.Player, "You", playerSprite);
        }

        private void Update()
        {
            UpdateVisibleTiles();
            UpdatePlayerPoiPosition();
        }


        private void UpdateVisibleTiles()
        {
            Vector2Int middlePos = CalculateTileCoordinates(LocalPos);
            
            if (_tiles[middlePos.x, middlePos.y] == null)
            {
                var tileImage = pools.GetTile(Vector2.zero, imageTiler.GetSprite(middlePos.x, middlePos.y, 0));
                _tiles[middlePos.x, middlePos.y] = tileImage;
                float centerX = (maxTiles * tileSize) / 2f;
                float centerY = (maxTiles * tileSize) / 2f;

                tileImage.rectTransform.anchoredPosition =
                    new Vector2((middlePos.x * tileSize) - centerX, ((maxTiles - 1 - middlePos.y) * tileSize) - centerY);
                tileImage.rectTransform.sizeDelta = new Vector2(256,256);
                tileImage.transform.localScale = new Vector3(1, 1, 1);

            }
        }

        void CreateTile(int x, int y)
        {
            // Texture2D tileTexture = ImageTiler.GetTileTexture(x, y);
            Sprite tileTexture = imageTiler.GetSprite(x, y, 0);
            Image tileImage = new GameObject("Tile_" + x + "_" + y).AddComponent<Image>();

            tileImage.sprite = tileTexture;
            tileImage.rectTransform.SetParent(_mapRectTransform, false);

            // Calculate the offset to center the map
            float centerX = (maxTiles * tileSize) / 2f;
            float centerY = (maxTiles * tileSize) / 2f;

            tileImage.rectTransform.anchoredPosition =
                new Vector2((x * tileSize) - centerX, ((maxTiles - 1 - y) * tileSize) - centerY);
            tileImage.rectTransform.sizeDelta = new Vector2(tileSize, tileSize);

            _tiles[x, y] = tileImage;
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


        /**
         *         void RaycastUI()
        {
            // Create a new PointerEventData
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);

            // Set the position to the center of the screen
            pointerEventData.position = new Vector2(Screen.width / 2, Screen.height / 2);

            // Create a list to receive the results
            List<RaycastResult> results = new List<RaycastResult>();

            // Raycast using the GraphicsRaycaster and mouse click position
            EventSystem.current.RaycastAll(pointerEventData, results);

            // Check if the raycast hit any UI elements
            if (results.Count > 0)
            {
                // Print the name of the first UI element hit
                Debug.Log("RayCast Hit: " + results[0].gameObject.name);
            }
        }
         */
    }
}