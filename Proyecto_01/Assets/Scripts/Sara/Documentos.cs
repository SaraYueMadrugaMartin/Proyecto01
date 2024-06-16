using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Documentos : MonoBehaviour
{
    [SerializeField] private GameObject panelAsociado;

    public bool jugadorTocando = false;

    private bool docuAbierto = false;

    SFXManager sfxManager;

    private void Start()
    {
        sfxManager = FindObjectOfType<SFXManager>();
    }

    void Update()
    {
        if (jugadorTocando && Input.GetKeyDown("e"))
        {
            sfxManager.PlaySFX(sfxManager.clipsDeAudio[9]);
            Time.timeScale = 0;
            panelAsociado.SetActive(true);
            docuAbierto = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (docuAbierto && Input.GetKeyDown(KeyCode.Escape))
        {
            SalirDocs();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
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

    public void SalirDocs()
    {
        Time.timeScale = 1;
        sfxManager.PlaySFX(sfxManager.clipsDeAudio[9]);
        panelAsociado.SetActive(false);
        docuAbierto = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
