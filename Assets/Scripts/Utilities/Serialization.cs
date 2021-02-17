using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

public class Serialization
{
    private static string path = "Assets/Resources/Database/HighScore.txt";
    /// <summary>
    /// Game Instance will be added to the file, only keeping 5 best scores
    /// </summary>
    /// <param name="instance"></param>
    public static void SaveFile(CreateNewGameInstance instance, string path)
    {
        try
        {
            string json = JsonUtility.ToJson(instance);
            File.AppendAllText(path, json);
        }
        catch (ArgumentNullException e)
        {
            Debug.Log("Argument Null Exception : " + e.Message);
        }
    }

    /// <summary>
    /// A list of Instance will be returned from which we will be able to retrieve the instances informations
    /// </summary>
    public static List<CreateNewGameInstance> Load(string path)
    {
        List<CreateNewGameInstance> games = new List<CreateNewGameInstance>();
        string[] strings = File.ReadAllLines(path);
        foreach (string s in strings)
        {
            games.Add(JsonUtility.FromJson<CreateNewGameInstance>(s));
        }
        return games;
    }

    public static string GetPath { get => path; set { path = value; } }

    public static string JsonSerializer { get; private set; }
}
