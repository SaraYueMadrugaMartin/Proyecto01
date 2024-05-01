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
    private bool cambiaAngulos = false;

    private void Awake()
    {
        transformPuntero = transform.Find("Puntero");
    }

    private void Update()
    {
        Vector3 posRaton = GetMouseWorldPosition();

        Vector3 direccionPuntero = (posRaton - transform.position).normalized;
        float angulo = Mathf.Atan2(direccionPuntero.y, direccionPuntero.x) * Mathf.Rad2Deg;

        angulo = Mathf.Clamp(angulo, anguloMin, anguloMax);
        Debug.Log(angulo);
        transformPuntero.eulerAngles = new Vector3(0, 0, angulo);

        Vector3 posMax = new Vector3(Mathf.Cos(anguloMax * Mathf.Deg2Rad)* 5, Mathf.Sin(anguloMax * Mathf.Deg2Rad) * 5, 0); 
        Debug.DrawRay(transformPuntero.position, posMax, Color.yellow);
        Vector3 posMin = new Vector3(Mathf.Cos(anguloMin * Mathf.Deg2Rad) * 5, Mathf.Sin(anguloMin * Mathf.Deg2Rad) * 5, 0);
        Debug.DrawRay(transformPuntero.position, posMin, Color.yellow);
    }

    public void VolteaPuntero()
    {
        Vector3 currentScale = transformPuntero.localScale;
        currentScale.x *= -1;
        transformPuntero.localScale = currentScale;
        //CambiaAngulos();
    }

    void CambiaAngulos()
    {
        cambiaAngulos = !cambiaAngulos;
        if (cambiaAngulos)
        {
            anguloMin += 180f;
            anguloMax += 180f;
        }
        else
        {
            anguloMin -= 180f;
            anguloMax -= 180f;
        }
        Debug.Log(anguloMin + ", " + anguloMax);
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
