using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public int idItem;
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
public struct EnemigosState
{
    public GameObject enemigoActivo;
}

[System.Serializable]
public struct SceneState
{    
    public Vector2 posicionPlayer;
    public bool playerMiraDerecha;

    public int enemigosMuertos;

    public List<PuertaState> puertasState; // Lista para guardar la información de PuertaState.
    public List<ItemState> itemsState; // Lista para guardar la información de ItemState.
    public List<InventarioState> inventarioState;
}

public class SaveManager: MonoBehaviour
{
    public static SaveManager instance;

    private SceneState savedSceneState;

    private int indiceEscenaGuardada;

    [SerializeField] private GameObject botonCargarPartida;

    Player infoPlayer;

    Enemigo[] infoEnemigos;

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
            botonCargarPartida.SetActive(true);
        }
        else
        {
            savedSceneState = new SceneState();
            Debug.Log("La escena acaba de empezar sin ningún dato guardado.");
            botonCargarPartida.SetActive(false);
        }
    }

    #region Guardar Datos Escena
    public void GuardarEstadoEscena()
    {
        SceneState sceneState = new SceneState();

        #region Guardado Player
        // JUGADOR
        infoPlayer = FindObjectOfType<Player>();
        sceneState.posicionPlayer = infoPlayer.GetPosition();
        sceneState.playerMiraDerecha = infoPlayer.GetMiraDerecha();
        Debug.Log("El jugador mira hacia la: " + sceneState.playerMiraDerecha);
        #endregion

        #region Guardar Numero Enemigos Muertos
        // ENEMIGOS
        /*infoEnemigos = FindObjectsOfType<Enemigo>();
        foreach (Enemigo enemigo in infoEnemigos)
        {
            sceneState.enemigosMuertos += enemigo.GetNumEnemMuertos();
            Debug.Log("Número de enemigos muertos: " + sceneState.enemigosMuertos);
        }*/

        sceneState.enemigosMuertos = Enemigo.contadorEnemigosMuertos;
        Debug.Log("Numero de enemigos muertos total: " + sceneState.enemigosMuertos);
        #endregion

        #region Guardado Items
        // ITEMS
        Items[] items = FindObjectsOfType<Items>();
        sceneState.itemsState = new List<ItemState>();

        foreach (Items item in items)
        {
            ItemState itemState = new ItemState();

            itemState.nombreItem = item.nombreItem;
            itemState.idItem = item.GetIDsItem();
            Debug.Log("Se ha guardado el objeto: " + itemState.nombreItem + " con ID: " + itemState.idItem);
            itemState.objetoRecogido = item.GetObjetoRecogido();
            itemState.posicionItem = item.GetPosition();
            itemState.spritesActivos = new bool[item.GetSpriteRenderers().Length];

            for (int i = 0; i < item.GetSpriteRenderers().Length; i++)
            {
                itemState.spritesActivos[i] = item.GetSpriteRenderers()[i].enabled;
            }

            sceneState.itemsState.Add(itemState);
        }
        #endregion

        #region Guardado Inventario
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
        #endregion

        #region Guardado Puertas Desbloqueadas
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
        #endregion

        // Guardamos el estado de la escena en formato JSON
        string sceneStateJson = JsonUtility.ToJson(sceneState);
        PlayerPrefs.SetString("SavedSceneState", sceneStateJson);
        PlayerPrefs.Save();
    }
    #endregion

    #region Cargar Datos Escena
    public void CargarEstadoEscena()
    {
        if (PlayerPrefs.HasKey("SavedSceneState"))
        {
            string sceneStateJson = PlayerPrefs.GetString("SavedSceneState");
            savedSceneState = JsonUtility.FromJson<SceneState>(sceneStateJson);

            #region Cargar Datos Player
            //PLAYER
            Player playerMovement = FindObjectOfType<Player>();
            if (playerMovement != null)
            {
                playerMovement.SetPosition(savedSceneState.posicionPlayer);
                playerMovement.SetMiraDerecha(savedSceneState.playerMiraDerecha);
            }
            #endregion
            Enemigo.contadorEnemigosMuertos = savedSceneState.enemigosMuertos;
            Debug.Log("Enemigos eliminados hasta ahora: " + savedSceneState.enemigosMuertos);
            #region Cargar Numero Enemigos Muertos
            //ENEMIGOS

            #endregion

            #region Cargar Datos Items
            //ITEMS
            Items[] items = FindObjectsOfType<Items>();
            if (items != null)
            {
                if (savedSceneState.itemsState != null)
                {
                    foreach (ItemState itemState in savedSceneState.itemsState)
                    {
                        Items item = Array.Find(items, x => x.GetIDsItem() == itemState.idItem);
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
            #endregion

            #region Cargar Datos Estado Inventario
            // INVENTARIO
            Inventario inventario = FindObjectOfType<Inventario>();
            if (inventario != null && savedSceneState.inventarioState != null)
            {
                for (int i = 0; i < savedSceneState.inventarioState.Count; i++)
                {
                    var inventarioState = savedSceneState.inventarioState[i];

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
                            }
                        }
                    }
                }
            }
            #endregion

            #region Cargar Datos Estado Puertas Bloqueadas
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
            #endregion
        }
    }
    #endregion

    private Puerta GetPuertaID(int idPuerta)
    {
        // Iteramos sobre todas las puertas en tu escena y devuelve la puerta con el ID dado
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
