using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarPuzzle01 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private PuzzleDeslizable puzzle01Deslizable;
    [SerializeField] private GameObject panelResultado;
    [SerializeField] private FadeAnimation fadeAnimation;
    private static GameObject panelPuzzle01;

    private bool jugadorTocando;

    void Start()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            Transform panelTransform = canvas.transform.Find("Puzzle01");
            if (panelTransform != null)
            {
                panelPuzzle01 = panelTransform.gameObject;
            }
        }
        panelResultado.SetActive(false);
    }

    void Update()
    {
        if (jugadorTocando && Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 0f;
            panelPuzzle01.SetActive(true);
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
        }
    }
    public void PuzzleResuelto()
    {
        StartCoroutine(MostrarResultadoPuzzle01());        
    }

    IEnumerator MostrarResultadoPuzzle01()
    {
        Debug.Log("Esperando 5 segundos en tiempo real...");
        yield return new WaitForSecondsRealtime(5f);
        fadeAnimation.FadeOut();
        StartCoroutine(QuitarPanelPuzzle());
    }

    IEnumerator QuitarPanelPuzzle()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        panelPuzzle01.SetActive(false);
        panelResultado.SetActive(true);
    }
}
