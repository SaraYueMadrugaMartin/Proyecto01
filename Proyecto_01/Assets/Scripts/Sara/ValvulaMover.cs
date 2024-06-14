using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ValvulaMover : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform posValvula;
    private Vector2 posInicial;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private ValvulaRotar rotarValvula;
    [SerializeField] private GameObject panelSiguientePaso;

    private static bool cabezaColocada = false;
    private static bool cuerpoColocado = false;
    //private static int contador = 0; // Lo pongo en estático para que ambas piezas compartan la variable, sino no suma en ambas.

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        rotarValvula = GetComponent<ValvulaRotar>();

        posInicial = rectTransform.anchoredPosition;
        panelSiguientePaso.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            rectTransform.anchoredPosition = localPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true;
        }

        if (Vector2.Distance(rectTransform.anchoredPosition, posValvula.anchoredPosition) <= 20f)
        {
            if (gameObject.CompareTag("ValvulaCuerpo"))
            {
                rectTransform.anchoredPosition = posValvula.anchoredPosition;
                gameObject.SetActive(false);
                cuerpoColocado = true;
                cabezaColocada = false;
                //contador++;
                //Debug.Log(contador);
            }

            if (!cuerpoColocado)
            {
                rectTransform.anchoredPosition = posInicial;
                Debug.Log("Antes de poner esta pieza necesitas poner la otra");
            }
            else if(cuerpoColocado)
            {
                rectTransform.anchoredPosition = posValvula.anchoredPosition;
                cabezaColocada = true;
            }
        }
        else
        {
            rectTransform.anchoredPosition = posInicial;
        }

        if(cabezaColocada && cuerpoColocado)
        {
            // Sonido de piezas encajando
            panelSiguientePaso.SetActive(true);
            StartCoroutine(SiguientePaso());
        }
    }

    IEnumerator SiguientePaso()
    {
        yield return new WaitForSecondsRealtime(5f);
        panelSiguientePaso.SetActive(false);
        //rotarValvula.IniciarRotacion();
    }
}