using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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
    public Vector2 posicionItem;
    public bool[] spritesActivos;
}

[System.Serializable]
public struct InventarioState
{
    public bool objetoEnInventario;
    public string nombreItem;
    public Sprite spriteItem;
}

[System.Serializable]
public struct SceneState
{    
    public Vector2 posicionPlayer;
    public bool playerMiraDerecha;

    public List<PuertaState> puertasState; // Lista para guardar la información de PuertaState.
    public List<ItemState> itemsState; // Lista para guardar la información de ItemState.
    public List<InventarioState> inventarioState;
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
        // Al darle a Play, se quedaban guardados los últimos datos, por eso lo limpio al principio (temporal).
        PlayerPrefs.DeleteKey("SavedSceneState");

        if (PlayerPrefs.HasKey("SavedSceneState"))
        {
            string sceneStateJson = PlayerPrefs.GetString("SavedSceneState");
            savedSceneState = JsonUtility.FromJson<SceneState>(sceneStateJson);
        }
        else
        {
            savedSceneState = new SceneState();
            Debug.Log("La escena se ha iniciado en un estado limpio sin datos previamente guardados.");
        }
    }

    public void GuardarEstadoEscena()
    {
        SceneState sceneState = new SceneState();

        // JUGADOR
        infoPlayer = FindObjectOfType<PlayerMovement>();
        sceneState.posicionPlayer = infoPlayer.GetPosition();
        sceneState.playerMiraDerecha = infoPlayer.GetMiraDerecha();
        Debug.Log("El jugador mira hacia la: " + sceneState.playerMiraDerecha);

        // ITEMS
        Items[] items = FindObjectsOfType<Items>();
        sceneState.itemsState = new List<ItemState>();

        foreach (Items item in items)
        {
            ItemState itemState = new ItemState();

            itemState.nombreItem = item.nombreItem;
            itemState.objetoRecogido = item.GetObjetoRecogido();
            itemState.posicionItem = item.GetPosition();
            itemState.spritesActivos = new bool[item.GetSpriteRenderers().Length];

            for (int i = 0; i < item.GetSpriteRenderers().Length; i++)
            {
                itemState.spritesActivos[i] = item.GetSpriteRenderers()[i].enabled;
            }

            sceneState.itemsState.Add(itemState);
        }

        // INVENTARIO
        Inventario inventario = FindObjectOfType<Inventario>(); // Buscamos el objeto que contiene el componente "Inventario".
        sceneState.inventarioState = new List<InventarioState>(); // Lista donde almacenamos el estado del inventario.
        if (inventario != null)
        {
            foreach (var hueco in inventario.huecosInventario)
            {
                InventarioState inventarioState = new InventarioState();

                inventarioState.objetoEnInventario = inventario.GetObjetoEnInventario();

                if (hueco.estaCompleto)
                {
                    inventarioState.nombreItem = hueco.nombreItem;
                    inventarioState.spriteItem = hueco.sprite;
                }

                sceneState.inventarioState.Add(inventarioState);
            }
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

            //Debug.Log(sceneStateJson);
            savedSceneState = JsonUtility.FromJson<SceneState>(sceneStateJson);

            //PLAYER
            PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.SetPosition(savedSceneState.posicionPlayer);
                playerMovement.SetMiraDerecha(savedSceneState.playerMiraDerecha);
                Debug.Log("El jugador mira hacia la: " + savedSceneState.playerMiraDerecha);
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
                            item.SetPosition(itemState.posicionItem);
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
                            if (item.objetoRecogido)
                            {
                                item.DesactivarItem();
                            }
                        }
                    }
                }
            }

            // INVENTARIO
            Inventario inventario = FindObjectOfType<Inventario>();
            if (inventario != null && savedSceneState.inventarioState != null)
            {
                for (int i = 0; i < savedSceneState.inventarioState.Count; i++)
                {
                    var inventarioState = savedSceneState.inventarioState[i];

                    Debug.Log("Procesando InventarioState: " + (inventarioState.objetoEnInventario ? inventarioState.nombreItem : "Hueco vacío"));

                    // Si el objeto está en el inventario
                    if (inventarioState.objetoEnInventario)
                    {
                        // Busca el objeto asociado al nombre en el array items
                        Items item = Array.Find(items, x => x.nombreItem == inventarioState.nombreItem);
                        if (item != null)
                        {
                            // Añade el objeto al inventario utilizando su nombre y sprite, en el hueco correspondiente
                            if (i < inventario.huecosInventario.Length)
                            {
                                var sprite = item.GetSpriteItems(); // Obtener el sprite del objeto
                                inventario.AñadirObjeto(inventarioState.nombreItem, sprite); // Añadir el objeto al hueco correspondiente
                                Debug.Log("Objeto añadido al hueco: " + item.nombreItem);
                            }
                        }
                    }
                }
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
}
