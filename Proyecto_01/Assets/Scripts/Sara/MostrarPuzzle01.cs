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

    private bool puzzleYaResuelto = false;

    void Start()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            Transform panelTransform = canvas.transform.Find("Puzzle01");
            if (panelTransform != null)
            {
                panelPuzzle01 = panelTransform.gameObject;
            } else
            {
                Debug.LogWarning("No ha encontrado panel puzzle");
            }
        }
        panelResultado.SetActive(false);
    }

    void Update()
    {
        if (jugadorTocando && Input.GetKeyDown(KeyCode.E))
        {
            if (!puzzleYaResuelto)
            {
                Time.timeScale = 0f;
                panelPuzzle01.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                PuzzleYaResuelto();
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
        }
    }
    public void PuzzleResuelto()
    {
        StartCoroutine(MostrarResultadoPuzzle01());        
    }

    IEnumerator MostrarResultadoPuzzle01()
    {
        Debug.Log("Has resuelto el puzzle, algo est� pasando.");
        puzzleYaResuelto = true;
        yield return new WaitForSecondsRealtime(2f);
        fadeAnimation.FadeOut();
        StartCoroutine(QuitarPanelPuzzle());
    }

    IEnumerator QuitarPanelPuzzle()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        panelPuzzle01.SetActive(false);
        panelResultado.SetActive(true);
    }

    public void QuitarResultadoPuzzle01()
    {
        Time.timeScale = 1f;
        panelResultado.SetActive(false);
        StopAllCoroutines();
    }

    public void CerrarPuzzle()
    {
        Time.timeScale = 1f;
        panelPuzzle01.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void PuzzleYaResuelto()
    {
        Time.timeScale = 0f;
        panelResultado.SetActive(true);
        panelPuzzle01.SetActive(false);
    }
}
