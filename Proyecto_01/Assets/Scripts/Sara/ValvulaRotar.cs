using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ValvulaRotar : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Vector2 posInicial;
    private Vector2 direccionGiro;
    private bool estaGirando;
    private float anguloTotalGirado = 0f;
    private float ultimoAngulo = 0f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        posInicial = rectTransform.position;
        direccionGiro = Vector2.right;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            estaGirando = true;
            Vector2 screenPos = eventData.position;
            float angle = Mathf.Atan2(screenPos.y - posInicial.y, screenPos.x - posInicial.x) * Mathf.Rad2Deg;
            ultimoAngulo = angle;
        }
    }

    // Empezamos a girar al hacer click, ese primer click se considera el ángulo 0 para empezar a girar
    public void OnDrag(PointerEventData eventData)
    {
        if (estaGirando)
        {
            Vector2 currentMousePosition = eventData.position;
            float angle = Mathf.Atan2(currentMousePosition.y - posInicial.y, currentMousePosition.x - posInicial.x) * Mathf.Rad2Deg;
            float angleOffset = angle - ultimoAngulo;

            if (angleOffset > 180f)
            {
                angleOffset -= 360f;
            }
            else if (angleOffset < -180f)
            {
                angleOffset += 360f;
            }

            rectTransform.rotation = Quaternion.Euler(0, 0, rectTransform.eulerAngles.z + angleOffset);
            anguloTotalGirado += angleOffset;
            ultimoAngulo = angle;
            Debug.Log("Ángulo girado: " + anguloTotalGirado);

            if (Mathf.Abs(anguloTotalGirado) >= 720f)
            {
                Debug.Log("Has abierto la puerta");
                estaGirando = false;
                MostrarPuzleValvula.Instance.GiroCompletado();
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            estaGirando = false;
        }
    }
}
