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
public struct SceneState
{    
    public Vector2 posicionPlayer;

    public string nombreItem;
    public bool objetoRecogido;

    public List<PuertaState> puertasState; // Lista para guardar la información de PuertaState.
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


        // ITEMS (NO FUNCIONA NADA)
        /*infoItems = FindObjectOfType<Items>(); // Si pongo esto, encuentra el primer objeto con este script, así que sale como que se ha recogido la Moneda, aún guardando antes de haber recogido nada.
        if (infoItems.ultimoObjetoRecogido != null)
        {
            sceneState.nombreItem = infoItems.nombreItem;
            sceneState.objetoRecogido = infoItems.GetObjetoRecogido();
            Debug.Log("Se ha guardado el dato de que el objeto recogido es: " + sceneState.nombreItem);
        }
        else
            Debug.Log("Ningún objeto se ha guardado");*/


        //PUERTAS
        Puerta[] puertas = FindObjectsOfType<Puerta>(); // Creamos un array de Puertas y buscamos todos los objetos de tipo "Puerta". 
        sceneState.puertasState = new List<PuertaState>();

        foreach (Puerta puerta in puertas) // Recorremos cada elemento de "puertas".
        {
            PuertaState puertaState = new PuertaState();

            puertaState.idPuerta = puerta.idPuerta;
            puertaState.puertaBloqueada = puerta.GetPuertaBloqueada();
            puertaState.puertaActivada = puerta.gameObject.activeSelf;

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
            infoPlayer.SetPosition(savedSceneState.posicionPlayer);
            Debug.Log("Posición" + savedSceneState.posicionPlayer);


            //ITEMS (NO FUNCIONA NADA)
            /*infoItems.SetObjetoRecogido(savedSceneState.objetoRecogido);
            Debug.Log("El objeto " + savedSceneState.nombreItem + "está: " + savedSceneState.objetoRecogido);*/

            //PUERTAS
            /*foreach (PuertaState puertaState in savedSceneState.puertasState)
            {
                Puerta puerta = FindPuertaById(puertaState.idPuerta);
                if (puerta != null)
                {
                    puerta.SetPuertaBloqueada(puertaState.puertaBloqueada);
                    Debug.Log("Puerta ID " + puertaState.idPuerta + " está bloqueada: " + puertaState.puertaBloqueada);
                }
            }

            /*GameObject itemObject = GameObject.Find(savedSceneState.nombreItem);
            if (itemObject != null)
            {
                itemObject.transform.position = savedSceneState.posicionInicialItems;
            }

            if (!savedSceneState.objetoRecogido && savedSceneState.objetoActivo)
            {
                EliminarObjetoDelInventario(savedSceneState.nombreItem);
                Debug.Log("El objeto ha sido devuelto");
            }*/

            //PUERTAS
            //Puerta[] puertas = FindObjectsOfType<Puerta>();
            /*foreach (Puerta puerta in puertas)
            {
                puerta.gameObject.SetActive(true);
            }*/

            foreach (PuertaState puertaState in savedSceneState.puertasState)
            {
                Puerta puerta = GetPuertaID(puertaState.idPuerta);
                if (puerta != null)
                {
                    puerta.SetPuertaBloqueada(puertaState.puertaBloqueada);
                    puerta.gameObject.SetActive(puertaState.puertaActivada);

                    Debug.Log("Puerta ID " + puertaState.idPuerta + " - Bloqueada: " + puertaState.puertaBloqueada + ", Activada: " + puertaState.puertaActivada);
                }
                else
                {
                    Debug.LogWarning("No se pudo encontrar la puerta con ID: " + puertaState.idPuerta);
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

    private void EliminarObjetoDelInventario(string nombreItem)
    {
        Inventario inventario = FindObjectOfType<Inventario>();
        inventario.VaciarHueco(nombreItem);
    }

    //ANTIGUO SAVE SYSTEM
    /*public static void SavePlayerData(PlayerMovement player)
    {
        PlayerData playerData = new PlayerData(player);
        string dataPath = Application.persistentDataPath + "/player.save";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fileStream, playerData);
        fileStream.Close();
    }

    public static PlayerData LoadPlayerData()
    {
        string dataPath = Application.persistentDataPath + "/player.save";
        if(File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode .Open);
            BinaryFormatter formatter = new BinaryFormatter();
            PlayerData playerData = (PlayerData )formatter.Deserialize(fileStream);
            fileStream.Close ();
            return playerData;
        }
        else
        {
            return null;
        }
    }

    public static void DeleteSavedData()
    {
        string dataPath = Application.persistentDataPath + "/player.save";
        if (File.Exists(dataPath))
        {
            File.Delete(dataPath);
            Debug.Log("Se han borrado los datos.");
        }
        else
        {
            Debug.LogWarning("No hay datos guardados para eliminar.");
        }
    }*/
}
