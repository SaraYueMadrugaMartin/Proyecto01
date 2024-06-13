using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ValvulaArrastrar : MonoBehaviour
{
    public GameObject piezaSeleccionada;
    public int PiezasEncajadas = 0;
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SeleccionarPieza();
        }

        if (Input.GetMouseButtonUp(0))
        {
            SoltarPieza();
        }

        if (piezaSeleccionada != null)
        {
            ArrastrarPieza();
        }

        if (PiezasEncajadas == 2)
        {
            // Se debe girar la válvula.
        }
    }

    void SeleccionarPieza()
    {
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        if (results.Count > 0)
        {
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("Valvula"))
                {
                    PiezasValvula thisTile = result.gameObject.GetComponent<PiezasValvula>();
                    if (thisTile != null && !thisTile.encajada)
                    {
                        piezaSeleccionada = result.gameObject;
                        piezaSeleccionada.GetComponent<PiezasValvula>().seleccionada = true;
                        break;
                    }
                }
            }
        }
    }

    void SoltarPieza()
    {
        if (piezaSeleccionada != null)
        {
            piezaSeleccionada.GetComponent<PiezasValvula>().seleccionada = false;
            piezaSeleccionada = null;
        }
    }

    void ArrastrarPieza()
    {
        Vector2 raton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        piezaSeleccionada.transform.position = new Vector2(raton.x, raton.y);
    }
}
