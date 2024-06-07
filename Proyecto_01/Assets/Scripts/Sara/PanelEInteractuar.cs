using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelEInteractuar : MonoBehaviour
{
    [SerializeField] private GameObject panelInteraE;

    // Start is called before the first frame update
    void Start()
    {
        panelInteraE.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
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
