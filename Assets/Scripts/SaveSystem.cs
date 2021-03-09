using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveSystem {
    public static void savePlayerData(Player player) {
        string path = Application.persistentDataPath + "/" + player.getName() + ".player";
        FileStream stream = new FileStream(path, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, new PlayerData(player));
        stream.Close();
    }

    public static PlayerData loadPlayerData(string playerName) {
        string path = Application.persistentDataPath + "/" + playerName + ".player";
        if (File.Exists(path)) {
            FileStream stream = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return playerData;
        } else {
            Debug.Log("Could not find player with name: " + playerName);
            return null;
        }
    }
}
