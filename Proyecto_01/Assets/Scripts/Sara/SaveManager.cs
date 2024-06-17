using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;


#region Toda informacion a guardar
[System.Serializable]
public struct AlexState
{
    public Vector2 posicionPlayer;
    public bool playerMiraDerecha;
    public float corrupcionPlayer;
    public int armaEquipadaPlayer;
    public int corrupcionTotal;
}

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
public struct PuertaFinalState
{
    public bool puertaFinal;
    public bool puertaSinLlaveFinal;
    public bool colliderPuertaFinalActivo;
    public bool spritePuertaFinal;
}

[System.Serializable]
public struct ItemState
{
    public int idItem;
    public string nombreItem;
    public bool objetoRecogido;
    public Vector2 posicionItem;
    public bool[] spritesActivos;
    public bool[] collidersItemsActivos;
}

[System.Serializable]
public struct InventarioState
{
    public int idItem;
    public bool objetoEnInventario;
    public string nombreItem;
    public Sprite spriteItem;
}

[System.Serializable]
public struct EnemigosState
{
    public int idEnemigo;
    public bool[] colliderEnemigosActivos;
    public int enemigosMuertosTotal;
    // Hay que guardar el estado de la animación
}

[System.Serializable]
public struct SceneState
{
    public bool cadenasActivas;

    public AlexState playerState;
    public List<PuertaState> puertasState; // Lista para guardar la información de PuertaState.
    public PuertaLaberintoState puertaLaberintoState;
    public PuertaFinalState puertaFinalState;
    public List<ItemState> itemsState; // Lista para guardar la información de ItemState.
    public List<EnemigosState> enemigosState;
    public List<InventarioState> inventarioState;
}
#endregion

public class SaveManager: MonoBehaviour
{
    public static SaveManager instance;
    private SceneState savedSceneState;

    private int indiceEscenaGuardada;

    [SerializeField] private GameObject botonCargarPartida;

    PuertasIDControler infoCadenas;
    
    Items[] items;
    Enemy[] infoEnemigos;

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
        Player infoPlayer = FindObjectOfType<Player>();
        sceneState.playerState.posicionPlayer = infoPlayer.GetPosition();
        sceneState.playerState.playerMiraDerecha = infoPlayer.GetMiraDerecha();
        Debug.Log("El jugador mira hacia la: " + sceneState.playerState.playerMiraDerecha);
        sceneState.playerState.corrupcionPlayer = infoPlayer.GetNivelCorrupcion();
        sceneState.playerState.armaEquipadaPlayer = infoPlayer.GetArmaEquipada();
        sceneState.playerState.corrupcionTotal = infoPlayer.GetCorrupcionTotal();
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
        Debug.Log("Las cadenas de la Planta 1 están: " +  sceneState.cadenasActivas);

        //Estado Puerta del Laberinto Planta 2
        PuertaLaberinto infoPuertaLaberinto = FindObjectOfType<PuertaLaberinto>();
        if(infoPuertaLaberinto != null)
        {
            sceneState.puertaLaberintoState.puertaLaberinto = infoPuertaLaberinto.GetPuertaLaberintoBloqueada();
            sceneState.puertaLaberintoState.puertaSinLlaveLaberinto = infoPuertaLaberinto.GetPuertaSinLlaveLaberintoActivada();
            sceneState.puertaLaberintoState.colliderPuertaLaberintoActivo = infoPuertaLaberinto.colliderPuertaLaberinto.enabled;
        }
        Debug.Log("La puerta del laberinto está: " + sceneState.puertaLaberintoState.puertaLaberinto);
        Debug.Log("Estado de la puerta sin llave laberinto al guardar: " + sceneState.puertaLaberintoState.puertaSinLlaveLaberinto);

        //Estado Puerta Final Planta 2
        PuertaFinal infoPuertaFinal = FindObjectOfType<PuertaFinal>();
        if (infoPuertaFinal != null)
        {
            sceneState.puertaFinalState.puertaFinal = infoPuertaFinal.GetPuertaFinalBloqueada();
            sceneState.puertaFinalState.puertaSinLlaveFinal = infoPuertaFinal.GetPuertaSinLlaveFinalActivada();
            sceneState.puertaFinalState.spritePuertaFinal = infoPuertaFinal.spritePuertaFinal.enabled;
            sceneState.puertaFinalState.colliderPuertaFinalActivo = infoPuertaFinal.colliderPuertaFinal.enabled;
        }
        Debug.Log("La puerta final está: " + sceneState.puertaFinalState.puertaFinal);
        Debug.Log("Estado de la puerta sin llave final al guardar: " + sceneState.puertaFinalState.puertaSinLlaveFinal);
        
        #endregion

        #region Guardar Numero Enemigos Muertos
        // ENEMIGOS
        infoEnemigos = FindObjectsOfType<Enemy>();
        sceneState.enemigosState = new List<EnemigosState>();
        foreach(Enemy enemigo in infoEnemigos)
        {
            EnemigosState enemigosState = new EnemigosState();

            enemigosState.idEnemigo = enemigo.idEnemigo;
            enemigosState.enemigosMuertosTotal = enemigo.GetNumEnemMuertos();
            Debug.Log("Número de enemigos muertos: " + enemigosState.enemigosMuertosTotal);
            enemigosState.colliderEnemigosActivos = new bool[enemigo.colliderEnemigo.Length];
            for (int i = 0; i < enemigo.colliderEnemigo.Length; i++)
            {
                enemigosState.colliderEnemigosActivos[i] = enemigo.colliderEnemigo[i].enabled;
            }
            sceneState.enemigosState.Add(enemigosState);
        }
        #endregion

        #region Guardado Items
        // ITEMS
        items = FindObjectsOfType<Items>();
        sceneState.itemsState = new List<ItemState>();

        foreach (Items item in items)
        {
            ItemState itemState = new ItemState();

            itemState.idItem = item.idsItems.IDsItem; // Asignamos el ID del item y lo guardamos
            itemState.nombreItem = item.nombreItem; // Asignamos el nombre del item y guardamos
            itemState.objetoRecogido = item.GetObjetoRecogido(); // Asignamos si el item ha sido o no recogido y lo guardamos
            itemState.posicionItem = item.GetPosition(); // Asignamos la posición donde está el item y lo guardamos
            Debug.Log("Item guardado: " + itemState.nombreItem + " con ID: " + itemState.idItem);

            itemState.collidersItemsActivos = new bool[item.collidersItems.Length];

            for (int i = 0; i < item.collidersItems.Length; i++)
            {
                itemState.collidersItemsActivos[i] = item.collidersItems[i].enabled;
                Debug.Log("Item guardado: " + itemState.nombreItem + " con ID: " + itemState.idItem);
            }

            itemState.spritesActivos = new bool[item.spriteRenderers.Length];
            for (int i = 0; i < item.spriteRenderers.Length; i++)
            {
                itemState.spritesActivos[i] = item.spriteRenderers[i].enabled; // Asignamos el estado del sprite del item y lo guardamos
            }


            sceneState.itemsState.Add(itemState); // Añadimos toda esta información a nuestra lista de Items
            //Debug.Log("Guardado Item - Nombre: " + itemState.nombreItem + "ID del item: " + itemState.idItem + ", Recogido: " + itemState.objetoRecogido);
        }
        #endregion

        #region Guardado Inventario
        // INVENTARIO
        Inventario inventario = FindObjectOfType<Inventario>();
        if (inventario != null)
        {
            sceneState.inventarioState = new List<InventarioState>();

            foreach (HuecosInventario hueco in inventario.huecosInventario)
            {
                if (hueco.estaCompleto)
                {
                    InventarioState inventarioState = new InventarioState();
                    inventarioState.nombreItem = hueco.nombreItem;
                    //inventarioState.idLlave = hueco.idLlave; // Si es necesario
                    inventarioState.spriteItem = hueco.sprite; // Guardar el sprite del objeto

                    sceneState.inventarioState.Add(inventarioState);
                }
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
            Player playerGuardado = FindObjectOfType<Player>();
            if (playerGuardado != null)
            {
                playerGuardado.SetPosition(savedSceneState.playerState.posicionPlayer);
                playerGuardado.SetMiraDerecha(savedSceneState.playerState.playerMiraDerecha);
                playerGuardado.SetNivelCorrupcion(savedSceneState.playerState.corrupcionPlayer);
                playerGuardado.SetArmaEquipada(savedSceneState.playerState.armaEquipadaPlayer);
                playerGuardado.SetCorrupcionTotal(savedSceneState.playerState.corrupcionTotal);
                Debug.Log("La corrupción total es de: " + savedSceneState.playerState.corrupcionTotal);
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
            Debug.Log("Se ha cargado la información de que las cadenas de la Planta 1 están: " + savedSceneState.cadenasActivas);

            //Estado de Puerta Laberinto Planta2
            PuertaLaberinto puertaLaberinto = FindObjectOfType<PuertaLaberinto>();
            if(puertaLaberinto != null)
            {
                puertaLaberinto.SetPuertaLaberintoBloqueada(savedSceneState.puertaLaberintoState.puertaLaberinto);
                puertaLaberinto.SetPuertaSinLlaveLaberintoActivada(savedSceneState.puertaLaberintoState.puertaSinLlaveLaberinto);
                puertaLaberinto.colliderPuertaLaberinto.enabled = savedSceneState.puertaLaberintoState.colliderPuertaLaberintoActivo;
            }
            Debug.Log("Se ha cargado la información de que la puerta del laberinto está: " + savedSceneState.puertaLaberintoState.puertaLaberinto);
            Debug.Log("Estado de la puerta sin llave laberinto al cargar: " + savedSceneState.puertaLaberintoState.puertaSinLlaveLaberinto);

            //Estado Puerta Final Planta 2
            PuertaFinal puertaFinal = FindObjectOfType<PuertaFinal>();
            if (puertaFinal != null)
            {
                puertaFinal.SetPuertaFinalBloqueada(savedSceneState.puertaFinalState.puertaFinal);
                puertaFinal.SetPuertaSinLlaveFinalActivada(savedSceneState.puertaFinalState.puertaSinLlaveFinal);
                puertaFinal.colliderPuertaFinal.enabled = savedSceneState.puertaFinalState.colliderPuertaFinalActivo;
                puertaFinal.spritePuertaFinal.enabled = savedSceneState.puertaFinalState.spritePuertaFinal;
            }
            #endregion

            #region Cargar Numero Enemigos Muertos
            //ENEMIGOS
            foreach (EnemigosState enemigoMuerto in savedSceneState.enemigosState)
            {
                Enemy enemigo = GetEnemigoID(enemigoMuerto.idEnemigo);
                if (enemigo != null)
                {
                    enemigo.SetNumEnemMuertos(enemigoMuerto.enemigosMuertosTotal);
                    Debug.Log("Has matado: " + enemigoMuerto.enemigosMuertosTotal);

                    for (int i = 0; i < enemigo.colliderEnemigo.Length; i++) // Recorremos los colliders de las puertas para saber si deben activarse de nuevo o no.
                    {
                        enemigo.colliderEnemigo[i].enabled = enemigoMuerto.colliderEnemigosActivos[i];
                    }
                }
            }
            #endregion

            #region Cargar Datos Items
            //ITEMS
            foreach(ItemState itemGuardado in savedSceneState.itemsState)
            {
                Items item = GetItemsID(itemGuardado.idItem);
                if (item != null)
                {
                    item.nombreItem = itemGuardado.nombreItem;
                    item.SetObjetoRecogido(itemGuardado.objetoRecogido);
                    item.SetPosition(itemGuardado.posicionItem);
                    Debug.Log("Item: " + itemGuardado.nombreItem + " con ID: " + itemGuardado.idItem);

                    for (int i = 0; i < item.collidersItems.Length; i++)
                    {
                        item.collidersItems[i].enabled = itemGuardado.collidersItemsActivos[i];
                    }

                    for (int i = 0; i < item.spriteRenderers.Length; i++)
                    {
                        item.spriteRenderers[i].enabled = itemGuardado.spritesActivos[i];
                    }
                }
                //Debug.Log("Se ha cargado - Nombre: " + itemGuardado.nombreItem + "ID del item: " + itemGuardado.idItem + ", Recogido: " + itemGuardado.objetoRecogido);
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
            /*Inventario inventario = FindObjectOfType<Inventario>();
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
            }*/
            #endregion

            #region Cargar Datos Inventario
            // INVENTARIO
            Inventario inventario = FindObjectOfType<Inventario>();
            if (inventario != null && savedSceneState.inventarioState != null)
            {
                foreach (InventarioState inventarioState in savedSceneState.inventarioState)
                {
                    // Añadir el objeto al inventario
                    Items item = FindObjectsOfType<Items>().FirstOrDefault(obj => obj.nombreItem == inventarioState.nombreItem);
                    if (item != null)
                    {
                        // Cargar el objeto en el inventario con su sprite
                        inventario.AñadirObjeto(inventarioState.nombreItem, inventarioState.spriteItem, inventarioState.idItem);
                    }
                }
            }
            #endregion

        }
    }
    #endregion

    #region Recibir IDs
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

    private Enemy GetEnemigoID(int idEnemigo)
    {
        Enemy[] enemigos = FindObjectsOfType<Enemy>();
        foreach (Enemy enemigo in enemigos)
        {
            if (enemigo.idEnemigo == idEnemigo)
            {
                return enemigo;
            }
            Debug.Log(enemigo.name);
        }
        return null;
    }

    private Items GetItemsID(int idItem)
    {
        Items[] items = FindObjectsOfType<Items>();
        foreach (Items item in items)
        {
            if (item.idsItems.GetIDsItem() == idItem)
            {
                return item;
            }
        }
        return null;
    }
    #endregion
}
