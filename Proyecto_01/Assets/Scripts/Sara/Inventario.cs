using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    [SerializeField] private GameObject inventario;
    [SerializeField] private List<GameObject> objetos = new List<GameObject>();
    [SerializeField] private List<Image> imagenesObjetos = new List<Image>(); // Lista de imágenes de los objetos

    public HuecosInventario[] huecosInventario;

    public static Inventario Instance;
    private bool estadoInvent = false;

    public bool TieneObjetosRequeridos
    {
        get
        {
            return ObjetosGuardarPartida();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
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
        for(int i = 0; i < huecosInventario.Length; i++)
        {
            if (huecosInventario[i].estaCompleto == false)
            {
                huecosInventario[i].AñadirObjeto(nombreItem, sprite);
                return;
            }
        }
    }

    // Método para mostrar el objeto en el inventario
    /*private void MostrarObjetoInventario(int indice)
    {
        imagenesObjetos[indice].gameObject.SetActive(true); // Activar la imagen correspondiente al objeto en el inventario
    }

    public void AgregarObjeto(GameObject)
    {
        objetos.Add(objeto);

        // Buscar el índice del objeto en la lista de objetos
        int indiceObjeto = objetos.IndexOf(objeto);

        // Mostrar la imagen del objeto en el inventario
        if (indiceObjeto != -1 && indiceObjeto < imagenesObjetos.Count) // Verificar que el índice sea válido
        {
            MostrarObjetoInventario(indiceObjeto);
        }
    }*/

    private bool ObjetosGuardarPartida()
    {
        bool tieneTinta = false;
        bool tieneDiario = false;

        foreach (GameObject objeto in objetos)
        {
            if (objeto.CompareTag("Tinta"))
            {
                tieneTinta = true;
            }
            else if (objeto.CompareTag("Documento"))
            {
                tieneDiario = true;
            }
        }
        return tieneTinta && tieneDiario;
    }
}
