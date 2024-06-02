using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class PuertaFinal : MonoBehaviour
{
    [SerializeField] private Inventario inventario;
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private GameObject panelFaltanLlaves;
    [SerializeField] private TextMeshProUGUI textoLlaves;

    SFXManager sfxManager;



    private bool jugadorTocando = false;
    private bool todasLlaves = false;

    private int contadorLlaves;

    // Start is called before the first frame update
    void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();
        panelFaltanLlaves.SetActive(false);
        contadorLlaves = 3;
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
                if (contadorLlaves == 3)
                    textoLlaves.text = "Faltan 3 llaves para desbloquear esta puerta";
                else if(contadorLlaves == 2)
                    textoLlaves.text = "Faltan 2 llaves para desbloquear esta puerta";
                else if(contadorLlaves == 1)
                    textoLlaves.text = "Falta 1 llave para desbloquear esta puerta";
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
            contadorLlaves--;
            if (inventario.TieneObjeto("LlaveY"))
            {
                contadorLlaves--;
                if (inventario.TieneObjeto("LlaveZ"))
                {
                    contadorLlaves--;
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
        inventario.VaciarHueco("LlaveX");
        inventario.VaciarHueco("LlaveY");
        inventario.VaciarHueco("LlaveZ");
        Time.timeScale = 1f;
    }
}
