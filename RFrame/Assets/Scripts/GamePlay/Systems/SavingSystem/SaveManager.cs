using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using UnityEngine;
using Object = UnityEngine.Object;

public class SaveManager : SingletonMono<SaveManager>
{
    public Dictionary<string, SaveData> saveDic = new Dictionary<string, SaveData>();
    public List<ISaveHandler> saveList = new List<ISaveHandler>();
    public string jsonFolder;

    private void Start()
    {
        jsonFolder = Application.persistentDataPath + "/SAVE/";
    }

    public void Register(ISaveHandler handler)
    {
        if (saveList.Contains(handler) == false)
        {
            saveList.Add(handler);
        }
    }

    private void OnSaveEvent(Object msg)
    {
        var resultPath = jsonFolder + "save.txt";
        if (File.Exists(resultPath) == false)
        {
            File.Delete(resultPath);
        }
    }


    public void Save()
    {
        saveDic.Clear();
        foreach (var handler in saveList)
        {
            saveDic.Add(handler.GetType().Name, handler.GenerateSaveData());
        }

        var jsonData = JsonConvert.SerializeObject(saveDic);

        var resultPath = jsonFolder + "save.txt";
        if (File.Exists(resultPath) == false)
        {
            Directory.CreateDirectory(jsonFolder);
        }

        File.WriteAllText(resultPath, jsonData);
    }

    public void LoadData()
    {
        var resultPath = jsonFolder + "save.txt";
        var data = JsonConvert.DeserializeObject<Dictionary<string, SaveData>>(resultPath);
        foreach (var handler in saveList)
        {
            handler.LoadSaveData(data[handler.GetType().Name]);
        }
    }
}