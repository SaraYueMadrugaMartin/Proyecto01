using UnityEngine;

[System.Serializable]

public struct SceneState
{
    public string nombreItem;
    public Vector2 posicionItems;
    //public bool objetoActivo;
    public bool objetoRecogido;

    public Vector2 posicionPlayer;

    public bool puertaDesbloqueada;

    public Vector2 posicionInicialItems;
}

public class SaveManager: MonoBehaviour
{
    public static SaveManager instance;

    private SceneState savedSceneState;

    PlayerMovement infoPlayer;
    Puerta infoPuerta;
    Items infoItems;

    //private GameObject ultimoObjetoRecogido;

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

        //Jugador
        infoPlayer = FindObjectOfType<PlayerMovement>();
        sceneState.posicionPlayer = infoPlayer.GetPosition();


        //Items
        infoItems = FindObjectOfType<Items>(); // Si pongo esto, encuentra el primer objeto con este script, así que sale como que se ha recogido la Moneda, aún guardando antes de haber recogido nada.
        if (infoItems.ultimoObjetoRecogido != null)
        {
            sceneState.nombreItem = infoItems.nombreItem;
            sceneState.objetoRecogido = infoItems.GetObjetoRecogido();
            Debug.Log("Se ha guardado el dato de que el objeto recogido es: " + sceneState.nombreItem);
        }
        else
            Debug.Log("Ningún objeto se ha guardado");


        //PUERTAS
        infoPuerta = FindObjectOfType<Puerta>();
        sceneState.puertaDesbloqueada = infoPuerta.GetPuertaBloqueada();
        Debug.Log("La " + infoPuerta.name + " está " + sceneState.puertaDesbloqueada);

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


            //ITEMS
            infoItems.SetObjetoRecogido(savedSceneState.objetoRecogido);
            Debug.Log("El objeto " + savedSceneState.nombreItem + "está: " + savedSceneState.objetoRecogido);

            //PUERTAS
            infoPuerta.SetPuertaBloqueada(savedSceneState.puertaDesbloqueada);
            Debug.Log("Se ha guardado la información de que la puerta está: " + savedSceneState.puertaDesbloqueada);

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
        }
    }


    private void EliminarObjetoDelInventario(string nombreItem)
    {
        Inventario inventario = FindObjectOfType<Inventario>();
        inventario.VaciarHueco(nombreItem);
    }

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
