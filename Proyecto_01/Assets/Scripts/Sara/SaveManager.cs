using UnityEngine;
using Unity.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveManager
{
    public static void SavePlayerData(PlayerMovement player)
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
    }
}
