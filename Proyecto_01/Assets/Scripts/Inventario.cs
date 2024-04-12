using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    [SerializeField] private GameObject inventario;
    public bool cuadrado = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cuadrado)
        {
            Debug.Log("Tengo cuadrado");
        }
    }
}
