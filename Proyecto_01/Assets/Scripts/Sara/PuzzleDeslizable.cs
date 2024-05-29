using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class PuzzleDeslizable : MonoBehaviour
{
    [SerializeField] private RectTransform espacioVacio = null;
    [SerializeField] private TilesScript[] tiles;
    //[SerializeField] private Vector2[] posicionesCorrectas;

    private Camera camara;
    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;
    private MostrarPuzzle01 mostrarPuzzle01;
    private List<bool> posicionPiezaCorrecta;

    public int piezasEncajadas;

    //private int contador;

    void Start()
    {
        camara = Camera.main;
        raycaster = FindObjectOfType<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();
        mostrarPuzzle01 = FindObjectOfType<MostrarPuzzle01>();
        posicionPiezaCorrecta = new List<bool>();
        piezasEncajadas = 0;

        //contador = 0;

        for (int i = 0; i < tiles.Length; i++)
        {
            posicionPiezaCorrecta.Add(false);
        }
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
            TodasPiezasCorrectas();

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
                            espacioVacio.anchoredPosition = thisTile.targetPosition;
                            thisTile.targetPosition = ultimaPosicionEspacioVacio;
                            break;
                        }
                    }
                }
            }
        }
    }

    public void TodasPiezasCorrectas() // Dónde pongo este método, si lo pongo en el Update, el panel Fade me sale todo el rato. Si lo pongo en MoverPieza, solo comprueba al dar un click de más al colocar la última pieza.
    {
        if (piezasEncajadas == tiles.Length)
        {
            Debug.Log("Has ganado");
            mostrarPuzzle01.PuzzleResuelto();
        }
    }
}
