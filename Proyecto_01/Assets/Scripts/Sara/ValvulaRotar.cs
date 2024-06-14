using UnityEngine;
using UnityEngine.EventSystems;

public class ValvulaRotar : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Vector2 screenPos;
    private float startAngle;
    private bool isRotating;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isRotating = true;
            screenPos = eventData.position;
            startAngle = Mathf.Atan2(screenPos.y - rectTransform.position.y, screenPos.x - rectTransform.position.x) * Mathf.Rad2Deg;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isRotating)
        {
            Vector2 currentMousePosition = eventData.position;
            float angle = Mathf.Atan2(currentMousePosition.y - rectTransform.position.y, currentMousePosition.x - rectTransform.position.x) * Mathf.Rad2Deg;
            float angleOffset = angle - startAngle;

            rectTransform.rotation = Quaternion.Euler(0, 0, angleOffset);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isRotating = false;
        }
    }
}
