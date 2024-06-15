using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuertaLaberinto : MonoBehaviour
{
    [SerializeField] private Inventario inventario;
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private GameObject panelFaltanPiezas;
    [SerializeField] private TextMeshProUGUI textoPiezas;
    //[SerializeField] private GameObject panelAdvertencia;
    [SerializeField] private GameObject puzleValvula;
    [SerializeField] private GameObject puertaSinLlave;
    [SerializeField] private Player player;

    //[SerializeField] private MostrarPuzleValvula mostrarPuzleValvula;

    SFXManager sfxManager;

    private bool jugadorTocando = false;
    private bool todasPiezas = false;
    private bool valvulaCabeza = false;
    private bool valvulaCuerpo = false;
    private Vector2 posNueva = new Vector2(7.51f, 65.7f);

    void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();
        panelFaltanPiezas.SetActive(false);
        puzleValvula.SetActive(false);
        puertaSinLlave.SetActive(false);
    }

    void Update()
    {
        if(jugadorTocando && Input.GetKeyDown(KeyCode.E))
        {
            ComprobarTienePiezas();

            if (!todasPiezas)
            {
                if (!valvulaCabeza && !valvulaCuerpo)
                {
                    panelFaltanPiezas.SetActive(true);
                    textoPiezas.text = "Necesitas una válvula para poder abrir la puerta";
                }
                else if (!valvulaCabeza)
                {
                    panelFaltanPiezas.SetActive(true);
                    textoPiezas.text = "Te falta la cabeza de la válvula";
                }
                else if (!valvulaCuerpo)
                {
                    panelFaltanPiezas.SetActive(true);
                    textoPiezas.text = "Te falta la barra de la válvula";
                }
            }
            else
            {
                //puertaSinLlave.SetActive(true);
                //Time.timeScale = 0f;
                //puzleValvula.SetActive(true);
                StartCoroutine(PuzleLaberinto());
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

    private void ComprobarTienePiezas()
    {
        valvulaCabeza = inventario.TieneObjeto("ValvulaCabeza");
        valvulaCuerpo = inventario.TieneObjeto("ValvulaCuerpo");
        todasPiezas = valvulaCabeza && valvulaCuerpo;
    }

    IEnumerator PuzleLaberinto()
    {
        yield return new WaitForSecondsRealtime(0f);
        fadeAnimation.FadeOut();
        //sfxManager.PlaySFX(sfxManager.clipsDeAudio[19]);
        //player.transform.position = posNueva;
        puzleValvula.SetActive(true);
        //gameObject.SetActive(false);
        //inventario.VaciarHueco("ValvulaCabeza");
        //inventario.VaciarHueco("ValvulaCuerpo");
        Time.timeScale = 0f;
    }

    /*IEnumerator AbrirPuerta()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        fadeAnimation.FadeOut();
        sfxManager.PlaySFX(sfxManager.clipsDeAudio[19]);
        player.transform.position = posNueva;
        panelAdvertencia.SetActive(false);
        gameObject.SetActive(false);
        inventario.VaciarHueco("ValvulaCabeza");
        inventario.VaciarHueco("ValvulaCuerpo");
        Time.timeScale = 1f;
    }*/
}
