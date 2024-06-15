using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ValvulaRotar : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Vector2 initialPosition;
    private Vector2 initialDirection;
    private bool isRotating;
    private float totalAngleRotated = 0f;
    private float lastAngle = 0f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initialPosition = rectTransform.position;
        initialDirection = Vector2.right;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isRotating = true;
            Vector2 screenPos = eventData.position;
            float angle = Mathf.Atan2(screenPos.y - initialPosition.y, screenPos.x - initialPosition.x) * Mathf.Rad2Deg;
            lastAngle = angle; // No necesitamos usar Mathf.Repeat para el ángulo inicial
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isRotating)
        {
            Vector2 currentMousePosition = eventData.position;
            float angle = Mathf.Atan2(currentMousePosition.y - initialPosition.y, currentMousePosition.x - initialPosition.x) * Mathf.Rad2Deg;
            float angleOffset = angle - lastAngle;

            if (angleOffset > 180f)
            {
                angleOffset -= 360f;
            }
            else if (angleOffset < -180f)
            {
                angleOffset += 360f;
            }

            rectTransform.rotation = Quaternion.Euler(0, 0, rectTransform.eulerAngles.z + angleOffset);
            totalAngleRotated += angleOffset;
            lastAngle = angle;
            Debug.Log("Ángulo girado: " + totalAngleRotated);

            if (Mathf.Abs(totalAngleRotated) >= 720f)
            {
                Debug.Log("Has abierto la puerta");
                isRotating = false;
                MostrarPuzleValvula.Instance.GiroCompletado();
            }
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
