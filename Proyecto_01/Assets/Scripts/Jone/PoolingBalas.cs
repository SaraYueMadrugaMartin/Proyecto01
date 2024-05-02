using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingBalas : MonoBehaviour
{
    public static PoolingBalas instancia;
    public GameObject balaPrefab;
    [SerializeField] int cargadorFalso = 8;

    private List<GameObject> balas;

    private void Awake()
    {
        instancia = this;
        balas = new List<GameObject>(cargadorFalso);
        for (int i = 0; i < cargadorFalso; i++)
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
        return null;
    }
}
