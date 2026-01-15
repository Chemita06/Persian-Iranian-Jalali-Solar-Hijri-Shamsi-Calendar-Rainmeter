using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveGame(Jugador jugador)
    {
        string path = Application.persistentDataPath + "/GameData.json";
        PlayerData data = new PlayerData(jugador);

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);

        Debug.Log("Datos guardados en: " + path);
    }

    public static PlayerData LoadGame()
    {
        string path = Application.persistentDataPath + "/GameData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            Debug.Log("Datos cargados desde: " + path);
            return data;
        }
        else
        {
            Debug.LogWarning("No se encontró el archivo de guardado en: " + path);
            return null;
        }
    }
    public static void DeleteSaveGame()
    {
        string path = Application.persistentDataPath + "/GameData.json";
    
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Datos de guardado eliminados correctamente.");
        }
        else
        {
            Debug.LogWarning("No se encontró ningún archivo de guardado para eliminar.");
        }
    }

    
}