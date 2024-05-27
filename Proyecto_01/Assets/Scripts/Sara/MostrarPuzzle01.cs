using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarPuzzle01 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private PuzzleDeslizable puzzle01Deslizable;
    private static GameObject panelPuzzle01;

    private bool jugadorTocando;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (jugadorTocando && Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 0f;
            panelPuzzle01.SetActive(true);
        }

        //PuzzleResuelto();
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
        }
    }

    /*public void PuzzleResuelto()
    {
        if (puzzle01Deslizable.posicionPiezaCorrecta.TrueForAll(posicion => posicion))
        {
            panelPuzzle01.SetActive(false);
            Time.timeScale = 1f;
            Debug.Log("Has resuelto el puzzle.");
        }
    }*/

    public static void PuzzleResuelto()
    {
        //Desactivar panelPuzzle
        Time.timeScale = 0f;
        panelPuzzle01.SetActive(false);
    }
}
