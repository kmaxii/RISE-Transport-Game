using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FulScreenMapController : MonoBehaviour, IDragHandler, IScrollHandler
{
    private RectTransform _mapRectTransform;
    [SerializeField] private float zoomSpeed = 0.5f;
    [SerializeField] private float maxZoom = 5f;
    [SerializeField] private float minZoom = 1f;

    private Vector3 _originalScale;

    private void Start()
    {
        if (_mapRectTransform == null)
        {
            _mapRectTransform = GetComponent<RectTransform>();
        }
        _originalScale = _mapRectTransform.localScale;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _mapRectTransform.anchoredPosition += eventData.delta;
    }

    public void OnScroll(PointerEventData eventData)
    {
        float scroll = eventData.scrollDelta.y;
        Vector3 newScale = _mapRectTransform.localScale + Vector3.one * scroll * zoomSpeed;
        newScale = new Vector3(
            Mathf.Clamp(newScale.x, _originalScale.x * minZoom, _originalScale.x * maxZoom),
            Mathf.Clamp(newScale.y, _originalScale.y * minZoom, _originalScale.y * maxZoom),
            Mathf.Clamp(newScale.z, _originalScale.z * minZoom, _originalScale.z * maxZoom)
        );

        _mapRectTransform.localScale = newScale;
    }
}