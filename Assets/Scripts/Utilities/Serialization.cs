using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serialization
{
    /// <summary>
    /// Game Instance will be added to the file, only keeping 5 best scores
    /// </summary>
    /// <param name="instance"></param>
    public static void SaveFile(CreateNewGameInstance instance, string path)
    {

    }

    /// <summary>
    /// A list of Instance will be returned from which we will be able to retrieve the instances informations
    /// </summary>
    public static List<CreateNewGameInstance> Load(string path)
    {

        return new List<CreateNewGameInstance>();
    }
}
