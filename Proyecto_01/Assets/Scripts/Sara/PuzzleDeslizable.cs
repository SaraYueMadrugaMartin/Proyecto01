using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PuzzleDeslizable : MonoBehaviour
{
    [SerializeField] private RectTransform espacioVacio = null;
    [SerializeField] private TilesScript[] tiles;
    [SerializeField] private Vector2[] posicionesCorrectas;

    private Camera camara;
    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;

    private List<bool> posicionPiezaCorrecta;

    void Start()
    {
        camara = Camera.main;
        raycaster = FindObjectOfType<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();
        posicionPiezaCorrecta = new List<bool>();
    }

    void Update()
    {
        MoverPieza();
    }

    public void MoverPieza()
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

                        if (Vector2.Distance(espacioVacio.anchoredPosition, tileRect.anchoredPosition) < 300)
                        {
                            Vector2 ultimaPosicionEspacioVacio = espacioVacio.anchoredPosition;
                            espacioVacio.anchoredPosition = thisTile.targetPosition;
                            thisTile.targetPosition = ultimaPosicionEspacioVacio;
                            AcertarPosicionPieza();
                            break;
                        }
                    }
                }
            }
        }
    }

    public void AcertarPosicionPieza()
    {
        posicionPiezaCorrecta.Clear();

        for (int i = 0; i < tiles.Length; i++)
        {
            bool esCorrecta = false;

            for (int j = 0; j < posicionesCorrectas.Length; j++)
            {
                if (Vector2.Distance(posicionesCorrectas[j], tiles[i].targetPosition) < 0.01f)
                {
                    esCorrecta = true;
                    Debug.Log("La pieza: " + tiles[i].name + " está en su posición correcta.");
                    break;
                }
                else
                {
                    Debug.Log("Esta no la posición correcta de la pieza: " + tiles[i].name);
                }
            }
            posicionPiezaCorrecta.Add(esCorrecta);
        }
        if (posicionPiezaCorrecta.TrueForAll(posicion => posicion))
        {
            Debug.Log("Has resuelto el puzzle.");
        }
    }
}