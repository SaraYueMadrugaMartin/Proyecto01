using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    [SerializeField] private GameObject inventario;
    [SerializeField] private List<GameObject> objetos = new List<GameObject>();
    public static Inventario Instance;
    private bool estadoInvent = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
