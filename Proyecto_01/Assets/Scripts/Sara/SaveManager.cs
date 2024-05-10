using UnityEngine;

[System.Serializable]

public struct SceneState
{
    public string nombreItem;
    public Vector2 posicionItems;
    public bool objetoActivo;

    public Vector2 posicionPlayer;

    public bool puertaDesbloqueada;
}

public class SaveManager: MonoBehaviour
{
    public static SaveManager instance;

    private SceneState savedSceneState;

    PlayerMovement infoPlayer;
    Items infoItems;

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

        //sceneState.vida = acciones.GetVida();
        sceneState.posicionPlayer = infoPlayer.GetPosition();
        //sceneState.enemigos = UIManager.instance.contenedorEnemigos;
        //sceneState.time = timer;

        //Items
        infoItems = FindObjectOfType<Items>();
        sceneState.posicionItems = infoItems.GetPositionItem();
        sceneState.objetoActivo = infoItems.SetObjetoActivo(bool);

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

            //acciones.SetVida(savedSceneState.vida);
            //Debug.Log("Vida" + savedSceneState.vida);
            infoPlayer.SetPosition(savedSceneState.posicionPlayer);
            Debug.Log("Posición" + savedSceneState.posicionPlayer);
            //SceneManager.LoadScene(1);
            //savedSceneState.enemigos = UIManager.instance.contenedorEnemigos;
            //savedSceneState.time = timer;
            //Debug.Log("Tiempo" + savedSceneState.time);
            //UIManager.instance.actualizaTextoTiempo(timer);
            //juegoParado = false;

            infoItems.SetPositionItem(savedSceneState.posicionItems);
            infoItems.SetObjetoActivo(savedSceneState.objetoActivo);
            Debug.Log("Posición del item: " + savedSceneState.posicionItems);
            // Aquí deberías activar o desactivar el objeto según el estado guardado
        }
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
