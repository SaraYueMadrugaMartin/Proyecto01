using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiarioGuardarPartida : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] private GameObject panelNoGuardado;
    [SerializeField] private GameObject panelGuardando;

    private Inventario inventario;
    private bool jugadorTocando = false;

    SFXManager sfxManager;

    private void Start()
    {        
        gameManager = GameManager.instance;
        inventario = FindObjectOfType<Inventario>();
        panelNoGuardado.SetActive(false);
        panelGuardando.SetActive(false);
        sfxManager = FindObjectOfType<SFXManager>();
    }

    private void Update()
    {
        if (jugadorTocando && Input.GetKeyDown(KeyCode.E))
        {
            if(inventario != null)
            {
                if (inventario.TieneObjeto("Tinta"))
                {
                    GuardandoPartida();
                }
                else
                {
                    panelNoGuardado.SetActive(true);
                    Debug.Log("Necesitas tinta");
                }
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
            panelNoGuardado.SetActive(false);
        }
    }

    private void GuardandoPartida()
    {
        inventario.VaciarHueco("Tinta");
        panelGuardando.SetActive(true);
        Time.timeScale = 0f;
        sfxManager.PlaySFX(sfxManager.clipsDeAudio[15]);
        gameManager.GuardarDatosEscena();
        Debug.Log("Se han guardado los datos.");
        StartCoroutine(TerminarGuardarPartida());
    }

    IEnumerator TerminarGuardarPartida()
    {
        yield return new WaitForSecondsRealtime(2f);
        panelGuardando.SetActive(false);
        Time.timeScale = 1f;
    }
}
