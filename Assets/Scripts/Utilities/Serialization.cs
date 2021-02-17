using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

public class Serialization
{
    private static string path = "Assets/Resources/Database/HighScore.txt";
    /// <summary>
    /// Game Instance will be added to the file, only keeping 5 best scores
    /// </summary>
    /// <param name="instance"></param>
    public static void SaveFile(CreateNewGameInstance instance, string path)
    {
        //OPTION 1
        //FileStream fileStream;
        //if (!File.Exists(path))
        //{
        //    fileStream = new FileStream(path, FileMode.Create);
        //}
        //fileStream = new FileStream(path, FileMode.Append);
        //BinaryFormatter binaryFormatter = new BinaryFormatter();
        //binaryFormatter.Serialize(fileStream, instance);
        //fileStream.Close();

        //OPTION 2
        //string json = JsonUtility.ToJson(instance);
        //File.AppendAllText(path, json);

        //OPTION 3
        JsonSerializer jsonSerializer = new JsonSerializer();
        FileStream fileStream;
        if (!File.Exists(path))
        {
            fileStream = new FileStream(path, FileMode.Create);
        }
        else
        {
            fileStream = new FileStream(path, FileMode.Append);
        }
        StreamWriter streamWriter = new StreamWriter(fileStream);
        jsonSerializer.Serialize(streamWriter, instance);
        streamWriter.Close();
        fileStream.Close();
    }

    /// <summary>
    /// A list of Instance will be returned from which we will be able to retrieve the instances informations
    /// </summary>
    public static List<CreateNewGameInstance> Load(string path)
    {
        return null;
    }

    public static string GetPath { get => path; set { path = value; } }

    public static string JsonSerializer { get; private set; }
}
