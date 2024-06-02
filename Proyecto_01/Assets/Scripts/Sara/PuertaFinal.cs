using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaFinal : MonoBehaviour
{
    [SerializeField] private Inventario inventario;

    private bool jugadorTocando = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (jugadorTocando && Input.GetKeyDown(KeyCode.E))
        {
            ComprobarTieneLlaves();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorTocando = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorTocando = false;
        }
    }

    public void ComprobarTieneLlaves()
    {
        if (inventario.TieneObjeto("LlaveX"))
        {
            if (inventario.TieneObjeto("LlaveY"))
            {
                if (inventario.TieneObjeto("LlaveZ"))
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
