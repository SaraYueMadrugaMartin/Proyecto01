using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PuzzleDeslizable : MonoBehaviour
{
    [SerializeField] private RectTransform espacioVacio = null;
    [SerializeField] public TilesScript[] tiles;

    //private TilesScript tile;
    //[SerializeField] private Vector2[] posicionesCorrectas;

    [SerializeField] private GameObject canvas;
    private GraphicRaycaster raycaster;
    [SerializeField] private GameObject eventSystemGO;
    private EventSystem eventSystem;
    [SerializeField] private GameObject mostrarPuzzle;
    private MostrarPuzzle01 mostrarPuzzle01;
    //private List<bool> posicionPiezaCorrecta;

    public int piezasEncajadas;
    public bool puzzle01Resuelto;
    public bool puzzleResuelto;

    //private int contador;

    void Start()
    {
        raycaster = canvas.GetComponent<GraphicRaycaster>();
        eventSystem = eventSystemGO.GetComponent<EventSystem>();
        mostrarPuzzle01 = mostrarPuzzle.GetComponent<MostrarPuzzle01>();
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
        if (!puzzleResuelto)
        {
            Debug.Log("Has ganado");
            puzzleResuelto = true;
            mostrarPuzzle01.PuzzleResuelto();
        }
    }
}
