using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace minimap
{
    public class MiniMapPOI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private Image image; 
        public TextMeshProUGUI textMesh; 

        [Tooltip("Scale factor for when the mouse hovers over")]
        public float hoverScale = 1.2f; 
        private Vector3 _originalScale;

        public RectTransform rectTransform;

        private PoiType _poiType;

        public PoiType PoiType => _poiType;


        public Vector2 Position
        {
            set => rectTransform.anchoredPosition = value;
        }
        


        private void Start()
        {
            _originalScale = image.rectTransform.localScale;
            ShowText(false); // Initially hide the text
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // When the mouse hovers over, enlarge the image and show the text
            image.rectTransform.localScale = _originalScale * hoverScale;
            ShowText(true);
            transform.SetAsLastSibling();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // When the mouse exits, reset the image size and hide the text
            image.rectTransform.localScale = _originalScale;
            ShowText(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            FullScreenMap.Instance.ClickedPoi(this);
        }

        public string GetText()
        {
            return textMesh.text;
        }

        public void Setup(Sprite sprite, String text, PoiType poiType)
        {
            _poiType = poiType;
            SetText(text);
            SetSprite(sprite);
        }


        private void SetText(String text)
        {
            textMesh.text = text;
        }

        private void SetSprite(Sprite sprite)
        {
            image.sprite = sprite;
        }

        private void ShowText(bool value)
        {
            textMesh.gameObject.SetActive(value);
        }
    }
}