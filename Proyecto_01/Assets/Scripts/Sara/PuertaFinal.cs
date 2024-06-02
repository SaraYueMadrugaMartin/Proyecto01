using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PuertaFinal : MonoBehaviour
{
    [SerializeField] private Inventario inventario;
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private GameObject panelFaltanLlaves;

    SFXManager sfxManager;

    private bool jugadorTocando = false;
    private bool todasLlaves = false;

    // Start is called before the first frame update
    void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();
        panelFaltanLlaves.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (jugadorTocando && Input.GetKeyDown(KeyCode.E))
        {
            ComprobarTieneLlaves();

            if (!todasLlaves)
            {
                panelFaltanLlaves.SetActive(true);
            }
            else
            {
                Time.timeScale = 0f;
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
            panelFaltanLlaves.SetActive(false);
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
                    todasLlaves = true;
                }
            }
        }
    }

    IEnumerator AbrirPuerta()
    {
        fadeAnimation.FadeOutNivel();
        yield return new WaitForSecondsRealtime(0.5f);
        
        sfxManager.PlaySFX(sfxManager.clipsDeAudio[13]);
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
