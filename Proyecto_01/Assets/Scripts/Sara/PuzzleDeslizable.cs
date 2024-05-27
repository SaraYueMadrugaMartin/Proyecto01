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
    private MostrarPuzzle01 mostrarPuzzle01;
    private List<bool> posicionPiezaCorrecta;

    private int contador;

    void Start()
    {
        camara = Camera.main;
        raycaster = FindObjectOfType<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();
        mostrarPuzzle01 = FindObjectOfType<MostrarPuzzle01>();
        posicionPiezaCorrecta = new List<bool>();

        contador = 0;

        for (int i = 0; i < tiles.Length; i++)
        {
            posicionPiezaCorrecta.Add(false);
        }
    }

    void Update()
    {
        MoverPieza();

        if (Input.GetKeyDown(KeyCode.P))
        {
            ColocarPiezasAutomaticamente();
        }
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
                            espacioVacio.anchoredPosition = thisTile.targetPosition;
                            thisTile.targetPosition = ultimaPosicionEspacioVacio;
                            ActualizarPosicionPiezaCorrecta(thisTile);
                            contador++;
                            Debug.Log("Numero de intentos: " + contador);
                            if (TodasPiezasCorrectas())
                            {
                                mostrarPuzzle01.PuzzleResuelto();
                            }

                            break;
                        }
                    }
                }
            }
        }
    }

    private void ActualizarPosicionPiezaCorrecta(TilesScript tile)
    {
        int index = System.Array.IndexOf(tiles, tile);
        if (index != -1)
        {
            bool esCorrecta = false;
            for (int i = 0; i < posicionesCorrectas.Length; i++)
            {
                if (Vector2.Distance(posicionesCorrectas[i], tile.targetPosition) < 0.01f)
                {
                    esCorrecta = true;
                    break;
                }
            }

            posicionPiezaCorrecta[index] = esCorrecta;
        }
    }

    private bool TodasPiezasCorrectas()
    {
        foreach (bool posicion in posicionPiezaCorrecta)
        {
            if (!posicion)
            {
                return false;
            }
        }
        return true;
    }

    // Comentar luego, solo es para pruebas.
    private void ColocarPiezasAutomaticamente()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].targetPosition = posicionesCorrectas[i];
            tiles[i].GetComponent<RectTransform>().anchoredPosition = posicionesCorrectas[i];
            ActualizarPosicionPiezaCorrecta(tiles[i]);
        }

        if (TodasPiezasCorrectas())
        {
            mostrarPuzzle01.PuzzleResuelto();
        }
    }
}
