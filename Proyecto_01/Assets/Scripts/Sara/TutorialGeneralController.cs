using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGeneralController : MonoBehaviour
{
    [SerializeField] private GameObject panelTutorial01;
    private bool panelTutoActivo = false;

    private void Update()
    {
        if (panelTutoActivo && Input.GetMouseButtonDown(0))
        {
            DesactivarPanelTuto01();
        }
    }

    public void ActivarPanelTuto01()
    {
        Time.timeScale = 0f;
        panelTutorial01.SetActive(true);
        panelTutoActivo = true;
    }

    public void DesactivarPanelTuto01()
    {
        Destroy(panelTutorial01);
        Time.timeScale = 1f;
        panelTutoActivo = false;
    }
}
