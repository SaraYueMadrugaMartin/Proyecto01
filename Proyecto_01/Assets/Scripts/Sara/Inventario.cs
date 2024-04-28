using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    [SerializeField] private GameObject inventario;

    public HuecosInventario[] huecosInventario;

    public static Inventario Instance;
    private bool estadoInvent = false;
    private int llaveID = 0;

    public bool TieneObjeto(string nombreItem)
    {
        foreach (HuecosInventario hueco in huecosInventario)
        {
            if (hueco.nombreItem == nombreItem)
            {
                return true;
            }
        }
        return false;
    }

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown("i") && estadoInvent)
        {
            Time.timeScale = 1;
            inventario.SetActive(false);
            estadoInvent = false;
        }
        else if(Input.GetKeyDown("i") && !estadoInvent)
        {
            Time.timeScale = 0;
            inventario.SetActive(true);
            estadoInvent = true;
        }
    }

    public void AñadirObjeto(string nombreItem, Sprite sprite)
    {
        Debug.Log("Añadiendo objeto al inventario: " + nombreItem);
        for (int i = 0; i < huecosInventario.Length; i++)
        {
            if (huecosInventario[i].estaCompleto == false)
            {
                huecosInventario[i].AñadirObjeto(nombreItem, sprite);
                // Llamar a ActualizarEstadoPuerta después de agregar la llave al inventario
                /*Puerta puerta = FindObjectOfType<Puerta>();
                if (puerta != null)
                {
                    puerta.ActualizarEstadoPuerta();
                }*/
                return;
            }
        }
    }

    public void VaciarHueco(string nombreItem)
    {
        // Buscar el hueco del inventario correspondiente y vaciarlo
        foreach (HuecosInventario hueco in huecosInventario)
        {
            if (hueco.nombreItem == nombreItem)
            {
                hueco.VaciarHueco();
                break;
            }
        }
    }
    public void RecibeIDLlave(int ID)
    {
        llaveID = ID;
    }
    public int BuscaIDLlave()
    {
        return llaveID;
    }
}
