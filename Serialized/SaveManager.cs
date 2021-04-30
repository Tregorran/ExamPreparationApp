using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

//Controls the saving and loading of information
public static class SaveManager
{
    public static string directory = "SaveData";
    public static string fileName = "MySave.txt";
    public static string persistentPath = Application.persistentDataPath;//the persistent data directory
    public static string slashes = "/";

    //converts object into byte stream and stores data
    public static void Save(SerializableObject so)
    {
        checkDirectoryExists();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(GetFullPath());
        bf.Serialize(file, so);
        file.Close();
    }

    private static void checkDirectoryExists() {
        if (!DirectoryExists())
        {
            Directory.CreateDirectory(persistentPath + slashes + directory);
        }
    }

    //deseralises the data in the file location returning the so object stored
    public static SerializableObject Load() {
        if (saveExists())
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(GetFullPath(), FileMode.OpenOrCreate);
                SerializableObject so = (SerializableObject)bf.Deserialize(file);
                file.Close();

                return so;
            }
            catch (SerializationException)
            {
                Debug.Log("Failed to load file");
            }
        }
        return null;
    }

    private static bool saveExists() {
        return File.Exists(GetFullPath());
    }

    private static bool DirectoryExists()
    {
        return Directory.Exists(persistentPath + slashes + directory);
    }

    private static string GetFullPath() {
        return persistentPath + slashes + directory + slashes + fileName;
    }
}
