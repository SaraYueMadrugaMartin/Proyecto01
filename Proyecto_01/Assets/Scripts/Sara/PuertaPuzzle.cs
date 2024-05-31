using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject panelMensajeNo;
    [SerializeField] private PuzzleDeslizable puzzleDeslizable;
    private Collider2D[] puertaColliders;
    private bool jugadorTocando;

    // Start is called before the first frame update
    void Start()
    {
        puertaColliders = GetComponents<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(jugadorTocando && Input.GetKeyDown(KeyCode.E))
        {
            if (puzzleDeslizable.puzzle01Resuelto)
            {
                DesactivarColliders();
            }
            else
            {
                panelMensajeNo.SetActive(true);

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
            panelMensajeNo.SetActive(false);
        }
    }

    public void DesactivarColliders()
    {
        foreach (Collider2D colliderPuerta in puertaColliders)
        {
            colliderPuerta.enabled = false;
        }
    }
}
