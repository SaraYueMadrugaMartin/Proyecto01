using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct PuertaState
{
    public int idPuerta;
    public bool puertaBloqueada;
    public bool puertaActivada;
    public bool[] collidersActivos;
    public bool puertasSinLlaveActivadas;
}

[System.Serializable]
public struct PuertaLaberintoState
{
    public bool puertaLaberinto;
    public bool puertaSinLlaveLaberinto;
    public bool colliderPuertaLaberintoActivo;
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

    public bool cadenasActivas;
    
    public int enemigosMuertos;


    public List<PuertaState> puertasState; // Lista para guardar la informaci�n de PuertaState.
    public PuertaLaberintoState puertaLaberintoState;
    public List<ItemState> itemsState; // Lista para guardar la informaci�n de ItemState.
    public List<InventarioState> inventarioState;
}

public class SaveManager: MonoBehaviour
{
    public static SaveManager instance;
    private SceneState savedSceneState;

    private int indiceEscenaGuardada;

    [SerializeField] private GameObject botonCargarPartida;

    Player infoPlayer;

    PuertasIDControler infoCadenas;
    

    Items[] items;

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
        // Al darle a Play, se quedaban guardados los �ltimos datos, por eso lo limpio al principio (temporal).
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
            Debug.Log("La escena acaba de empezar sin ning�n dato guardado.");
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

        #region Guardado Puertas Desbloqueadas
        //PUERTAS
        Puerta[] puertas = FindObjectsOfType<Puerta>(); // Creamos un array de Puertas y buscamos todos los objetos de tipo "Puerta". 
        sceneState.puertasState = new List<PuertaState>();

        foreach (Puerta puerta in puertas) // Recorremos cada elemento "puerta" en el array creado antes de Puertas.
        {
            PuertaState puertaState = new PuertaState();

            puertaState.idPuerta = puerta.idPuerta;
            puertaState.puertaBloqueada = puerta.GetPuertaBloqueada();
            puertaState.puertaActivada = puerta.gameObject.activeSelf;
            puertaState.puertasSinLlaveActivadas = puerta.GetPuertaSinLlaveActivada();
            puertaState.collidersActivos = new bool[puerta.puertaColliders.Length];

            for (int i = 0; i < puerta.puertaColliders.Length; i++)
            {
                puertaState.collidersActivos[i] = puerta.puertaColliders[i].enabled;
            }

            sceneState.puertasState.Add(puertaState);

            Debug.Log("Puerta ID: " + puerta.idPuerta + ", Bloqueada: " + puertaState.puertaBloqueada + ", Activada: " + puertaState.puertaActivada);
        }

        //Estado CADENAS Planta 1
        infoCadenas = FindObjectOfType<PuertasIDControler>();
        sceneState.cadenasActivas = infoCadenas.GetCadenasActivadas();
        Debug.Log("Las cadenas de la Planta 1 est�n: " +  sceneState.cadenasActivas);

        //Estado Puerta del Laberinto Planta 2
        PuertaLaberinto infoPuertaLaberinto = FindObjectOfType<PuertaLaberinto>();
        if(infoPuertaLaberinto != null)
        {
            sceneState.puertaLaberintoState.puertaLaberinto = infoPuertaLaberinto.GetPuertaLaberintoBloqueada();
            sceneState.puertaLaberintoState.puertaSinLlaveLaberinto = infoPuertaLaberinto.GetPuertaSinLlaveLaberintoActivada();
            sceneState.puertaLaberintoState.colliderPuertaLaberintoActivo = infoPuertaLaberinto.colliderPuertaLaberinto.enabled;
        }

        Debug.Log("La puerta del laberinto est�: " + sceneState.puertaLaberintoState.puertaLaberinto);
        Debug.Log("Estado de la puerta sin llave laberinto al guardar: " + sceneState.puertaLaberintoState.puertaSinLlaveLaberinto);
        #endregion

        #region Guardar Numero Enemigos Muertos
        // ENEMIGOS
        /*infoEnemigos = FindObjectsOfType<Enemigo>();
        foreach (Enemigo enemigo in infoEnemigos)
        {
            sceneState.enemigosMuertos += enemigo.GetNumEnemMuertos();
            Debug.Log("N�mero de enemigos muertos: " + sceneState.enemigosMuertos);
        }*/

        sceneState.enemigosMuertos = Enemigo.contadorEnemigosMuertos;
        Debug.Log("Numero de enemigos muertos total: " + sceneState.enemigosMuertos);
        #endregion

        #region Guardado Items
        // ITEMS
        items = FindObjectsOfType<Items>();
        sceneState.itemsState = new List<ItemState>();

        foreach (Items item in items)
        {
            ItemState itemState = new ItemState();

            itemState.nombreItem = item.nombreItem; // Asignamos el nombre del item y guardamos
            itemState.idItem = item.GetIDsItem(); // Asignamos el ID del item y lo guardamos
            itemState.objetoRecogido = item.GetObjetoRecogido(); // Asignamos si el item ha sido o no recogido y lo guardamos
            itemState.posicionItem = item.GetPosition(); // Asignamos la posici�n donde est� el item y lo guardamos

            itemState.spritesActivos = new bool[item.GetSpriteRenderers().Length];
            for (int i = 0; i < item.GetSpriteRenderers().Length; i++)
            {
                itemState.spritesActivos[i] = item.GetSpriteRenderers()[i].enabled; // Asignamos el estado del sprite del item y lo guardamos
            }

            sceneState.itemsState.Add(itemState); // A�adimos toda esta informaci�n a nuestra lista de Items
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

            #region Cargar Datos Estado Puertas Bloqueadas
            //PUERTAS
            foreach (PuertaState puertaBloqueada in savedSceneState.puertasState)
            {
                Puerta puerta = GetPuertaID(puertaBloqueada.idPuerta);
                if (puerta != null)
                {
                    puerta.SetPuertaBloqueada(puertaBloqueada.puertaBloqueada);
                    puerta.gameObject.SetActive(puertaBloqueada.puertaActivada);
                    puerta.SetPuertaSinLlaveActivada(puertaBloqueada.puertasSinLlaveActivadas);

                    for (int i = 0; i < puerta.puertaColliders.Length; i++) // Recorremos los colliders de las puertas para saber si deben activarse de nuevo o no.
                    {
                        puerta.puertaColliders[i].enabled = puertaBloqueada.collidersActivos[i];
                    }

                    Debug.Log("Puerta ID " + puertaBloqueada.idPuerta + " - Bloqueada: " + puertaBloqueada.puertaBloqueada + ", Activada: " + puertaBloqueada.puertaActivada);
                }
            }

            //Estado CADENAS Planta 1
            PuertasIDControler cadenasEstado = FindObjectOfType<PuertasIDControler>();
            if(cadenasEstado != null)
                cadenasEstado.SetCadenasActivadas(savedSceneState.cadenasActivas);
            Debug.Log("Se ha cargado la informaci�n de que las cadenas de la Planta 1 est�n: " + savedSceneState.cadenasActivas);

            //Estado de Puerta Laberinto Planta2
            PuertaLaberinto puertaLaberinto = FindObjectOfType<PuertaLaberinto>();
            if(puertaLaberinto != null)
            {
                puertaLaberinto.SetPuertaLaberintoBloqueada(savedSceneState.puertaLaberintoState.puertaLaberinto);
                puertaLaberinto.SetPuertaSinLlaveLaberintoActivada(savedSceneState.puertaLaberintoState.puertaSinLlaveLaberinto);
                puertaLaberinto.colliderPuertaLaberinto.enabled = savedSceneState.puertaLaberintoState.colliderPuertaLaberintoActivo;
            }
            Debug.Log("Se ha cargado la informaci�n de que la puerta del laberinto est�: " + savedSceneState.puertaLaberintoState.puertaLaberinto);
            Debug.Log("Estado de la puerta sin llave laberinto al cargar: " + savedSceneState.puertaLaberintoState.puertaSinLlaveLaberinto);
            #endregion

            #region Cargar Numero Enemigos Muertos
            //ENEMIGOS
            Enemigo.contadorEnemigosMuertos = savedSceneState.enemigosMuertos;
            Debug.Log("Enemigos eliminados hasta ahora: " + savedSceneState.enemigosMuertos);
            #endregion

            #region Cargar Datos Items
            //ITEMS
            /*Items[] itemsGuardados = FindObjectsOfType<Items>();

            if (itemsGuardados != null)
            {
                foreach (ItemState itemState in savedSceneState.itemsState)
                {
                    Items item = itemsGuardados.SingleOrDefault(x => x.GetIDsItem() == itemState.idItem);
                    if (item != null)
                    {
                        Debug.Log("Hasta aqu�");
                    }
                }
            }
            /*Items[] itemsGuardados = FindObjectsOfType<Items>();
            if (itemsGuardados != null)
            {
                if (savedSceneState.itemsState != null)
                {
                    foreach (ItemState itemState in savedSceneState.itemsState)
                    {
                        // Busca el objeto Items con el mismo ID que el estado guardado
                        Items item = itemsGuardados.FirstOrDefault(x => x.GetIDsItem() == itemState.idItem);

                        if (item != null)
                        {
                            item.SetIDsItem(itemState.idItem);
                            item.objetoRecogido = itemState.objetoRecogido;
                            item.SetPosition(itemState.posicionItem);

                            // Actualiza el estado de los SpriteRenderer
                            var spriteRenderers = item.GetSpriteRenderers();
                            if (spriteRenderers != null)
                            {
                                for (int i = 0; i < spriteRenderers.Length; i++)
                                {
                                    if (i < itemState.spritesActivos.Length) // Verificar que el �ndice sea v�lido
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
            }*/
            #endregion

            #region Cargar Datos Estado Inventario
            // INVENTARIO
            /*Inventario inventario = FindObjectOfType<Inventario>();
            if (inventario != null && savedSceneState.inventarioState != null)
            {
                for (int i = 0; i < savedSceneState.inventarioState.Count; i++)
                {
                    var inventarioState = savedSceneState.inventarioState[i];

                    // Si el objeto est� en el inventario
                    if (inventarioState.objetoEnInventario)
                    {
                        // Busca el objeto asociado al nombre en el array items
                        Items item = Array.Find(items, x => x.nombreItem == inventarioState.nombreItem);
                        if (item != null)
                        {
                            // A�ade el objeto al inventario utilizando su nombre y sprite, en el hueco correspondiente
                            if (i < inventario.huecosInventario.Length)
                            {
                                var sprite = item.GetSpriteItems(); // Obtener el sprite del objeto
                                inventario.A�adirObjeto(inventarioState.nombreItem, sprite); // A�adir el objeto al hueco correspondiente
                            }
                        }
                    }
                }
            }*/
            #endregion

        }
    }
    #endregion

    private Puerta GetPuertaID(int idPuerta)
    {
        // Iteramos sobre todas las puertas y devolvemos la puerta con el ID
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
