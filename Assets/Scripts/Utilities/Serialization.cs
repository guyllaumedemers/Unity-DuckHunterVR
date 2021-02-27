using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.Text;

public class Serialization
{
    private static string path = Application.persistentDataPath + "/HighScore.txt";
    //private static string path = "Assets/Resources/Database/HighScore.txt";
    /// <summary>
    /// Game Instance will be added to the file, only keeping 5 best scores
    /// </summary>
    /// <param name="instance"></param>
    public static void SaveFile(CreateNewGameInstance instance, string path)
    {
        JsonSerializer jsonSerializer = new JsonSerializer();
        FileStream fileStream;
        StreamWriter streamWriter;
        if (!File.Exists(path))
        {
            fileStream = new FileStream(path, FileMode.Create);
            streamWriter = new StreamWriter(fileStream);
        }
        else
        {
            fileStream = new FileStream(path, FileMode.Append);
            byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
            fileStream.Write(newline, 0, newline.Length);
            streamWriter = new StreamWriter(fileStream);
        }
        jsonSerializer.Serialize(streamWriter, instance);
        streamWriter.Close();
        fileStream.Close();
    }

    /// <summary>
    /// A list of Instance will be returned from which we will be able to retrieve the instances informations
    /// </summary>
    public static List<CreateNewGameInstance> Load(string path)
    {
        List<CreateNewGameInstance> gameInstances = new List<CreateNewGameInstance>();
        string[] myArr = File.ReadAllLines(path);
        JsonSerializer jsonSerializer = new JsonSerializer();
        StringReader stringReader;
        try
        {
            for (int i = 0; i < myArr.Length; i++)
            {
                stringReader = new StringReader(myArr[i]);
                CreateNewGameInstance instance = (CreateNewGameInstance)jsonSerializer.Deserialize(stringReader, typeof(CreateNewGameInstance));
                gameInstances.Add(instance);
            }
            return gameInstances;
        }
        catch (FileNotFoundException e)
        {
            Debug.Log("File Not Found Exception : " + e.Message);
        }
        return null;
    }

    public static string GetPath { get => path; set { path = value; } }

    public static string JsonSerializer { get; private set; }
}
