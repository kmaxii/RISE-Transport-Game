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
            set => rectTransform.localPosition = value;
            get => rectTransform.localPosition;
        }

        private Vector2 _initialLocalPos;

        public void AdjustWithinBounds(RectTransform grandParent, RectTransform canvas)
        {
            Vector2 adjustedPosition = grandParent.TransformPoint(_initialLocalPos);

           // Debug.Log(Position);
            // Get the corners of the canvas
            Vector3[] corners = new Vector3[4];
            canvas.GetWorldCorners(corners);
            
            // Calculate canvas width and height
            float canvasWidth = Vector3.Distance(corners[0], corners[3]);
            float canvasHeight = Vector3.Distance(corners[0], corners[1]);

            Vector2 bottomLeft = corners[0];

            Vector2 savedAdjustedPosition = adjustedPosition;
            // Adjusting the position by considering the canvas pinned to the top left
            // We use the bottom left corner as the reference for the X and Y min values
            adjustedPosition.x = Mathf.Clamp(adjustedPosition.x, bottomLeft.x + 25, bottomLeft.x + canvasWidth - 25);
            adjustedPosition.y = Mathf.Clamp(adjustedPosition.y, bottomLeft.y + 25, bottomLeft.y + canvasHeight - 25);

            
            // If the position was adjusted, we need to update the local position
            image.color = adjustedPosition != savedAdjustedPosition ? Color.gray : Color.white;
            
            
            
            rectTransform.position = adjustedPosition;
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
            _initialLocalPos = Position;
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