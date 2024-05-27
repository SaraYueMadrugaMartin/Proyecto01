using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PuzzleDeslizable : MonoBehaviour
{
    [SerializeField] private RectTransform espacioVacio = null;
    [SerializeField] private TilesScript[] tiles;

    private Camera camara;
    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;

    void Start()
    {
        camara = Camera.main;
        raycaster = FindObjectOfType<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);

            if (results.Count > 0)
            {
                foreach (RaycastResult result in results)
                {
                    TilesScript thisTile = result.gameObject.GetComponent<TilesScript>();
                    if (thisTile != null)
                    {
                        RectTransform tileRect = thisTile.GetComponent<RectTransform>();

                        if (Vector2.Distance(espacioVacio.anchoredPosition, tileRect.anchoredPosition) < 1000)
                        {
                            Vector2 ultimaPosicionEspacioVacio = espacioVacio.anchoredPosition;
                            espacioVacio.anchoredPosition = thisTile.targetPosition;
                            thisTile.targetPosition = ultimaPosicionEspacioVacio;
                            break;
                        }
                    }
                }
            }
        }
    }

    public void Mezclar()
    {

    }
}
