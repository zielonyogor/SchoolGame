using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
    public static void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.school";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = MiniGameManager.instance.gameData;
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGameData()
    {
        string path = Application.persistentDataPath + "/game.school";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            return data;
        }
        else
        {
            return new GameData();
        }
    }

    public static void DeleteSaveFile()
    {
        string path = Application.persistentDataPath + "/game.school";
        MiniGameManager.instance.gameData = new();
        if (File.Exists(path))
        {
            try
            {
                File.Delete(path);
                Debug.Log("Save file deleted successfully.");
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to delete save file: " + ex.Message);
            }
        }
    }
}
