using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Puntero : MonoBehaviour
{
    private Transform transformPuntero;
    [SerializeField] float anguloMax = 60f;
    [SerializeField] float anguloMin = -60f;
    private float angulo = 0f;
    public static bool cambiaAngulos = false;


    private void Awake()
    {
        transformPuntero = transform.Find("Puntero");
    }

    private void Update()
    {
        Vector3 posRaton = GetMouseWorldPosition(); // Posición ratón
       
        Vector3 direccionPuntero = (posRaton - transformPuntero.position).normalized;

        angulo = Mathf.Atan2(direccionPuntero.y, direccionPuntero.x) * Mathf.Rad2Deg;

        if (cambiaAngulos)
        {
            angulo = CambiaAngulos(angulo);
        }

        angulo = Mathf.Clamp(angulo, anguloMin, anguloMax);

        transformPuntero.localEulerAngles = new Vector3(0, 0, angulo);
    }

    public void VolteaPuntero()
    {
        cambiaAngulos = !cambiaAngulos;
        Vector3 currentScale = transformPuntero.localScale;
        currentScale.z *= -1;
        transformPuntero.localScale = currentScale;
    }

    private float CambiaAngulos(float angulo)
    {
        if (angulo > 0)
            angulo = (angulo - 180f) * -1;
        else
            angulo = (angulo + 180f) * -1;
        return angulo;
    }

    // Función para obtener el vector que indica la posición del ratón
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Utiliza la posición del ratón respecto a la cámara
        vec.z = 0f; // Porque estamos en 2D
        return vec;
    }
}
