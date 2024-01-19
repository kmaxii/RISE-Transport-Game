using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform background;
    public RectTransform handle;
    private Vector2 _inputVector;

    private void Start()
    {
        if (background == null || handle == null)
        {
            Debug.LogError("Joystick background and handle have not been assigned!");
            return;
        }

        // Initialize joystick in the center
        handle.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, background.position);
        Vector2 radius = background.sizeDelta / 2;
        _inputVector = (eventData.position - position) / (radius);
        
        // Clamp joystick within its background
        _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;
        
        // Move joystick handle
        handle.anchoredPosition = new Vector2(_inputVector.x * radius.x, _inputVector.y * radius.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    public float Horizontal()
    {
        return _inputVector.x;
    }

    public float Vertical()
    {
        return _inputVector.y;
    }
}