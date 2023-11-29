using System;
using minimap;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniMapPOI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public RectTransform imageTransform; // Assign the RectTransform of the POI image
    public TextMeshProUGUI textMesh; // Assign the TextMeshPro component

    public float hoverScale = 1.2f; // Scale factor for when the mouse hovers over
    private Vector3 _originalScale;


    private RectTransform _rectTransform;

    public Vector2 Position
    {
        set => _rectTransform.anchoredPosition = value;
    }

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public string GetText()
    {
        return textMesh.text;
    }
    
    public void Setup(Sprite sprite, String text)
    {
        textMesh.text = text;

       // imageTransform.GetComponent<Image>().sprite = sprite;
    }

    private void Start()
    {
        _originalScale = imageTransform.localScale;
        textMesh.gameObject.SetActive(false); // Initially hide the text
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // When the mouse hovers over, enlarge the image and show the text
        imageTransform.localScale = _originalScale * hoverScale;
        textMesh.gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // When the mouse exits, reset the image size and hide the text
        imageTransform.localScale = _originalScale;
        textMesh.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FullScreenMap.Instance.ClickedPoi(this);
    }
}