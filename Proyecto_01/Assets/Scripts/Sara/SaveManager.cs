using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PuertaState
{
    public int idPuerta;
    public bool puertaBloqueada;
    public bool puertaActivada;
    public bool[] collidersActivos;
}

[System.Serializable]
public struct ItemState
{
    public string nombreItem;
    public bool objetoRecogido;
    public bool[] spritesActivos;
}

[System.Serializable]
public struct SceneState
{    
    public Vector2 posicionPlayer;

    public List<PuertaState> puertasState; // Lista para guardar la información de PuertaState.
    public List<ItemState> itemsState; // Lista para guardar la información de ItemState.
}

public class SaveManager: MonoBehaviour
{
    public static SaveManager instance;

    private SceneState savedSceneState;

    PlayerMovement infoPlayer;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("SavedSceneState"))
        {
            string sceneStateJson = PlayerPrefs.GetString("SavedSceneState");
            savedSceneState = JsonUtility.FromJson<SceneState>(sceneStateJson);
        }
    }

    public void GuardarEstadoEscena()
    {
        SceneState sceneState = new SceneState();

        // JUGADOR
        infoPlayer = FindObjectOfType<PlayerMovement>();
        sceneState.posicionPlayer = infoPlayer.GetPosition();


        // ITEMS
        Items[] items = FindObjectsOfType<Items>();
        sceneState.itemsState = new List<ItemState>();

        foreach (Items item in items)
        {
            ItemState itemState = new ItemState();

            itemState.nombreItem = item.nombreItem;
            itemState.objetoRecogido = item.GetObjetoRecogido();
            itemState.spritesActivos = new bool[item.GetSpriteRenderers().Length];

            for (int i = 0; i < item.GetSpriteRenderers().Length; i++)
            {
                itemState.spritesActivos[i] = item.GetSpriteRenderers()[i].enabled;
            }

            sceneState.itemsState.Add(itemState);
            Debug.Log("Se ha guardado el dato de que el objeto recogido es: " + itemState.nombreItem + " - " + itemState.objetoRecogido);
        }

        //PUERTAS
        Puerta[] puertas = FindObjectsOfType<Puerta>(); // Creamos un array de Puertas y buscamos todos los objetos de tipo "Puerta". 
        sceneState.puertasState = new List<PuertaState>();

        foreach (Puerta puerta in puertas) // Recorremos cada elemento de "puertas".
        {
            PuertaState puertaState = new PuertaState();

            puertaState.idPuerta = puerta.idPuerta;
            puertaState.puertaBloqueada = puerta.GetPuertaBloqueada();
            puertaState.puertaActivada = puerta.gameObject.activeSelf;

            puertaState.collidersActivos = new bool[puerta.puertaColliders.Length];
            for (int i = 0; i < puerta.puertaColliders.Length; i++)
            {
                puertaState.collidersActivos[i] = puerta.puertaColliders[i].enabled;
            }

            sceneState.puertasState.Add(puertaState);

            Debug.Log("Puerta ID: " + puerta.idPuerta + ", Bloqueada: " + puertaState.puertaBloqueada + ", Activada: " + puertaState.puertaActivada);
        }

        // Guardamos el estado de la escena en formato JSON
        string sceneStateJson = JsonUtility.ToJson(sceneState);
        PlayerPrefs.SetString("SavedSceneState", sceneStateJson);
        PlayerPrefs.Save();

    }

    public void CargarEstadoEscena()
    {
        if (PlayerPrefs.HasKey("SavedSceneState"))
        {
            string sceneStateJson = PlayerPrefs.GetString("SavedSceneState");

            Debug.Log(sceneStateJson);
            savedSceneState = JsonUtility.FromJson<SceneState>(sceneStateJson);

            //PLAYER
            PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.SetPosition(savedSceneState.posicionPlayer);
                Debug.Log("Posición" + savedSceneState.posicionPlayer);
            }


            //ITEMS
            Items[] items = FindObjectsOfType<Items>();
            if (items != null)
            {
                Debug.Log("Número de objetos Items encontrados: " + items.Length);
                if (savedSceneState.itemsState != null)
                {
                    foreach (ItemState itemState in savedSceneState.itemsState)
                    {
                        Debug.Log("Procesando ItemState: " + itemState.nombreItem);
                        Items item = Array.Find(items, x => x.nombreItem == itemState.nombreItem);
                        if (item != null)
                        {
                            item.objetoRecogido = itemState.objetoRecogido;

                            // Actualiza el estado de los SpriteRenderer
                            var spriteRenderers = item.GetSpriteRenderers();
                            if (spriteRenderers != null)
                            {
                                for (int i = 0; i < spriteRenderers.Length; i++)
                                {
                                    if (i < itemState.spritesActivos.Length) // Verificar que el índice sea válido
                                    {
                                        spriteRenderers[i].enabled = itemState.spritesActivos[i];
                                    }
                                }
                            }
                            else
                            {
                                Debug.LogWarning("spriteRenderers es null para el objeto: " + itemState.nombreItem);
                            }

                            if (item.objetoRecogido)
                            {
                                item.DesactivarItem();
                            }

                            Debug.Log("El objeto " + itemState.nombreItem + " está: " + (itemState.objetoRecogido ? "Recogido" : "No recogido"));
                        }
                    }
                }
                /*else
                {
                    Debug.LogWarning("No se encontraron estados de items guardados.");
                }*/
            }

            //PUERTAS
            foreach (PuertaState puertaState in savedSceneState.puertasState)
            {
                Puerta puerta = GetPuertaID(puertaState.idPuerta);
                if (puerta != null)
                {
                    puerta.SetPuertaBloqueada(puertaState.puertaBloqueada);
                    puerta.gameObject.SetActive(puertaState.puertaActivada);

                    for (int i = 0; i < puerta.puertaColliders.Length; i++) // Recorremos los colliders de las puertas para saber si deben activarse de nuevo o no.
                    {
                        puerta.puertaColliders[i].enabled = puertaState.collidersActivos[i];
                    }

                    Debug.Log("Puerta ID " + puertaState.idPuerta + " - Bloqueada: " + puertaState.puertaBloqueada + ", Activada: " + puertaState.puertaActivada);
                }
                /*else
                {
                    Debug.LogWarning("No se pudo encontrar la puerta con ID: " + puertaState.idPuerta);
                }*/
            }
        }
        else
        {
            Debug.LogWarning("No se encontró ningún estado guardado de la escena.");
        }
    }

    private Puerta GetPuertaID(int idPuerta)
    {
        // Itera sobre todas las puertas en tu escena y devuelve la puerta con el ID dado
        Puerta[] puertas = FindObjectsOfType<Puerta>();
        foreach (Puerta puerta in puertas)
        {
            if (puerta.idPuerta == idPuerta)
            {
                return puerta;
            }
        }
        return null;
    }

    private void EliminarObjetoDelInventario(string nombreItem)
    {
        Inventario inventario = FindObjectOfType<Inventario>();
        inventario.VaciarHueco(nombreItem);
    }
}
