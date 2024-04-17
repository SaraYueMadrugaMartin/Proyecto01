using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    [SerializeField] private GameObject inventario;
    [SerializeField] private List<GameObject> objetos = new List<GameObject>();
    public static Inventario Instance;
    private bool estadoInvent = true;

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
            if(!estadoInvent)
            {
                inventario.SetActive(false);
                estadoInvent = true;
            }
            else
            {
                inventario.SetActive(true);
                estadoInvent = false;
            }
        }
    }

    public void AgregarObjeto(GameObject objeto)
    {
        objetos.Add(objeto);
        //objeto.SetActive(false);
    }
}
