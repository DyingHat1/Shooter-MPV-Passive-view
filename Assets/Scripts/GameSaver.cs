using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class GameSaver
{
    public static void Save(Player player, Inventory inventory)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream filestream = new FileStream(Application.persistentDataPath + Config.SaveFileName, FileMode.Create);
        PlayerData data = new PlayerData(player, inventory);
        binaryFormatter.Serialize(filestream, data);
        filestream.Close();
    }
}

public static class GameLoader
{
    public static PlayerData Load()
    {
        if (File.Exists(Application.persistentDataPath + Config.SaveFileName) == false)
            return null;

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream filestream = new FileStream(Application.persistentDataPath + Config.SaveFileName, FileMode.Open);
        PlayerData data = binaryFormatter.Deserialize(filestream) as PlayerData;
        filestream.Close();
        return data;
    }
}
