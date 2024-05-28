using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarPuzzle02 : MonoBehaviour
{
    [SerializeField] private Puzle puzle;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject panelPuzzle;
    private bool jugadorTocando;

    // Start is called before the first frame update
    void Start()
    {
        panelPuzzle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(jugadorTocando && Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 0f;
            panelPuzzle.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            jugadorTocando = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorTocando = false;
        }
    }

    public void VerificarCodigoCompleto()
    {
        if (Puzle.totalNumerosCorrectos == 4)
        {
            Time.timeScale = 1f;
            panelPuzzle.SetActive(false);
        }
        else
        {
            Debug.Log("Este no es el código correcto");
        }
    }
}
