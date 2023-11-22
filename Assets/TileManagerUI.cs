using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileManagerUI : MonoBehaviour, IDragHandler, IScrollHandler
{
    public RectTransform mapRectTransform;
    public float zoomSpeed = 0.5f;
    public float maxZoom = 5f;
    public float minZoom = 1f;
    public int tileSize = 256;
    public int maxTiles = 21;

    private RawImage[,] tiles;
    private Vector3 originalScale;

    private void Awake()
    {
        if (mapRectTransform == null)
        {
            mapRectTransform = GetComponent<RectTransform>();
        }

        originalScale = mapRectTransform.localScale;

        tiles = new RawImage[maxTiles, maxTiles];
        UpdateVisibleTiles();
        
       // AddPOI(CoordinateUtils.ConvertLatLongToGameCoords(57.70755463163524d, 11.973603733465872d));
    }

    
    public GameObject poiPrefab; // Assign this in the Unity Editor

    public void AddPOI(Vector2 poiCoordinates)
    {
        // Convert POI coordinates to map's local coordinates
        Vector2 localPos = ConvertCoordinatesToLocalPosition(poiCoordinates);

        // Instantiate the POI prefab and position it
        GameObject poi = Instantiate(poiPrefab, mapRectTransform);
        poi.GetComponent<RectTransform>().anchoredPosition = localPos;
    }

    private Vector2 ConvertCoordinatesToLocalPosition(Vector2 poiCoordinates)
    {
        // Assuming the total size of your map is 10752x10752 pixels
        float mapSize = 10752f;

        // Convert global map coordinates to local UI coordinates
        float localX = (poiCoordinates.x - mapSize / 2f) / mapSize * (tileSize * maxTiles);
        float localY = (mapSize / 2f - poiCoordinates.y) / mapSize * (tileSize * maxTiles);

        return new Vector2(localX, localY);
    }
    
    
    private void Update()
    {
        UpdateVisibleTiles();
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
            Mathf.Clamp(newScale.x, originalScale.x * minZoom, originalScale.x * maxZoom),
            Mathf.Clamp(newScale.y, originalScale.y * minZoom, originalScale.y * maxZoom),
            Mathf.Clamp(newScale.z, originalScale.z * minZoom, originalScale.z * maxZoom)
        );

        // Calculate the ratio of change in scale
        Vector3 scaleRatioChange = newScale - oldScale;
        Vector2 adjustedCursorPos = new Vector2(localCursor.x * scaleRatioChange.x, localCursor.y * scaleRatioChange.y);

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
                    if (tiles[x, y] == null)
                    {
                        CreateTile(x, y);
                    }
                }
                else
                {
                    if (tiles[x, y] != null)
                    {
                        DestroyTile(x, y);
                    }
                }
            }
        }
    }

    bool TileShouldBeVisible(int x, int y)
    {
        // Logic to determine if a tile should be visible
        // Replace with your own logic based on the zoom level and position

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

        tiles[x, y] = tileImage;
    }

    void DestroyTile(int x, int y)
    {
        Destroy(tiles[x, y].gameObject);
        tiles[x, y] = null;
    }
}