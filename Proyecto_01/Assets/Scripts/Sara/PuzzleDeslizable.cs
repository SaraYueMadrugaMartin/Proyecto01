using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PuzzleDeslizable : MonoBehaviour
{
    [SerializeField] private RectTransform espacioVacio = null;
    [SerializeField] private TilesScript[] tiles;
    //[SerializeField] private Vector2[] posicionesCorrectas;

    private Camera camara;
    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;
    private MostrarPuzzle01 mostrarPuzzle01;
    //private List<bool> posicionPiezaCorrecta;

    public int piezasEncajadas;
    public bool puzzle01Resuelto;

    //private int contador;

    void Start()
    {
        camara = Camera.main;
        raycaster = FindObjectOfType<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();
        mostrarPuzzle01 = FindObjectOfType<MostrarPuzzle01>();
        //posicionPiezaCorrecta = new List<bool>();
        piezasEncajadas = 0;
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

                        if (Vector2.Distance(espacioVacio.anchoredPosition, tileRect.anchoredPosition) < 150)
                        {
                            Vector2 ultimaPosicionEspacioVacio = espacioVacio.anchoredPosition;
                            espacioVacio.anchoredPosition = thisTile.posPiezaInicial;
                            thisTile.posPiezaInicial = ultimaPosicionEspacioVacio;
                            break;
                        }
                    }
                }
            }
        }
    }

    public void TodasPiezasCorrectas()
    {
        bool unaVez = true;
        if(unaVez)
        {
            Debug.Log("Has ganado");
            puzzle01Resuelto = true;
            mostrarPuzzle01.PuzzleResuelto();
            unaVez = false;
        }        
    }
}
