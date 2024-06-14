using UnityEngine;
using UnityEngine.EventSystems;

public class ValvulaMover : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform posValvula;
    private Vector2 posInicial;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        posInicial = rectTransform.anchoredPosition;
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
                MostrarPuzleValvula.Instance.ColocarCuerpo();
            }
            else if (gameObject.CompareTag("ValvulaCabeza"))
            {
                if (!MostrarPuzleValvula.Instance.cuerpoColocado)
                {
                    rectTransform.anchoredPosition = posInicial;
                    Debug.Log("Antes de poner esta pieza necesitas poner la otra");
                }
                else
                {
                    rectTransform.anchoredPosition = posValvula.anchoredPosition;
                    gameObject.SetActive(false);
                    MostrarPuzleValvula.Instance.ColocarCabeza();
                }
            }
        }
        else
        {
            rectTransform.anchoredPosition = posInicial;
        }
    }
}
