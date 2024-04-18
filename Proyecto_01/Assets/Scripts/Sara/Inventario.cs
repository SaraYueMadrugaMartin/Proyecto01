using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    [SerializeField] private GameObject inventario;
    [SerializeField] private List<GameObject> objetos = new List<GameObject>();
    [SerializeField] private List<Image> imagenesObjetos = new List<Image>(); // Lista de imágenes de los objetos

    public static Inventario Instance;
    private bool estadoInvent = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            estadoInvent = !estadoInvent; // Alternar el estado del inventario
            inventario.SetActive(estadoInvent);
        }
    }

    // Método para mostrar el objeto en el inventario
    private void MostrarObjetoInventario(int indice)
    {
        imagenesObjetos[indice].gameObject.SetActive(true); // Activar la imagen correspondiente al objeto en el inventario
    }

    public void AgregarObjeto(GameObject objeto)
    {
        objetos.Add(objeto);

        // Buscar el índice del objeto en la lista de objetos
        int indiceObjeto = objetos.IndexOf(objeto);

        // Mostrar la imagen del objeto en el inventario
        if (indiceObjeto != -1 && indiceObjeto < imagenesObjetos.Count) // Verificar que el índice sea válido
        {
            MostrarObjetoInventario(indiceObjeto);
        }
    }
}
