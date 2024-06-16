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
    [SerializeField] private GameObject puzleValvula;
    [SerializeField] private GameObject puertaSinLlave;
    [SerializeField] private Player player;

    SFXManager sfxManager;

    private bool jugadorTocando = false;
    private bool todasPiezas = false;
    private bool valvulaCabeza = false;
    private bool valvulaCuerpo = false;
    //private Vector2 posNueva = new Vector2(7.51f, 65.7f);

    #region Variables para el Save System
    public Collider2D colliderPuertaLaberinto;
    private bool puertaLaberintoBloqueada = true;
    #endregion

    void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();
        colliderPuertaLaberinto = GetComponent<Collider2D>();
        panelFaltanPiezas.SetActive(false);
        puzleValvula.SetActive(false);
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
        puzleValvula.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void AbrirPuertaLaberinto()
    {
        StartCoroutine(AbrirPuerta());
    }

    IEnumerator AbrirPuerta()
    {
        yield return new WaitForSecondsRealtime(0f);
        //player.transform.position = posNueva;
        //gameObject.SetActive(false);
        colliderPuertaLaberinto.enabled = false;
        puertaSinLlave.SetActive(true);
        puertaLaberintoBloqueada = false;
        inventario.VaciarHueco("ValvulaCabeza");
        inventario.VaciarHueco("ValvulaCuerpo");
        Time.timeScale = 1f;
    }

    #region Metodos para el SaveSystem
    public bool GetPuertaLaberintoBloqueada()
    {
        return puertaLaberintoBloqueada;
    }

    public void SetPuertaLaberintoBloqueada(bool value)
    {
        puertaLaberintoBloqueada = value;
    }

    public bool GetPuertaSinLlaveLaberintoActivada()
    {
        return puertaSinLlave.activeSelf;
    }

    public void SetPuertaSinLlaveLaberintoActivada(bool value)
    {
        puertaSinLlave.SetActive(value);
    }
    #endregion
}
