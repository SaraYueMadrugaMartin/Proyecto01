using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutoriales : MonoBehaviour
{
    [SerializeField] private GameObject panelTutoriales;
    private Collider2D collidersTuto;
    private bool colliderDesactivado = false;
    private bool tutoAbierto = false;

    private bool panelAbierto = false;

    void Start()
    {
        collidersTuto = GetComponent<Collider2D>();
        panelTutoriales.SetActive(false);
    }

    private void Update()
    {
        if(tutoAbierto && Input.anyKeyDown)
        {
            SalirTuto();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !colliderDesactivado)
        {
            panelAbierto = true;
            Time.timeScale = 0f;
            panelTutoriales.SetActive(true);
            tutoAbierto = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if(panelAbierto)
                SFXManager.instance.StopAudios();
        }
    }

    public void SalirTuto()
    {
        panelTutoriales.SetActive(false);
        tutoAbierto = false;
        collidersTuto.enabled = false;
        colliderDesactivado = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
}
