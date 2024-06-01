using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiarioGuardarPartida : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] private GameObject panelNoGuardado;

    private Inventario inventario;
    private bool jugadorTocando = false;

    private void Start()
    {
        gameManager = GameManager.instance;
        inventario = FindObjectOfType<Inventario>();
        panelNoGuardado.SetActive(false);
    }

    private void Update()
    {
        if (jugadorTocando && Input.GetKeyDown(KeyCode.E))
        {
            if(inventario != null)
            {
                if (inventario.TieneObjeto("Tinta"))
                {
                    gameManager.GuardarDatosEscena();
                    inventario.VaciarHueco("Tinta");
                    Debug.Log("Se han guardado los datos.");
                }
                else
                {
                    panelNoGuardado.SetActive(true);
                    Debug.Log("Necesitas tinta");
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorTocando = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorTocando = false;
            panelNoGuardado.SetActive(false);
        }
    }
}
