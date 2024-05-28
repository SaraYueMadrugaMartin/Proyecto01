using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelEInteractuar : MonoBehaviour
{
    [SerializeField] private GameObject panelInteraE;

    //private bool jugadorTocando;

    private void Start()
    {
        panelInteraE.SetActive(false);
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ItemInteractuable"))
        {
            panelInteraE.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ItemInteractuable"))
        {
            panelInteraE.SetActive(false);
        }
    }
}
