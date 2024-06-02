using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaLaberinto : MonoBehaviour
{
    [SerializeField] private Inventario inventario;
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private GameObject panelFaltanPiezas;
    [SerializeField] private GameObject panelAdvertencia;

    SFXManager sfxManager;

    private bool jugadorTocando = false;
    private bool todasPiezas = false;

    void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();
        panelFaltanPiezas.SetActive(false);
        panelAdvertencia.SetActive(false);
    }

    void Update()
    {
        if(jugadorTocando && Input.GetKeyDown(KeyCode.E))
        {
            ComprobarTienePiezas();

            if (!todasPiezas)
            {
                panelFaltanPiezas.SetActive(true);
            }
            else
            {
                Time.timeScale = 0f;
                panelAdvertencia.SetActive(true);
                StartCoroutine(AbrirPuerta());
            }
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
            panelFaltanPiezas.SetActive(false);
        }
    }

    public void ComprobarTienePiezas()
    {
        if(inventario.TieneObjeto("ValvulaCabeza"))
        {
            if (inventario.TieneObjeto("ValvulaCuerpo"))
            {
                todasPiezas = true;
            }
        }
    }

    IEnumerator AbrirPuerta()
    {
        yield return new WaitForSecondsRealtime(1f);
        fadeAnimation.FadeOutNivel();
        sfxManager.PlaySFX(sfxManager.clipsDeAudio[19]);
        panelAdvertencia.SetActive(false);
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
