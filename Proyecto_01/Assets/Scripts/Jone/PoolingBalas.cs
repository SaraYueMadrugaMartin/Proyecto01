using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingBalas : MonoBehaviour
{
    public static PoolingBalas instancia;
    public GameObject balaPrefab;
    [SerializeField] int cargador = 8;

    private List<GameObject> balas;

    private void Awake()
    {
        instancia = this;
        balas = new List<GameObject>(cargador);
        for (int i = 0; i < cargador; i++)
        {
            GameObject prefabInstancia = Instantiate(balaPrefab);
            prefabInstancia.transform.SetParent(transform);
            prefabInstancia.SetActive(false);
            balas.Add(prefabInstancia);
        }
    }

    public GameObject GetBala()
    {
        int totalBalas = balas.Count;

        for (int i = 0; i < totalBalas; i++)
        {
            if (!balas[i].activeInHierarchy)
            {
                balas[i].SetActive(true);
                return balas[i];
            }
        }
        // return null;

        // Si no hay balas suficientes desactivadas, creamos nuevas
        GameObject prefabInstancia = Instantiate(balaPrefab);
        prefabInstancia.transform.SetParent(transform);
        prefabInstancia.SetActive(true); // Cuando iniciamos el juego no se tienen que disparar
        balas.Add(prefabInstancia);

        return prefabInstancia;
    }
}
