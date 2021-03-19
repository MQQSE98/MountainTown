///<summary>
/// Contains all functionality to save/load serialized player data.
///</summary>

//using UnityEngine;
//using UnityEngine.SceneManagement;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;

//public static class SaveManager
//{
//    public static void SaveGame(Player player, Scene scene)
//    {
//        SavePlayer(player);
//        SaveScene(scene);
//        // Any other individual pieces to be saved with go here.
//        // I have not concluded on an optimal save order.
//    }
//    public static void SavePlayer(Player player)
//    {
//        BinaryFormatter formatter = new BinaryFormatter();

//        string path = Application.persistentDataPath + "/player.save";
//        FileStream stream = new FileStream(path, FileMode.Create);

//        Player data = new Player(player);

//        formatter.Serialize(stream, data);
//        stream.Close();
//    }

//    public static Player LoadPlayer()
//    {
//        string path = Application.persistentDataPath + "/player.save";
//        if (File.Exists(path))
//        {
//            BinaryFormatter formatter = new BinaryFormatter();
//            FileStream stream = new FileStream(path, FileMode.Open);

//            Player data = formatter.Deserialize(stream) as Player;
//            stream.Close();

//            return data;
//        }
//        else
//        {
//            Debug.LogError("Save file not found in " + path);

//            return null;
//        }
//    }

//    public static void SaveScene(Scene scene)
//    {
//        // Saves the state of the scene
//        // in a binary file.
//    }

//    public static void LoadScene()
//    {
//        // Returns the scene object?
//        // Requires investigation..
//    }
//}